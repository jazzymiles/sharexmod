using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using ShareX.SettingsHelpers;

namespace ShareX
{
    public partial class ImageQualityUI : UserControl
    {
        private UserConfig Config { get; set; }

        public ImageQualityUI()
        {
            InitializeComponent();
        }

        public void ConfigUI(UserConfig config)
        {
            Config = config;

            cbImageFormat.SelectedIndex = (int)Config.ImageFormat;
            nudImageJPEGQuality.Value = Config.ImageJPEGQuality;
            cbImageGIFQuality.SelectedIndex = (int)Config.ImageGIFQuality;
            nudUseImageFormat2After.Value = Config.ImageSizeLimit;
            cbImageFormat2.SelectedIndex = (int)Config.ImageFormat2;
        }

        private void cbImageFormat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ImageFormat2 = (EImageFormat)cbImageFormat2.SelectedIndex;
            UpdateGuiQuality();
        }

        private void nudImageJPEGQuality_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageJPEGQuality = (int)nudImageJPEGQuality.Value;
        }

        private void nudUseImageFormat2After_ValueChanged(object sender, EventArgs e)
        {
            Config.ImageSizeLimit = (int)nudUseImageFormat2After.Value;
        }

        private void cbImageGIFQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ImageGIFQuality = (GIFQuality)cbImageGIFQuality.SelectedIndex;
        }

        private void cbImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ImageFormat = (EImageFormat)cbImageFormat.SelectedIndex;
            UpdateGuiQuality();
        }

        private void UpdateGuiQuality()
        {
            cbImageFormat2.Enabled = nudUseImageFormat2After.Value > 0;

            tcQuality.TabPages.Clear();
            UpdateGuiQualityTabs(Config.ImageFormat);
            UpdateGuiQualityTabs(Config.ImageFormat2);
            tcQuality.Visible = tcQuality.TabPages.Count > 0;
        }

        private void UpdateGuiQualityTabs(EImageFormat format)
        {
            switch (format)
            {
                case EImageFormat.GIF:
                    if (!tcQuality.TabPages.Contains(tpQualityGif))
                        tcQuality.TabPages.Add(tpQualityGif);
                    break;
                case EImageFormat.JPEG:
                    if (!tcQuality.TabPages.Contains(tpQualityJpeg))
                        tcQuality.TabPages.Add(tpQualityJpeg);
                    break;
            }
        }
    }
}