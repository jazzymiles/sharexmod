namespace HelpersLib.UserControls
{
    partial class AfterCaptureTasksUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpTasks = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flpTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTasks.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTasks.Location = new System.Drawing.Point(0, 0);
            this.flpTasks.Name = "flowLayoutPanel1";
            this.flpTasks.Size = new System.Drawing.Size(275, 174);
            this.flpTasks.TabIndex = 0;
            // 
            // AfterCatureTasksUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpTasks);
            this.Name = "AfterCatureTasksUI";
            this.Size = new System.Drawing.Size(275, 174);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpTasks;
    }
}
