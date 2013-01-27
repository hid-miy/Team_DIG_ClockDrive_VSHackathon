namespace ClockDrive
{
    partial class DebugForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimeHMS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Simulate24Hour = new System.Windows.Forms.Button();
            this.Simulate1Hour = new System.Windows.Forms.Button();
            this.RecordRoad = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TimeHMS
            // 
            this.TimeHMS.Location = new System.Drawing.Point(119, 12);
            this.TimeHMS.Name = "TimeHMS";
            this.TimeHMS.Size = new System.Drawing.Size(217, 19);
            this.TimeHMS.TabIndex = 0;
            this.TimeHMS.TextChanged += new System.EventHandler(this.TimeHMS_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "時刻？(HH:MM:SS)";
            // 
            // Simulate24Hour
            // 
            this.Simulate24Hour.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Simulate24Hour.Location = new System.Drawing.Point(14, 37);
            this.Simulate24Hour.Name = "Simulate24Hour";
            this.Simulate24Hour.Size = new System.Drawing.Size(322, 78);
            this.Simulate24Hour.TabIndex = 2;
            this.Simulate24Hour.Text = "２４時間ぐるっと回す！！";
            this.Simulate24Hour.UseVisualStyleBackColor = true;
            this.Simulate24Hour.Click += new System.EventHandler(this.Simulate_Click);
            // 
            // Simulate1Hour
            // 
            this.Simulate1Hour.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Simulate1Hour.Location = new System.Drawing.Point(14, 121);
            this.Simulate1Hour.Name = "Simulate1Hour";
            this.Simulate1Hour.Size = new System.Drawing.Size(322, 78);
            this.Simulate1Hour.TabIndex = 3;
            this.Simulate1Hour.Text = "１時間ドライブする！！";
            this.Simulate1Hour.UseVisualStyleBackColor = true;
            this.Simulate1Hour.Click += new System.EventHandler(this.Simulate_Click);
            // 
            // RecordRoad
            // 
            this.RecordRoad.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RecordRoad.Location = new System.Drawing.Point(14, 205);
            this.RecordRoad.Name = "RecordRoad";
            this.RecordRoad.Size = new System.Drawing.Size(322, 78);
            this.RecordRoad.TabIndex = 4;
            this.RecordRoad.Text = "[REC] 道の軌跡を記録する";
            this.RecordRoad.UseVisualStyleBackColor = true;
            this.RecordRoad.Click += new System.EventHandler(this.RecordRoad_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 295);
            this.Controls.Add(this.RecordRoad);
            this.Controls.Add(this.Simulate1Hour);
            this.Controls.Add(this.Simulate24Hour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimeHMS);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DebugForm_FormClosed);
            this.Load += new System.EventHandler(this.DebugForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TimeHMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Simulate24Hour;
        private System.Windows.Forms.Button Simulate1Hour;
        private System.Windows.Forms.Button RecordRoad;
        private System.Windows.Forms.Timer timer1;
    }
}