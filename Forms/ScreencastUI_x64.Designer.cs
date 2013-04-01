namespace ShareX.Forms
{
    partial class ScreencastUI
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
            this.components = new System.ComponentModel.Container();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.ttApp = new System.Windows.Forms.ToolTip(this.components);
            this.timerScreencastDelay = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progress.Location = new System.Drawing.Point(0, 0);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(32, 32);
            this.progress.TabIndex = 0;
            this.ttApp.SetToolTip(this.progress, "ShareXmod - Encoding in progresss");
            this.progress.Visible = false;
            // 
            // timerScreencast
            // 
            this.timerScreencastDelay.Enabled = true;
            this.timerScreencastDelay.Interval = 750;
            this.timerScreencastDelay.Tick += new System.EventHandler(this.timerScreencast_Tick);
            // 
            // ScreencastUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ShareX.Properties.Resources.record;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(32, 32);
            this.Controls.Add(this.progress);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(32, 32);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(32, 32);
            this.Name = "ScreencastUI";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ttApp.SetToolTip(this, "ShareXmod");
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ScreencastUI_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScreencastUI_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ScreencastUI_MouseDoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.ToolTip ttApp;
        private System.Windows.Forms.Timer timerScreencastDelay;
    }
}