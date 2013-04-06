namespace HelpersLib.Hotkeys2
{
    partial class WorkflowManager
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
            this.hmHotkeys = new HelpersLib.Hotkeys2.HotkeyManagerControl();
            this.SuspendLayout();
            // 
            // hmHotkeys
            // 
            this.hmHotkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmHotkeys.Location = new System.Drawing.Point(0, 0);
            this.hmHotkeys.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.hmHotkeys.Name = "hmHotkeys";
            this.hmHotkeys.Size = new System.Drawing.Size(741, 613);
            this.hmHotkeys.TabIndex = 0;
            // 
            // WorkflowManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 613);
            this.Controls.Add(this.hmHotkeys);
            this.MinimumSize = new System.Drawing.Size(757, 649);
            this.Name = "WorkflowManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Workflows";
            this.ResumeLayout(false);

        }

        #endregion

        private HotkeyManagerControl hmHotkeys;
    }
}