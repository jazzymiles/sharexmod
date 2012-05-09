using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;

namespace ShareX
{
    public partial class WindowAfterCapture : Form
    {
        public TaskImageJob Config { get; set; }

        public WindowAfterCapture(TaskImageJob config)
        {
            InitializeComponent();

            Config = config;

            var taskImageJobs = Enum.GetValues(typeof(TaskImageJob)).Cast<TaskImageJob>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            int maxWidth = 0;
            int yGap = 8;

            foreach (var job in taskImageJobs)
            {
                switch (job.Enum)
                {
                    case TaskImageJob.None:
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

            this.Width = maxWidth + btnOk.Width;
            this.Height = yGap + 60;
        }

        private void chkJob_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkJob = sender as CheckBox;
            if (chkJob.Checked)
                Config |= (TaskImageJob)chkJob.Tag;
            else
                Config &= ~(TaskImageJob)chkJob.Tag;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}