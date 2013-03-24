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
            this.btnAbort = new System.Windows.Forms.Button();
            this.lblGeneralInfo = new System.Windows.Forms.Label();
            this.tcAfterCapture = new System.Windows.Forms.TabControl();
            this.tpActions = new System.Windows.Forms.TabPage();
            this.pbImage = new HelpersLib.MyPictureBox();
            this.tpImageResize = new System.Windows.Forms.TabPage();
            this.tpImageQuality = new System.Windows.Forms.TabPage();
            this.btnCopyImage = new System.Windows.Forms.Button();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.tpImageName = new System.Windows.Forms.TabPage();
            this.ucImageResize = new ShareX.ImageResizeUI();
            this.ucImageQuality = new ShareX.ImageQualityUI();
            this.tcAfterCapture.SuspendLayout();
            this.tpActions.SuspendLayout();
            this.tpImageResize.SuspendLayout();
            this.tpImageQuality.SuspendLayout();
            this.tpImageName.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(721, 422);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 27);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point(615, 422);
            this.btnAbort.Margin = new System.Windows.Forms.Padding(4);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(99, 27);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "&Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblGeneralInfo
            // 
            this.lblGeneralInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblGeneralInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGeneralInfo.ForeColor = System.Drawing.Color.White;
            this.lblGeneralInfo.Location = new System.Drawing.Point(0, 457);
            this.lblGeneralInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(824, 36);
            this.lblGeneralInfo.TabIndex = 4;
            this.lblGeneralInfo.Text = "Disable this prompt by going to Settings > Configuration > Advanced";
            this.lblGeneralInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tcAfterCapture
            // 
            this.tcAfterCapture.Controls.Add(this.tpActions);
            this.tcAfterCapture.Controls.Add(this.tpImageResize);
            this.tcAfterCapture.Controls.Add(this.tpImageQuality);
            this.tcAfterCapture.Controls.Add(this.tpImageName);
            this.tcAfterCapture.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcAfterCapture.Location = new System.Drawing.Point(0, 0);
            this.tcAfterCapture.Margin = new System.Windows.Forms.Padding(4);
            this.tcAfterCapture.Name = "tcAfterCapture";
            this.tcAfterCapture.SelectedIndex = 0;
            this.tcAfterCapture.Size = new System.Drawing.Size(824, 408);
            this.tcAfterCapture.TabIndex = 0;
            // 
            // tpActions
            // 
            this.tpActions.Controls.Add(this.pbImage);
            this.tpActions.Location = new System.Drawing.Point(4, 25);
            this.tpActions.Margin = new System.Windows.Forms.Padding(4);
            this.tpActions.Name = "tpActions";
            this.tpActions.Padding = new System.Windows.Forms.Padding(4);
            this.tpActions.Size = new System.Drawing.Size(816, 379);
            this.tpActions.TabIndex = 0;
            this.tpActions.Text = "Actions";
            this.tpActions.UseVisualStyleBackColor = true;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.White;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbImage.Location = new System.Drawing.Point(301, 4);
            this.pbImage.Margin = new System.Windows.Forms.Padding(5);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(511, 371);
            this.pbImage.TabIndex = 0;
            // 
            // tpImageResize
            // 
            this.tpImageResize.Controls.Add(this.ucImageResize);
            this.tpImageResize.Location = new System.Drawing.Point(4, 25);
            this.tpImageResize.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageResize.Name = "tpImageResize";
            this.tpImageResize.Padding = new System.Windows.Forms.Padding(4);
            this.tpImageResize.Size = new System.Drawing.Size(816, 379);
            this.tpImageResize.TabIndex = 1;
            this.tpImageResize.Text = "Image Resize";
            this.tpImageResize.UseVisualStyleBackColor = true;
            // 
            // tpImageQuality
            // 
            this.tpImageQuality.Controls.Add(this.ucImageQuality);
            this.tpImageQuality.Location = new System.Drawing.Point(4, 25);
            this.tpImageQuality.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageQuality.Name = "tpImageQuality";
            this.tpImageQuality.Size = new System.Drawing.Size(816, 379);
            this.tpImageQuality.TabIndex = 2;
            this.tpImageQuality.Text = "Image Quality";
            this.tpImageQuality.UseVisualStyleBackColor = true;
            // 
            // btnCopyImage
            // 
            this.btnCopyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyImage.AutoSize = true;
            this.btnCopyImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCopyImage.Location = new System.Drawing.Point(11, 424);
            this.btnCopyImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyImage.Name = "btnCopyImage";
            this.btnCopyImage.Size = new System.Drawing.Size(159, 27);
            this.btnCopyImage.TabIndex = 3;
            this.btnCopyImage.Text = "Copy &Image and Close";
            this.btnCopyImage.UseVisualStyleBackColor = true;
            this.btnCopyImage.Click += new System.EventHandler(this.btnCopyImage_Click);
            // 
            // txtImageName
            // 
            this.txtImageName.Location = new System.Drawing.Point(16, 16);
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.Size = new System.Drawing.Size(712, 22);
            this.txtImageName.TabIndex = 5;
            // 
            // tpImageName
            // 
            this.tpImageName.Controls.Add(this.txtImageName);
            this.tpImageName.Location = new System.Drawing.Point(4, 25);
            this.tpImageName.Name = "tpImageName";
            this.tpImageName.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageName.Size = new System.Drawing.Size(816, 379);
            this.tpImageName.TabIndex = 3;
            this.tpImageName.Text = "Image Name";
            this.tpImageName.UseVisualStyleBackColor = true;
            // 
            // ucImageResize
            // 
            this.ucImageResize.Location = new System.Drawing.Point(11, 10);
            this.ucImageResize.Margin = new System.Windows.Forms.Padding(5);
            this.ucImageResize.Name = "ucImageResize";
            this.ucImageResize.Size = new System.Drawing.Size(587, 364);
            this.ucImageResize.TabIndex = 0;
            // 
            // ucImageQuality
            // 
            this.ucImageQuality.Location = new System.Drawing.Point(11, 10);
            this.ucImageQuality.Margin = new System.Windows.Forms.Padding(5);
            this.ucImageQuality.Name = "ucImageQuality";
            this.ucImageQuality.Size = new System.Drawing.Size(509, 273);
            this.ucImageQuality.TabIndex = 0;
            // 
            // WindowAfterCapture
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 493);
            this.Controls.Add(this.btnCopyImage);
            this.Controls.Add(this.tcAfterCapture);
            this.Controls.Add(this.lblGeneralInfo);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(842, 540);
            this.Name = "WindowAfterCapture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "After capture...";
            this.tcAfterCapture.ResumeLayout(false);
            this.tpActions.ResumeLayout(false);
            this.tpImageResize.ResumeLayout(false);
            this.tpImageQuality.ResumeLayout(false);
            this.tpImageName.ResumeLayout(false);
            this.tpImageName.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label lblGeneralInfo;
        private System.Windows.Forms.TabControl tcAfterCapture;
        private System.Windows.Forms.TabPage tpActions;
        private System.Windows.Forms.TabPage tpImageResize;
        private ImageResizeUI ucImageResize;
        private System.Windows.Forms.TabPage tpImageQuality;
        private ImageQualityUI ucImageQuality;
        private System.Windows.Forms.Button btnCopyImage;
        private HelpersLib.MyPictureBox pbImage;
        private System.Windows.Forms.TabPage tpImageName;
        private System.Windows.Forms.TextBox txtImageName;
    }
}