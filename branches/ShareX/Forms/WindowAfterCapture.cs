using HelpersLib;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using ShareX.HelperClasses;
using ShareX.Properties;
using ShareX.SettingsHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX
{
    public partial class WindowAfterCapture : Form
    {
        public Subtask ConfigSubtasks { get; set; }

        public UserConfig ConfigUser { get; set; }

        private ImageData _imgdata = null;

        public WindowAfterCapture(ImageData imgData, Subtask config)
        {
            InitializeComponent();
            _imgdata = imgData;

            this.Icon = Resources.ShareX;
            this.pbImage.LoadImage(_imgdata.Image);
            this.txtImageName.Text = _imgdata.Filename;

            ConfigSubtasks = config;

            var tasks = Enum.GetValues(typeof(Subtask)).Cast<Subtask>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            int maxWidth = 0;
            int yGap = 8;

            foreach (var job in tasks)
            {
                switch (job.Enum)
                {
                    case Subtask.None:
                        continue;
                }

                CheckBox chkJob = new CheckBox();
                chkJob.Tag = job.Enum;
                chkJob.Text = job.Description;
                chkJob.AutoSize = true;
                chkJob.Location = new Point(8, yGap);
                chkJob.CheckedChanged += new EventHandler(chkJob_CheckedChanged);
                chkJob.Checked = ConfigSubtasks.HasFlag(job.Enum);
                this.tpActions.Controls.Add(chkJob);

                maxWidth = Math.Max(maxWidth, chkJob.Width);
                yGap += chkJob.Height + 8;
            }

            this.Width = Math.Max(400, maxWidth + btnOk.Width * 2);
            this.tcAfterCapture.Height = yGap;
            this.Height = yGap + 8;

            ConfigUser = Helpers.Clone(SettingsManager.ConfigUser) as UserConfig;

            ucImageResize.ConfigUI(ConfigUser);
            ucImageQuality.ConfigUI(ConfigUser);
        }

        private void chkJob_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkJob = sender as CheckBox;
            if (chkJob.Checked)
                ConfigSubtasks |= (Subtask)chkJob.Tag;
            else
                ConfigSubtasks &= ~(Subtask)chkJob.Tag;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtImageName.Text))
                _imgdata.UserText = txtImageName.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void btnCopyImage_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}