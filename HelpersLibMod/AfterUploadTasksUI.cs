using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLibMod;

namespace HelpersLib.UserControls
{
    public partial class AfterUploadTasksUI : UserControl
    {
        public AfterUploadTasksUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Configure After Upload Tasks UI
        /// </summary>
        /// <param name="config">Initial config</param>
        /// <param name="chkAfterUploadTask_CheckedChanged">Event handler of the parent form</param>
        public void ConfigUI(AfterUploadTasks config, EventHandler chkAfterUploadTask_CheckedChanged)
        {
            var tasks = Enum.GetValues(typeof(AfterUploadTasks)).Cast<AfterUploadTasks>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            flpTasks.Controls.Clear();

            foreach (var job in tasks)
            {
                switch (job.Enum)
                {
                    case AfterUploadTasks.None:
                        continue;
                }

                CheckBox chkAfterCaptureTask = new CheckBox();
                chkAfterCaptureTask.Tag = job.Enum;
                chkAfterCaptureTask.Text = job.Description;
                chkAfterCaptureTask.AutoSize = true;
                chkAfterCaptureTask.CheckedChanged += chkAfterUploadTask_CheckedChanged;
                chkAfterCaptureTask.Checked = config.HasFlag(job.Enum);
                flpTasks.Controls.Add(chkAfterCaptureTask);
            }
        }
    }
}