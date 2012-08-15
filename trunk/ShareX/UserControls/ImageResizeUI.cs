using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShareX.SettingsHelpers;

namespace ShareX
{
    public partial class ImageResizeUI : UserControl
    {
        private UserConfig Config { get; set; }

        public ImageResizeUI()
        {
            InitializeComponent();
        }

        public void ConfigUI(UserConfig config)
        {
            Config = config;

            cbImageAutoResize.Checked = Config.ImageAutoResize;
            cbImageKeepAspectRatio.Checked = Config.ImageKeepAspectRatio;
            cbImageUseSmoothScaling.Checked = Config.ImageUseSmoothScaling;

            switch (Config.ImageScaleType)
            {
                case ImageScaleType.Percentage:
                    rbImageScaleTypePercentage.Checked = true;
                    break;
                case ImageScaleType.Width:
                    rbImageScaleTypeToWidth.Checked = true;
                    break;
                case ImageScaleType.Height:
                    rbImageScaleTypeToHeight.Checked = true;
                    break;
                case ImageScaleType.Specific:
                    rbImageScaleTypeSpecific.Checked = true;
                    break;
            }

            nudImageScalePercentageWidth.Value = Config.ImageScalePercentageWidth;
            nudImageScalePercentageHeight.Value = Config.ImageScalePercentageHeight;
            nudImageScaleToWidth.Value = Config.ImageScaleToWidth;
            nudImageScaleToHeight.Value = Config.ImageScaleToHeight;
            nudImageScaleSpecificWidth.Value = Config.ImageScaleSpecificWidth;
            nudImageScaleSpecificHeight.Value = Config.ImageScaleSpecificHeight;
        }

        private void nudImageScaleToWidth_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageScaleToWidth = (int)nudImageScaleToWidth.Value;
        }

        private void cbImageKeepAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            Config.ImageKeepAspectRatio = cbImageKeepAspectRatio.Checked;

            if (Config.ImageKeepAspectRatio)
            {
                nudImageScalePercentageHeight.Value = nudImageScalePercentageWidth.Value;
            }
        }

        private void rbImageScaleTypeToHeight_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void cbImageUseSmoothScaling_CheckedChanged(object sender, EventArgs e)
        {
            Config.ImageUseSmoothScaling = cbImageUseSmoothScaling.Checked;
        }

        private void rbImageScaleTypeToWidth_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void nudImageScalePercentageHeight_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageScalePercentageHeight = (int)nudImageScalePercentageHeight.Value;

            if (Config.ImageKeepAspectRatio)
            {
                nudImageScalePercentageWidth.Value = Config.ImageScalePercentageHeight;
            }
        }

        private void nudImageScalePercentageWidth_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageScalePercentageWidth = (int)nudImageScalePercentageWidth.Value;

            if (Config.ImageKeepAspectRatio)
            {
                nudImageScalePercentageHeight.Value = Config.ImageScalePercentageWidth;
            }
        }

        private void cbImageAutoResize_CheckedChanged(object sender, EventArgs e)
        {
            Config.ImageAutoResize = cbImageAutoResize.Checked;
        }

        private void nudImageScaleSpecificHeight_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageScaleSpecificHeight = (int)nudImageScaleSpecificHeight.Value;
        }

        private void nudImageScaleSpecificWidth_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageScaleSpecificWidth = (int)nudImageScaleSpecificWidth.Value;
        }

        private void rbImageScaleTypePercentage_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void rbImageScaleTypeSpecific_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void nudImageScaleToHeight_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageScaleToHeight = (int)nudImageScaleToHeight.Value;
        }

        private void CheckImageScaleType()
        {
            bool aspectRatioEnabled = true;

            if (rbImageScaleTypePercentage.Checked)
            {
                Config.ImageScaleType = ImageScaleType.Percentage;
            }
            else if (rbImageScaleTypeToWidth.Checked)
            {
                Config.ImageScaleType = ImageScaleType.Width;
            }
            else if (rbImageScaleTypeToHeight.Checked)
            {
                Config.ImageScaleType = ImageScaleType.Height;
            }
            else if (rbImageScaleTypeSpecific.Checked)
            {
                Config.ImageScaleType = ImageScaleType.Specific;
                aspectRatioEnabled = false;
            }

            cbImageKeepAspectRatio.Enabled = aspectRatioEnabled;
        }
    }
}