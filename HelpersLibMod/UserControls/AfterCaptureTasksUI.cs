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
    public partial class AfterCaptureTasksUI : UserControl
    {
        public AfterCaptureTasksUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Configure After Capture Tasks UI
        /// </summary>
        /// <param name="config">Initial config</param>
        /// <param name="chkAfterCaptureTask_CheckedChanged">Event handler of the parent form</param>
        public void ConfigUI(Subtask config, EventHandler chkAfterCaptureTask_CheckedChanged)
        {
            var tasks = Enum.GetValues(typeof(Subtask)).Cast<Subtask>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            flpTasks.Controls.Clear();

            foreach (var job in tasks)
            {
                switch (job.Enum)
                {
                    case Subtask.None:
                        continue;
                }

                CheckBox chkAfterCaptureTask = new CheckBox();
                chkAfterCaptureTask.Tag = job.Enum;
                chkAfterCaptureTask.Text = job.Description;
                chkAfterCaptureTask.AutoSize = true;
                chkAfterCaptureTask.CheckedChanged += chkAfterCaptureTask_CheckedChanged;
                chkAfterCaptureTask.Checked = config.HasFlag(job.Enum);
                flpTasks.Controls.Add(chkAfterCaptureTask);
            }
        }
    }
}