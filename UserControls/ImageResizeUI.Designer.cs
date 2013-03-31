namespace ShareX
{
    partial class ImageResizeUI
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
            this.cbImageUseSmoothScaling = new System.Windows.Forms.CheckBox();
            this.cbImageKeepAspectRatio = new System.Windows.Forms.CheckBox();
            this.cbImageAutoResize = new System.Windows.Forms.CheckBox();
            this.gbImageScaleSettings = new System.Windows.Forms.GroupBox();
            this.rbImageScaleTypePercentage = new System.Windows.Forms.RadioButton();
            this.lblImageScaleToHeight2 = new System.Windows.Forms.Label();
            this.rbImageScaleTypeToWidth = new System.Windows.Forms.RadioButton();
            this.lblImageScaleSpecificWidth2 = new System.Windows.Forms.Label();
            this.rbImageScaleTypeToHeight = new System.Windows.Forms.RadioButton();
            this.lblImageScaleSpecificHeight2 = new System.Windows.Forms.Label();
            this.rbImageScaleTypeSpecific = new System.Windows.Forms.RadioButton();
            this.lblImageScaleToWidth2 = new System.Windows.Forms.Label();
            this.lblImageScalePercentageWidth = new System.Windows.Forms.Label();
            this.lblImageScalePercentageHeight2 = new System.Windows.Forms.Label();
            this.nudImageScalePercentageWidth = new System.Windows.Forms.NumericUpDown();
            this.lblImageScalePercentageWidth2 = new System.Windows.Forms.Label();
            this.lblImageScalePercentageHeight = new System.Windows.Forms.Label();
            this.nudImageScaleToHeight = new System.Windows.Forms.NumericUpDown();
            this.nudImageScalePercentageHeight = new System.Windows.Forms.NumericUpDown();
            this.nudImageScaleToWidth = new System.Windows.Forms.NumericUpDown();
            this.lblImageScaleToWidth = new System.Windows.Forms.Label();
            this.nudImageScaleSpecificHeight = new System.Windows.Forms.NumericUpDown();
            this.lblImageScaleToHeight = new System.Windows.Forms.Label();
            this.lblImageScaleSpecificHeight = new System.Windows.Forms.Label();
            this.lblImageScaleSpecificWidth = new System.Windows.Forms.Label();
            this.nudImageScaleSpecificWidth = new System.Windows.Forms.NumericUpDown();
            this.gbImageScaleSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScalePercentageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleToHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScalePercentageHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleToWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleSpecificHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleSpecificWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // cbImageUseSmoothScaling
            // 
            this.cbImageUseSmoothScaling.AutoSize = true;
            this.cbImageUseSmoothScaling.Location = new System.Drawing.Point(8, 56);
            this.cbImageUseSmoothScaling.Name = "cbImageUseSmoothScaling";
            this.cbImageUseSmoothScaling.Size = new System.Drawing.Size(183, 17);
            this.cbImageUseSmoothScaling.TabIndex = 2;
            this.cbImageUseSmoothScaling.Text = "Use smooth scaling (Anti aliasing)";
            this.cbImageUseSmoothScaling.UseVisualStyleBackColor = true;
            this.cbImageUseSmoothScaling.CheckedChanged += new System.EventHandler(this.cbImageUseSmoothScaling_CheckedChanged);
            // 
            // cbImageKeepAspectRatio
            // 
            this.cbImageKeepAspectRatio.AutoSize = true;
            this.cbImageKeepAspectRatio.Location = new System.Drawing.Point(8, 32);
            this.cbImageKeepAspectRatio.Name = "cbImageKeepAspectRatio";
            this.cbImageKeepAspectRatio.Size = new System.Drawing.Size(109, 17);
            this.cbImageKeepAspectRatio.TabIndex = 1;
            this.cbImageKeepAspectRatio.Text = "Keep aspect ratio";
            this.cbImageKeepAspectRatio.UseVisualStyleBackColor = true;
            this.cbImageKeepAspectRatio.CheckedChanged += new System.EventHandler(this.cbImageKeepAspectRatio_CheckedChanged);
            // 
            // cbImageAutoResize
            // 
            this.cbImageAutoResize.AutoSize = true;
            this.cbImageAutoResize.Location = new System.Drawing.Point(8, 8);
            this.cbImageAutoResize.Name = "cbImageAutoResize";
            this.cbImageAutoResize.Size = new System.Drawing.Size(149, 17);
            this.cbImageAutoResize.TabIndex = 0;
            this.cbImageAutoResize.Text = "Automatically resize image";
            this.cbImageAutoResize.UseVisualStyleBackColor = true;
            this.cbImageAutoResize.CheckedChanged += new System.EventHandler(this.cbImageAutoResize_CheckedChanged);
            // 
            // gbImageScaleSettings
            // 
            this.gbImageScaleSettings.Controls.Add(this.rbImageScaleTypePercentage);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleToHeight2);
            this.gbImageScaleSettings.Controls.Add(this.rbImageScaleTypeToWidth);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleSpecificWidth2);
            this.gbImageScaleSettings.Controls.Add(this.rbImageScaleTypeToHeight);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleSpecificHeight2);
            this.gbImageScaleSettings.Controls.Add(this.rbImageScaleTypeSpecific);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleToWidth2);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScalePercentageWidth);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScalePercentageHeight2);
            this.gbImageScaleSettings.Controls.Add(this.nudImageScalePercentageWidth);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScalePercentageWidth2);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScalePercentageHeight);
            this.gbImageScaleSettings.Controls.Add(this.nudImageScaleToHeight);
            this.gbImageScaleSettings.Controls.Add(this.nudImageScalePercentageHeight);
            this.gbImageScaleSettings.Controls.Add(this.nudImageScaleToWidth);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleToWidth);
            this.gbImageScaleSettings.Controls.Add(this.nudImageScaleSpecificHeight);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleToHeight);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleSpecificHeight);
            this.gbImageScaleSettings.Controls.Add(this.lblImageScaleSpecificWidth);
            this.gbImageScaleSettings.Controls.Add(this.nudImageScaleSpecificWidth);
            this.gbImageScaleSettings.Location = new System.Drawing.Point(224, 8);
            this.gbImageScaleSettings.Name = "gbImageScaleSettings";
            this.gbImageScaleSettings.Size = new System.Drawing.Size(200, 272);
            this.gbImageScaleSettings.TabIndex = 3;
            this.gbImageScaleSettings.TabStop = false;
            this.gbImageScaleSettings.Text = "Scale settings";
            // 
            // rbImageScaleTypePercentage
            // 
            this.rbImageScaleTypePercentage.AutoSize = true;
            this.rbImageScaleTypePercentage.Location = new System.Drawing.Point(16, 24);
            this.rbImageScaleTypePercentage.Name = "rbImageScaleTypePercentage";
            this.rbImageScaleTypePercentage.Size = new System.Drawing.Size(123, 17);
            this.rbImageScaleTypePercentage.TabIndex = 0;
            this.rbImageScaleTypePercentage.TabStop = true;
            this.rbImageScaleTypePercentage.Text = "Scale by percentage";
            this.rbImageScaleTypePercentage.UseVisualStyleBackColor = true;
            this.rbImageScaleTypePercentage.CheckedChanged += new System.EventHandler(this.rbImageScaleTypePercentage_CheckedChanged);
            // 
            // lblImageScaleToHeight2
            // 
            this.lblImageScaleToHeight2.AutoSize = true;
            this.lblImageScaleToHeight2.Location = new System.Drawing.Point(144, 172);
            this.lblImageScaleToHeight2.Name = "lblImageScaleToHeight2";
            this.lblImageScaleToHeight2.Size = new System.Drawing.Size(33, 13);
            this.lblImageScaleToHeight2.TabIndex = 14;
            this.lblImageScaleToHeight2.Text = "pixels";
            // 
            // rbImageScaleTypeToWidth
            // 
            this.rbImageScaleTypeToWidth.AutoSize = true;
            this.rbImageScaleTypeToWidth.Location = new System.Drawing.Point(16, 96);
            this.rbImageScaleTypeToWidth.Name = "rbImageScaleTypeToWidth";
            this.rbImageScaleTypeToWidth.Size = new System.Drawing.Size(92, 17);
            this.rbImageScaleTypeToWidth.TabIndex = 7;
            this.rbImageScaleTypeToWidth.TabStop = true;
            this.rbImageScaleTypeToWidth.Text = "Scale to width";
            this.rbImageScaleTypeToWidth.UseVisualStyleBackColor = true;
            this.rbImageScaleTypeToWidth.CheckedChanged += new System.EventHandler(this.rbImageScaleTypeToWidth_CheckedChanged);
            // 
            // lblImageScaleSpecificWidth2
            // 
            this.lblImageScaleSpecificWidth2.AutoSize = true;
            this.lblImageScaleSpecificWidth2.Location = new System.Drawing.Point(144, 220);
            this.lblImageScaleSpecificWidth2.Name = "lblImageScaleSpecificWidth2";
            this.lblImageScaleSpecificWidth2.Size = new System.Drawing.Size(33, 13);
            this.lblImageScaleSpecificWidth2.TabIndex = 18;
            this.lblImageScaleSpecificWidth2.Text = "pixels";
            // 
            // rbImageScaleTypeToHeight
            // 
            this.rbImageScaleTypeToHeight.AutoSize = true;
            this.rbImageScaleTypeToHeight.Location = new System.Drawing.Point(16, 144);
            this.rbImageScaleTypeToHeight.Name = "rbImageScaleTypeToHeight";
            this.rbImageScaleTypeToHeight.Size = new System.Drawing.Size(96, 17);
            this.rbImageScaleTypeToHeight.TabIndex = 11;
            this.rbImageScaleTypeToHeight.TabStop = true;
            this.rbImageScaleTypeToHeight.Text = "Scale to height";
            this.rbImageScaleTypeToHeight.UseVisualStyleBackColor = true;
            this.rbImageScaleTypeToHeight.CheckedChanged += new System.EventHandler(this.rbImageScaleTypeToHeight_CheckedChanged);
            // 
            // lblImageScaleSpecificHeight2
            // 
            this.lblImageScaleSpecificHeight2.AutoSize = true;
            this.lblImageScaleSpecificHeight2.Location = new System.Drawing.Point(144, 244);
            this.lblImageScaleSpecificHeight2.Name = "lblImageScaleSpecificHeight2";
            this.lblImageScaleSpecificHeight2.Size = new System.Drawing.Size(33, 13);
            this.lblImageScaleSpecificHeight2.TabIndex = 21;
            this.lblImageScaleSpecificHeight2.Text = "pixels";
            // 
            // rbImageScaleTypeSpecific
            // 
            this.rbImageScaleTypeSpecific.AutoSize = true;
            this.rbImageScaleTypeSpecific.Location = new System.Drawing.Point(16, 192);
            this.rbImageScaleTypeSpecific.Name = "rbImageScaleTypeSpecific";
            this.rbImageScaleTypeSpecific.Size = new System.Drawing.Size(84, 17);
            this.rbImageScaleTypeSpecific.TabIndex = 15;
            this.rbImageScaleTypeSpecific.TabStop = true;
            this.rbImageScaleTypeSpecific.Text = "Specific size";
            this.rbImageScaleTypeSpecific.UseVisualStyleBackColor = true;
            this.rbImageScaleTypeSpecific.CheckedChanged += new System.EventHandler(this.rbImageScaleTypeSpecific_CheckedChanged);
            // 
            // lblImageScaleToWidth2
            // 
            this.lblImageScaleToWidth2.AutoSize = true;
            this.lblImageScaleToWidth2.Location = new System.Drawing.Point(144, 124);
            this.lblImageScaleToWidth2.Name = "lblImageScaleToWidth2";
            this.lblImageScaleToWidth2.Size = new System.Drawing.Size(33, 13);
            this.lblImageScaleToWidth2.TabIndex = 10;
            this.lblImageScaleToWidth2.Text = "pixels";
            // 
            // lblImageScalePercentageWidth
            // 
            this.lblImageScalePercentageWidth.AutoSize = true;
            this.lblImageScalePercentageWidth.Location = new System.Drawing.Point(32, 52);
            this.lblImageScalePercentageWidth.Name = "lblImageScalePercentageWidth";
            this.lblImageScalePercentageWidth.Size = new System.Drawing.Size(38, 13);
            this.lblImageScalePercentageWidth.TabIndex = 1;
            this.lblImageScalePercentageWidth.Text = "Width:";
            // 
            // lblImageScalePercentageHeight2
            // 
            this.lblImageScalePercentageHeight2.AutoSize = true;
            this.lblImageScalePercentageHeight2.Location = new System.Drawing.Point(144, 76);
            this.lblImageScalePercentageHeight2.Name = "lblImageScalePercentageHeight2";
            this.lblImageScalePercentageHeight2.Size = new System.Drawing.Size(15, 13);
            this.lblImageScalePercentageHeight2.TabIndex = 6;
            this.lblImageScalePercentageHeight2.Text = "%";
            // 
            // nudImageScalePercentageWidth
            // 
            this.nudImageScalePercentageWidth.Location = new System.Drawing.Point(80, 48);
            this.nudImageScalePercentageWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudImageScalePercentageWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageScalePercentageWidth.Name = "nudImageScalePercentageWidth";
            this.nudImageScalePercentageWidth.Size = new System.Drawing.Size(56, 20);
            this.nudImageScalePercentageWidth.TabIndex = 2;
            this.nudImageScalePercentageWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageScalePercentageWidth.ValueChanged += new System.EventHandler(this.nudImageScalePercentageWidth_ValueChanged);
            // 
            // lblImageScalePercentageWidth2
            // 
            this.lblImageScalePercentageWidth2.AutoSize = true;
            this.lblImageScalePercentageWidth2.Location = new System.Drawing.Point(144, 52);
            this.lblImageScalePercentageWidth2.Name = "lblImageScalePercentageWidth2";
            this.lblImageScalePercentageWidth2.Size = new System.Drawing.Size(15, 13);
            this.lblImageScalePercentageWidth2.TabIndex = 3;
            this.lblImageScalePercentageWidth2.Text = "%";
            // 
            // lblImageScalePercentageHeight
            // 
            this.lblImageScalePercentageHeight.AutoSize = true;
            this.lblImageScalePercentageHeight.Location = new System.Drawing.Point(32, 76);
            this.lblImageScalePercentageHeight.Name = "lblImageScalePercentageHeight";
            this.lblImageScalePercentageHeight.Size = new System.Drawing.Size(41, 13);
            this.lblImageScalePercentageHeight.TabIndex = 4;
            this.lblImageScalePercentageHeight.Text = "Height:";
            // 
            // nudImageScaleToHeight
            // 
            this.nudImageScaleToHeight.Location = new System.Drawing.Point(80, 168);
            this.nudImageScaleToHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudImageScaleToHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageScaleToHeight.Name = "nudImageScaleToHeight";
            this.nudImageScaleToHeight.Size = new System.Drawing.Size(56, 20);
            this.nudImageScaleToHeight.TabIndex = 13;
            this.nudImageScaleToHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageScaleToHeight.ValueChanged += new System.EventHandler(this.nudImageScaleToHeight_ValueChanged);
            // 
            // nudImageScalePercentageHeight
            // 
            this.nudImageScalePercentageHeight.Location = new System.Drawing.Point(80, 72);
            this.nudImageScalePercentageHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudImageScalePercentageHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageScalePercentageHeight.Name = "nudImageScalePercentageHeight";
            this.nudImageScalePercentageHeight.Size = new System.Drawing.Size(56, 20);
            this.nudImageScalePercentageHeight.TabIndex = 5;
            this.nudImageScalePercentageHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageScalePercentageHeight.ValueChanged += new System.EventHandler(this.nudImageScalePercentageHeight_ValueChanged);
            // 
            // nudImageScaleToWidth
            // 
            this.nudImageScaleToWidth.Location = new System.Drawing.Point(80, 120);
            this.nudImageScaleToWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudImageScaleToWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageScaleToWidth.Name = "nudImageScaleToWidth";
            this.nudImageScaleToWidth.Size = new System.Drawing.Size(56, 20);
            this.nudImageScaleToWidth.TabIndex = 9;
            this.nudImageScaleToWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageScaleToWidth.ValueChanged += new System.EventHandler(this.nudImageScaleToWidth_ValueChanged);
            // 
            // lblImageScaleToWidth
            // 
            this.lblImageScaleToWidth.AutoSize = true;
            this.lblImageScaleToWidth.Location = new System.Drawing.Point(32, 124);
            this.lblImageScaleToWidth.Name = "lblImageScaleToWidth";
            this.lblImageScaleToWidth.Size = new System.Drawing.Size(38, 13);
            this.lblImageScaleToWidth.TabIndex = 8;
            this.lblImageScaleToWidth.Text = "Width:";
            // 
            // nudImageScaleSpecificHeight
            // 
            this.nudImageScaleSpecificHeight.Location = new System.Drawing.Point(80, 240);
            this.nudImageScaleSpecificHeight.Name = "nudImageScaleSpecificHeight";
            this.nudImageScaleSpecificHeight.Size = new System.Drawing.Size(56, 20);
            this.nudImageScaleSpecificHeight.TabIndex = 20;
            this.nudImageScaleSpecificHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageScaleSpecificHeight.ValueChanged += new System.EventHandler(this.nudImageScaleSpecificHeight_ValueChanged);
            // 
            // lblImageScaleToHeight
            // 
            this.lblImageScaleToHeight.AutoSize = true;
            this.lblImageScaleToHeight.Location = new System.Drawing.Point(32, 172);
            this.lblImageScaleToHeight.Name = "lblImageScaleToHeight";
            this.lblImageScaleToHeight.Size = new System.Drawing.Size(41, 13);
            this.lblImageScaleToHeight.TabIndex = 12;
            this.lblImageScaleToHeight.Text = "Height:";
            // 
            // lblImageScaleSpecificHeight
            // 
            this.lblImageScaleSpecificHeight.AutoSize = true;
            this.lblImageScaleSpecificHeight.Location = new System.Drawing.Point(32, 244);
            this.lblImageScaleSpecificHeight.Name = "lblImageScaleSpecificHeight";
            this.lblImageScaleSpecificHeight.Size = new System.Drawing.Size(41, 13);
            this.lblImageScaleSpecificHeight.TabIndex = 19;
            this.lblImageScaleSpecificHeight.Text = "Height:";
            // 
            // lblImageScaleSpecificWidth
            // 
            this.lblImageScaleSpecificWidth.AutoSize = true;
            this.lblImageScaleSpecificWidth.Location = new System.Drawing.Point(32, 220);
            this.lblImageScaleSpecificWidth.Name = "lblImageScaleSpecificWidth";
            this.lblImageScaleSpecificWidth.Size = new System.Drawing.Size(38, 13);
            this.lblImageScaleSpecificWidth.TabIndex = 16;
            this.lblImageScaleSpecificWidth.Text = "Width:";
            // 
            // nudImageScaleSpecificWidth
            // 
            this.nudImageScaleSpecificWidth.Location = new System.Drawing.Point(80, 216);
            this.nudImageScaleSpecificWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudImageScaleSpecificWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageScaleSpecificWidth.Name = "nudImageScaleSpecificWidth";
            this.nudImageScaleSpecificWidth.Size = new System.Drawing.Size(56, 20);
            this.nudImageScaleSpecificWidth.TabIndex = 17;
            this.nudImageScaleSpecificWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageScaleSpecificWidth.ValueChanged += new System.EventHandler(this.nudImageScaleSpecificWidth_ValueChanged);
            // 
            // ImageResizeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbImageUseSmoothScaling);
            this.Controls.Add(this.cbImageKeepAspectRatio);
            this.Controls.Add(this.cbImageAutoResize);
            this.Controls.Add(this.gbImageScaleSettings);
            this.Name = "ImageResizeUI";
            this.Size = new System.Drawing.Size(440, 293);
            this.gbImageScaleSettings.ResumeLayout(false);
            this.gbImageScaleSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScalePercentageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleToHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScalePercentageHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleToWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleSpecificHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageScaleSpecificWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbImageUseSmoothScaling;
        private System.Windows.Forms.CheckBox cbImageKeepAspectRatio;
        private System.Windows.Forms.CheckBox cbImageAutoResize;
        private System.Windows.Forms.GroupBox gbImageScaleSettings;
        private System.Windows.Forms.RadioButton rbImageScaleTypePercentage;
        private System.Windows.Forms.Label lblImageScaleToHeight2;
        private System.Windows.Forms.RadioButton rbImageScaleTypeToWidth;
        private System.Windows.Forms.Label lblImageScaleSpecificWidth2;
        private System.Windows.Forms.RadioButton rbImageScaleTypeToHeight;
        private System.Windows.Forms.Label lblImageScaleSpecificHeight2;
        private System.Windows.Forms.RadioButton rbImageScaleTypeSpecific;
        private System.Windows.Forms.Label lblImageScaleToWidth2;
        private System.Windows.Forms.Label lblImageScalePercentageWidth;
        private System.Windows.Forms.Label lblImageScalePercentageHeight2;
        private System.Windows.Forms.NumericUpDown nudImageScalePercentageWidth;
        private System.Windows.Forms.Label lblImageScalePercentageWidth2;
        private System.Windows.Forms.Label lblImageScalePercentageHeight;
        private System.Windows.Forms.NumericUpDown nudImageScaleToHeight;
        private System.Windows.Forms.NumericUpDown nudImageScalePercentageHeight;
        private System.Windows.Forms.NumericUpDown nudImageScaleToWidth;
        private System.Windows.Forms.Label lblImageScaleToWidth;
        private System.Windows.Forms.NumericUpDown nudImageScaleSpecificHeight;
        private System.Windows.Forms.Label lblImageScaleToHeight;
        private System.Windows.Forms.Label lblImageScaleSpecificHeight;
        private System.Windows.Forms.Label lblImageScaleSpecificWidth;
        private System.Windows.Forms.NumericUpDown nudImageScaleSpecificWidth;
    }
}
