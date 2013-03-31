namespace ShareX
{
    partial class ImageQualityUI
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
            this.tcQuality = new System.Windows.Forms.TabControl();
            this.tpQualityJpeg = new System.Windows.Forms.TabPage();
            this.nudImageJPEGQuality = new System.Windows.Forms.NumericUpDown();
            this.lblImageJPEGQuality = new System.Windows.Forms.Label();
            this.lblImageJPEGQualityHint = new System.Windows.Forms.Label();
            this.tpQualityGif = new System.Windows.Forms.TabPage();
            this.lblImageGIFQuality = new System.Windows.Forms.Label();
            this.cbImageGIFQuality = new System.Windows.Forms.ComboBox();
            this.lblImageFormat = new System.Windows.Forms.Label();
            this.lblUseImageFormat2AfterHint = new System.Windows.Forms.Label();
            this.cbImageFormat = new System.Windows.Forms.ComboBox();
            this.cbImageFormat2 = new System.Windows.Forms.ComboBox();
            this.lblImageFormat2 = new System.Windows.Forms.Label();
            this.nudUseImageFormat2After = new System.Windows.Forms.NumericUpDown();
            this.lblUseImageFormat2After = new System.Windows.Forms.Label();
            this.tcQuality.SuspendLayout();
            this.tpQualityJpeg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageJPEGQuality)).BeginInit();
            this.tpQualityGif.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUseImageFormat2After)).BeginInit();
            this.SuspendLayout();
            // 
            // tcQuality
            // 
            this.tcQuality.Controls.Add(this.tpQualityJpeg);
            this.tcQuality.Controls.Add(this.tpQualityGif);
            this.tcQuality.Location = new System.Drawing.Point(16, 72);
            this.tcQuality.Name = "tcQuality";
            this.tcQuality.SelectedIndex = 0;
            this.tcQuality.Size = new System.Drawing.Size(344, 136);
            this.tcQuality.TabIndex = 7;
            // 
            // tpQualityJpeg
            // 
            this.tpQualityJpeg.Controls.Add(this.nudImageJPEGQuality);
            this.tpQualityJpeg.Controls.Add(this.lblImageJPEGQuality);
            this.tpQualityJpeg.Controls.Add(this.lblImageJPEGQualityHint);
            this.tpQualityJpeg.Location = new System.Drawing.Point(4, 22);
            this.tpQualityJpeg.Name = "tpQualityJpeg";
            this.tpQualityJpeg.Padding = new System.Windows.Forms.Padding(3);
            this.tpQualityJpeg.Size = new System.Drawing.Size(256, 110);
            this.tpQualityJpeg.TabIndex = 0;
            this.tpQualityJpeg.Text = "JPEG";
            this.tpQualityJpeg.UseVisualStyleBackColor = true;
            // 
            // nudImageJPEGQuality
            // 
            this.nudImageJPEGQuality.Location = new System.Drawing.Point(96, 12);
            this.nudImageJPEGQuality.Name = "nudImageJPEGQuality";
            this.nudImageJPEGQuality.Size = new System.Drawing.Size(56, 20);
            this.nudImageJPEGQuality.TabIndex = 1;
            this.nudImageJPEGQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudImageJPEGQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageJPEGQuality.ValueChanged += new System.EventHandler(this.nudImageJPEGQuality_ValueChanged);
            // 
            // lblImageJPEGQuality
            // 
            this.lblImageJPEGQuality.AutoSize = true;
            this.lblImageJPEGQuality.Location = new System.Drawing.Point(8, 16);
            this.lblImageJPEGQuality.Name = "lblImageJPEGQuality";
            this.lblImageJPEGQuality.Size = new System.Drawing.Size(70, 13);
            this.lblImageJPEGQuality.TabIndex = 0;
            this.lblImageJPEGQuality.Text = "JPEG quality:";
            // 
            // lblImageJPEGQualityHint
            // 
            this.lblImageJPEGQualityHint.AutoSize = true;
            this.lblImageJPEGQualityHint.Location = new System.Drawing.Point(160, 16);
            this.lblImageJPEGQualityHint.Name = "lblImageJPEGQualityHint";
            this.lblImageJPEGQualityHint.Size = new System.Drawing.Size(40, 13);
            this.lblImageJPEGQualityHint.TabIndex = 2;
            this.lblImageJPEGQualityHint.Text = "0 - 100";
            // 
            // tpQualityGif
            // 
            this.tpQualityGif.Controls.Add(this.lblImageGIFQuality);
            this.tpQualityGif.Controls.Add(this.cbImageGIFQuality);
            this.tpQualityGif.Location = new System.Drawing.Point(4, 22);
            this.tpQualityGif.Name = "tpQualityGif";
            this.tpQualityGif.Padding = new System.Windows.Forms.Padding(3);
            this.tpQualityGif.Size = new System.Drawing.Size(336, 110);
            this.tpQualityGif.TabIndex = 1;
            this.tpQualityGif.Text = "GIF";
            this.tpQualityGif.UseVisualStyleBackColor = true;
            // 
            // lblImageGIFQuality
            // 
            this.lblImageGIFQuality.AutoSize = true;
            this.lblImageGIFQuality.Location = new System.Drawing.Point(8, 16);
            this.lblImageGIFQuality.Name = "lblImageGIFQuality";
            this.lblImageGIFQuality.Size = new System.Drawing.Size(60, 13);
            this.lblImageGIFQuality.TabIndex = 0;
            this.lblImageGIFQuality.Text = "GIF quality:";
            // 
            // cbImageGIFQuality
            // 
            this.cbImageGIFQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageGIFQuality.FormattingEnabled = true;
            this.cbImageGIFQuality.Items.AddRange(new object[] {
            "Default (Fast)",
            "256 colors (8 bit)",
            "16 colors (4 bit)",
            "Grayscale"});
            this.cbImageGIFQuality.Location = new System.Drawing.Point(96, 12);
            this.cbImageGIFQuality.Name = "cbImageGIFQuality";
            this.cbImageGIFQuality.Size = new System.Drawing.Size(120, 21);
            this.cbImageGIFQuality.TabIndex = 1;
            this.cbImageGIFQuality.SelectedIndexChanged += new System.EventHandler(this.cbImageGIFQuality_SelectedIndexChanged);
            // 
            // lblImageFormat
            // 
            this.lblImageFormat.AutoSize = true;
            this.lblImageFormat.Location = new System.Drawing.Point(16, 40);
            this.lblImageFormat.Name = "lblImageFormat";
            this.lblImageFormat.Size = new System.Drawing.Size(71, 13);
            this.lblImageFormat.TabIndex = 2;
            this.lblImageFormat.Text = "Image format:";
            // 
            // lblUseImageFormat2AfterHint
            // 
            this.lblUseImageFormat2AfterHint.AutoSize = true;
            this.lblUseImageFormat2AfterHint.Location = new System.Drawing.Point(256, 40);
            this.lblUseImageFormat2AfterHint.Name = "lblUseImageFormat2AfterHint";
            this.lblUseImageFormat2AfterHint.Size = new System.Drawing.Size(23, 13);
            this.lblUseImageFormat2AfterHint.TabIndex = 5;
            this.lblUseImageFormat2AfterHint.Text = "KiB";
            // 
            // cbImageFormat
            // 
            this.cbImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFormat.FormattingEnabled = true;
            this.cbImageFormat.Items.AddRange(new object[] {
            "PNG",
            "JPEG",
            "GIF",
            "BMP",
            "TIFF"});
            this.cbImageFormat.Location = new System.Drawing.Point(104, 36);
            this.cbImageFormat.Name = "cbImageFormat";
            this.cbImageFormat.Size = new System.Drawing.Size(56, 21);
            this.cbImageFormat.TabIndex = 3;
            this.cbImageFormat.SelectedIndexChanged += new System.EventHandler(this.cbImageFormat_SelectedIndexChanged);
            // 
            // cbImageFormat2
            // 
            this.cbImageFormat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFormat2.FormattingEnabled = true;
            this.cbImageFormat2.Items.AddRange(new object[] {
            "PNG",
            "JPEG",
            "GIF",
            "BMP",
            "TIFF"});
            this.cbImageFormat2.Location = new System.Drawing.Point(304, 36);
            this.cbImageFormat2.Name = "cbImageFormat2";
            this.cbImageFormat2.Size = new System.Drawing.Size(56, 21);
            this.cbImageFormat2.TabIndex = 6;
            this.cbImageFormat2.SelectedIndexChanged += new System.EventHandler(this.cbImageFormat2_SelectedIndexChanged);
            // 
            // lblImageFormat2
            // 
            this.lblImageFormat2.AutoSize = true;
            this.lblImageFormat2.Location = new System.Drawing.Point(304, 16);
            this.lblImageFormat2.Name = "lblImageFormat2";
            this.lblImageFormat2.Size = new System.Drawing.Size(49, 13);
            this.lblImageFormat2.TabIndex = 1;
            this.lblImageFormat2.Text = "switch to";
            // 
            // nudUseImageFormat2After
            // 
            this.nudUseImageFormat2After.Location = new System.Drawing.Point(192, 37);
            this.nudUseImageFormat2After.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudUseImageFormat2After.Name = "nudUseImageFormat2After";
            this.nudUseImageFormat2After.Size = new System.Drawing.Size(56, 20);
            this.nudUseImageFormat2After.TabIndex = 4;
            this.nudUseImageFormat2After.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudUseImageFormat2After.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudUseImageFormat2After.ValueChanged += new System.EventHandler(this.nudUseImageFormat2After_ValueChanged);
            // 
            // lblUseImageFormat2After
            // 
            this.lblUseImageFormat2After.AutoSize = true;
            this.lblUseImageFormat2After.Location = new System.Drawing.Point(192, 16);
            this.lblUseImageFormat2After.Name = "lblUseImageFormat2After";
            this.lblUseImageFormat2After.Size = new System.Drawing.Size(84, 13);
            this.lblUseImageFormat2After.TabIndex = 0;
            this.lblUseImageFormat2After.Text = "after (0 disables)";
            // 
            // ImageQualityUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcQuality);
            this.Controls.Add(this.lblImageFormat);
            this.Controls.Add(this.lblUseImageFormat2AfterHint);
            this.Controls.Add(this.cbImageFormat);
            this.Controls.Add(this.cbImageFormat2);
            this.Controls.Add(this.lblImageFormat2);
            this.Controls.Add(this.nudUseImageFormat2After);
            this.Controls.Add(this.lblUseImageFormat2After);
            this.Name = "ImageQualityUI";
            this.Size = new System.Drawing.Size(382, 222);
            this.tcQuality.ResumeLayout(false);
            this.tpQualityJpeg.ResumeLayout(false);
            this.tpQualityJpeg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageJPEGQuality)).EndInit();
            this.tpQualityGif.ResumeLayout(false);
            this.tpQualityGif.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUseImageFormat2After)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcQuality;
        private System.Windows.Forms.TabPage tpQualityJpeg;
        private System.Windows.Forms.NumericUpDown nudImageJPEGQuality;
        private System.Windows.Forms.Label lblImageJPEGQuality;
        private System.Windows.Forms.Label lblImageJPEGQualityHint;
        private System.Windows.Forms.TabPage tpQualityGif;
        private System.Windows.Forms.Label lblImageGIFQuality;
        private System.Windows.Forms.ComboBox cbImageGIFQuality;
        private System.Windows.Forms.Label lblImageFormat;
        private System.Windows.Forms.Label lblUseImageFormat2AfterHint;
        private System.Windows.Forms.ComboBox cbImageFormat;
        private System.Windows.Forms.ComboBox cbImageFormat2;
        private System.Windows.Forms.Label lblImageFormat2;
        private System.Windows.Forms.NumericUpDown nudUseImageFormat2After;
        private System.Windows.Forms.Label lblUseImageFormat2After;
    }
}
