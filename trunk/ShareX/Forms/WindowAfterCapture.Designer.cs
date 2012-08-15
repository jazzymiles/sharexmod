namespace ShareX
{
    partial class WindowAfterCapture
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblGeneralInfo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpActions = new System.Windows.Forms.TabPage();
            this.tpImageResize = new System.Windows.Forms.TabPage();
            this.ucImageResize = new ShareX.ImageResizeUI();
            this.tpImageQuality = new System.Windows.Forms.TabPage();
            this.ucImageQuality = new ShareX.ImageQualityUI();
            this.tabControl1.SuspendLayout();
            this.tpImageResize.SuspendLayout();
            this.tpImageQuality.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(541, 350);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(74, 22);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(461, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 22);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblGeneralInfo
            // 
            this.lblGeneralInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblGeneralInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGeneralInfo.ForeColor = System.Drawing.Color.White;
            this.lblGeneralInfo.Location = new System.Drawing.Point(0, 379);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(618, 29);
            this.lblGeneralInfo.TabIndex = 3;
            this.lblGeneralInfo.Text = "Disable this prompt by going to Settings > Configuration > Advanced";
            this.lblGeneralInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpActions);
            this.tabControl1.Controls.Add(this.tpImageResize);
            this.tabControl1.Controls.Add(this.tpImageQuality);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(618, 344);
            this.tabControl1.TabIndex = 0;
            // 
            // tpActions
            // 
            this.tpActions.Location = new System.Drawing.Point(4, 22);
            this.tpActions.Name = "tpActions";
            this.tpActions.Padding = new System.Windows.Forms.Padding(3);
            this.tpActions.Size = new System.Drawing.Size(610, 318);
            this.tpActions.TabIndex = 0;
            this.tpActions.Text = "Actions";
            this.tpActions.UseVisualStyleBackColor = true;
            // 
            // tpImageResize
            // 
            this.tpImageResize.Controls.Add(this.ucImageResize);
            this.tpImageResize.Location = new System.Drawing.Point(4, 22);
            this.tpImageResize.Name = "tpImageResize";
            this.tpImageResize.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageResize.Size = new System.Drawing.Size(610, 318);
            this.tpImageResize.TabIndex = 1;
            this.tpImageResize.Text = "Image Resize";
            this.tpImageResize.UseVisualStyleBackColor = true;
            // 
            // ucImageResize
            // 
            this.ucImageResize.Location = new System.Drawing.Point(8, 8);
            this.ucImageResize.Name = "ucImageResize";
            this.ucImageResize.Size = new System.Drawing.Size(440, 296);
            this.ucImageResize.TabIndex = 0;
            // 
            // tpImageQuality
            // 
            this.tpImageQuality.Controls.Add(this.ucImageQuality);
            this.tpImageQuality.Location = new System.Drawing.Point(4, 22);
            this.tpImageQuality.Name = "tpImageQuality";
            this.tpImageQuality.Size = new System.Drawing.Size(610, 318);
            this.tpImageQuality.TabIndex = 2;
            this.tpImageQuality.Text = "Image Quality";
            this.tpImageQuality.UseVisualStyleBackColor = true;
            // 
            // ucImageQuality
            // 
            this.ucImageQuality.Location = new System.Drawing.Point(8, 8);
            this.ucImageQuality.Name = "ucImageQuality";
            this.ucImageQuality.Size = new System.Drawing.Size(382, 222);
            this.ucImageQuality.TabIndex = 0;
            // 
            // WindowAfterCapture
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 408);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblGeneralInfo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(624, 432);
            this.Name = "WindowAfterCapture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "After capture...";
            this.tabControl1.ResumeLayout(false);
            this.tpImageResize.ResumeLayout(false);
            this.tpImageQuality.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblGeneralInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpActions;
        private System.Windows.Forms.TabPage tpImageResize;
        private ImageResizeUI ucImageResize;
        private System.Windows.Forms.TabPage tpImageQuality;
        private ImageQualityUI ucImageQuality;
    }
}