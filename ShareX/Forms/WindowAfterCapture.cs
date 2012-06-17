using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using ShareX.Properties;

namespace ShareX
{
    public partial class WindowAfterCapture : Form
    {
        public Subtask Config { get; set; }

        public WindowAfterCapture(Subtask config)
        {
            InitializeComponent();
            this.Icon = Resources.ShareX;

            Config = config;

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
                chkJob.Checked = Config.HasFlag(job.Enum);
                this.Controls.Add(chkJob);

                maxWidth = Math.Max(maxWidth, chkJob.Width);
                yGap += 24;
            }

            this.Width = Math.Max(400, maxWidth + btnOk.Width * 2);
            this.Height = yGap + 60;
        }

        private void chkJob_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkJob = sender as CheckBox;
            if (chkJob.Checked)
                Config |= (Subtask)chkJob.Tag;
            else
                Config &= ~(Subtask)chkJob.Tag;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}