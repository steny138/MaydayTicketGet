namespace MaydayTicketGet
{
    partial class MaydayTicket
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaydayTicket));
            this.ExecuteTimeTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ResultView = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ExecuteTimeTxt
            // 
            this.ExecuteTimeTxt.Location = new System.Drawing.Point(12, 61);
            this.ExecuteTimeTxt.Name = "ExecuteTimeTxt";
            this.ExecuteTimeTxt.Size = new System.Drawing.Size(352, 22);
            this.ExecuteTimeTxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 10F);
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "執行時間(yyyy/MM/dd HH:mm)";
            // 
            // ResultView
            // 
            this.ResultView.FormattingEnabled = true;
            this.ResultView.ItemHeight = 12;
            this.ResultView.Location = new System.Drawing.Point(12, 179);
            this.ResultView.Name = "ResultView";
            this.ResultView.Size = new System.Drawing.Size(352, 208);
            this.ResultView.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 10F);
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "執行結果";
            // 
            // MaydayTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 399);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ResultView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExecuteTimeTxt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaydayTicket";
            this.Text = "寶寶想要搶票但寶寶不說";
            this.Load += new System.EventHandler(this.MaydayTicket_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ExecuteTimeTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ResultView;
        private System.Windows.Forms.Label label2;
    }
}

