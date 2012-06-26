using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpersLib.UserControls
{
    public partial class AfterCatureTasksUI : UserControl
    {
        public AfterCatureTasksUI()
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

            int maxWidth = 0;
            int yGap = 8;

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
                chkAfterCaptureTask.Location = new Point(8, yGap);
                chkAfterCaptureTask.CheckedChanged += chkAfterCaptureTask_CheckedChanged;
                chkAfterCaptureTask.Checked = config.HasFlag(job.Enum);
                this.Controls.Add(chkAfterCaptureTask);

                maxWidth = Math.Max(maxWidth, chkAfterCaptureTask.Width);
                yGap += 24;
            }

            this.Width = Math.Max(400, maxWidth);
            this.Height = yGap + 24;
        }
    }
}