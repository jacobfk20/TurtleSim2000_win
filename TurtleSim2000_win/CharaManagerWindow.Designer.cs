namespace TurtleSim2000_Linux
{
    partial class CharaManagerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lstActiveChara = new System.Windows.Forms.ListBox();
            this.lstCommands = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDumpLog = new System.Windows.Forms.Button();
            this.chkDumpOnExit = new System.Windows.Forms.CheckBox();
            this.btnResetCharaManager = new System.Windows.Forms.Button();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active Chara";
            // 
            // lstActiveChara
            // 
            this.lstActiveChara.FormattingEnabled = true;
            this.lstActiveChara.Location = new System.Drawing.Point(15, 34);
            this.lstActiveChara.Name = "lstActiveChara";
            this.lstActiveChara.Size = new System.Drawing.Size(175, 95);
            this.lstActiveChara.TabIndex = 1;
            // 
            // lstCommands
            // 
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.Location = new System.Drawing.Point(196, 34);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.Size = new System.Drawing.Size(365, 199);
            this.lstCommands.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Command Log";
            // 
            // btnDumpLog
            // 
            this.btnDumpLog.Location = new System.Drawing.Point(15, 237);
            this.btnDumpLog.Name = "btnDumpLog";
            this.btnDumpLog.Size = new System.Drawing.Size(175, 23);
            this.btnDumpLog.TabIndex = 4;
            this.btnDumpLog.Text = "Dump Log";
            this.btnDumpLog.UseVisualStyleBackColor = true;
            this.btnDumpLog.Click += new System.EventHandler(this.btnDumpLog_Click);
            // 
            // chkDumpOnExit
            // 
            this.chkDumpOnExit.AutoSize = true;
            this.chkDumpOnExit.Location = new System.Drawing.Point(196, 241);
            this.chkDumpOnExit.Name = "chkDumpOnExit";
            this.chkDumpOnExit.Size = new System.Drawing.Size(123, 17);
            this.chkDumpOnExit.TabIndex = 5;
            this.chkDumpOnExit.Text = "Dump on CharaExit?";
            this.chkDumpOnExit.UseVisualStyleBackColor = true;
            // 
            // btnResetCharaManager
            // 
            this.btnResetCharaManager.BackColor = System.Drawing.Color.DarkRed;
            this.btnResetCharaManager.Location = new System.Drawing.Point(15, 135);
            this.btnResetCharaManager.Name = "btnResetCharaManager";
            this.btnResetCharaManager.Size = new System.Drawing.Size(175, 36);
            this.btnResetCharaManager.TabIndex = 6;
            this.btnResetCharaManager.Text = "Reset CharaManager";
            this.btnResetCharaManager.UseVisualStyleBackColor = false;
            this.btnResetCharaManager.Click += new System.EventHandler(this.btnResetCharaManager_Click);
            // 
            // chkAutoScroll
            // 
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Location = new System.Drawing.Point(325, 241);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(77, 17);
            this.chkAutoScroll.TabIndex = 7;
            this.chkAutoScroll.Text = "Auto Scroll";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            // 
            // CharaManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 279);
            this.Controls.Add(this.chkAutoScroll);
            this.Controls.Add(this.btnResetCharaManager);
            this.Controls.Add(this.chkDumpOnExit);
            this.Controls.Add(this.btnDumpLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstCommands);
            this.Controls.Add(this.lstActiveChara);
            this.Controls.Add(this.label1);
            this.Name = "CharaManagerWindow";
            this.Text = "CharaManager Debugger";
            this.Load += new System.EventHandler(this.CharaManagerWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstActiveChara;
        private System.Windows.Forms.ListBox lstCommands;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDumpLog;
        private System.Windows.Forms.CheckBox chkDumpOnExit;
        private System.Windows.Forms.Button btnResetCharaManager;
        private System.Windows.Forms.CheckBox chkAutoScroll;
    }
}