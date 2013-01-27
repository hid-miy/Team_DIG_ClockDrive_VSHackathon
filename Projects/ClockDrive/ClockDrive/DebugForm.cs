using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ClockDrive
{
    public partial class DebugForm : Form
    {
        private Form1 SUT;
        private DateTime recordStarted;
        private Road recordRoad;
        private const double intervalSeconds = 5.0;

        /// <summary>
        /// 生成時に、親フォームへの参照を退避しておく
        /// </summary>
        public DebugForm(Form1 sut)
        {
            InitializeComponent();
            SUT = sut;
            recordRoad = new Road(Application.StartupPath + @"\datas\RoadData.csv");
        }

        /// <summary>
        /// 時刻の初期値をセットしておく
        /// </summary>
        private void DebugForm_Load(object sender, EventArgs e)
        {
            TimeHMS.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 時刻値が変更されたら、親フォームの描画内容へ反映させる
        /// </summary>
        private void TimeHMS_TextChanged(object sender, EventArgs e)
        {
            DateTime timeHMS;
            var success = DateTime.TryParse(TimeHMS.Text, out timeHMS);
            SUT.Draw(success ? timeHMS : DateTime.Now);
        }

        /// <summary>
        /// 早回しでシミュレート開始する（２４時間 or １時間）
        /// </summary>
        private void Simulate_Click(object sender, EventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();

            var totalHours = (sender == Simulate24Hour ? 24 : 1);
            var intervalSeconds = (sender == Simulate24Hour ? 90 : 15);
            var startTime = (sender == Simulate24Hour ? new DateTime(2000, 1, 1, 0, 0, 0) : DateTime.Now);
            for (var i = 0; i <= (totalHours * 60 * 60); i += intervalSeconds)
            {
                SUT.cloud.Move(1);
                SUT.Draw(startTime.AddSeconds(i));
                Application.DoEvents();
            }

            watch.Stop();
            this.Text = string.Format("所要時間は {0:0.000}秒 でした", watch.ElapsedMilliseconds * 0.001);
        }

        /// <summary>
        /// 道の軌跡を、手動で記録する
        /// </summary>
        private void RecordRoad_Click(object sender, EventArgs e)
        {
            var instruction = string.Format(
                "今から、マウスポインタの動きどおりに道の軌跡を記録します。"
                + "\n■ OKをクリックした{0}秒後から、軌跡の記録が開始されます。"
                + "\n■ つまり、{0}秒以内に'0時'にあたる位置にマウスポインタを置いてください。"
                + "\n■ そして、{0}秒間かけて'0時'から'1時'にあたる位置まで動かしていってください。"
                + "\n■ まず'0時'から'1時'の部分を記録し、その後は{0}秒ごとに、'1時'から'2時'の部分、'2時'から'3時'の部分、と移っていきます。"
                + "\n■ '11時'から'12時'の部分を記録し終わったら、道の軌跡が出来上がります。",
                intervalSeconds
                );
            var result = MessageBox.Show(instruction, "道の軌跡を、手動で記録する", MessageBoxButtons.OKCancel);
            if (result == System.Windows.Forms.DialogResult.Cancel) return;

            //TODO: ガイドラインとして、中心から１２等分する放射線を描く

            timer1.Enabled = true;
            recordStarted = DateTime.Now;
            recordRoad.roadPositions.Clear();
        }

        /// <summary>
        /// 道の軌跡を、手動で記録する
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            var recordingHour = DateTime.Now.Subtract(recordStarted).TotalSeconds / intervalSeconds - 1.0;

            // 最後まで記録したら、一時CSVファイルへ保存する
            if (recordingHour >= 12)
            {
                timer1.Enabled = false;

                //TODO: 動きノイズ低減のため、ローパスフィルタを適用する（過去５回ぶんのサンプルで平滑化する）

                var exportFilePath = Application.StartupPath + string.Format(@"\datas\RoadData.{0}.csv", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                using (var writer = new StreamWriter(exportFilePath))
                {
                    foreach(var pos in recordRoad.roadPositions)
                    {
                        writer.WriteLine(string.Format("{0},{1}", pos.X, pos.Y));
                    }
                }
                MessageBox.Show(string.Format("道の軌跡を記録しました！\n出力CSV '{0}'", exportFilePath), "お疲れ様です！！");
                return;
            }

            // どこを記録中で、残り時間があと何秒かを更新し続ける
            var elapsed = intervalSeconds * (recordingHour - (int)recordingHour);
            this.Text = (recordingHour < 0
                ? string.Format("あと{0:0.00}秒後に記録を開始します。", -elapsed)
                : string.Format("{0}時～{1}時を記録中、あと{2:0.00}秒", (int)recordingHour, ((int)recordingHour + 1) % 12, intervalSeconds - elapsed)
                );
            RecordRoad.Text = this.Text;

            using (var g = SUT.CreateGraphics())
            {
                if (recordingHour >= 0)
                {
                    // マウスポインタの位置を取得し、動いていれば、記録して、そこに丸を描く（静止していれば記録しない）
                    var pos = SUT.PointToClient(Cursor.Position);
                    if (recordRoad.roadPositions.Count > 0
                        && !pos.Equals(recordRoad.roadPositions[recordRoad.roadPositions.Count - 1]))
                    {
                        recordRoad.roadPositions.Add(pos);
                    }
                    var color = Color.FromArgb((int)(elapsed * 256 / intervalSeconds), 128, 255 - (int)(elapsed * 256 / intervalSeconds));
                    g.DrawEllipse(new Pen(Brushes.Red), (int)pos.X - 3, (int)pos.Y - 3, 6, 6);

                    // ガイドラインとして、短針の現在角度を描く
                    var angle = Math.PI * 2 * (-0.25 + Road.CalcPositionRatio(new DateTime(2000, 1, 1, (int)recordingHour, (int)(recordingHour * 60) % 60, 0)));
                    g.DrawLine(Pens.DarkRed
                                , SUT.Width / 2, SUT.Height / 2
                                , (int)(SUT.Width / 2 + Math.Cos(angle) * 1000), (int)(SUT.Height / 2 + Math.Sin(angle) * 1000)
                                );
                }

                // ガイドラインとして、０時～１２時の分割角度を描く
                for (var h = 0; h < 12; h++)
                {
                    var next = Math.PI * 2 * (-0.25 + Road.CalcPositionRatio(new DateTime(2000, 1, 1, h, 0, 0)));
                    g.DrawLine(Pens.Orange
                                , SUT.Width / 2, SUT.Height / 2
                                , (int)(SUT.Width / 2 + Math.Cos(next) * 1000), (int)(SUT.Height / 2 + Math.Sin(next) * 1000)
                                );
                }
            }
        }

        /// <summary>
        /// 閉じるとき、タイマーを再開する
        /// </summary>
        private void DebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SUT.EnableTimer();
        }

    }
}
