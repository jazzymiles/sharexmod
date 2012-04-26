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
    public partial class WindowWorkflow : Form
    {
        public WindowWorkflow(Workflow wf)
        {
            InitializeComponent();

            foreach (EActivity act in wf.Activities)
            {
                this.lbActivitiesUser.Items.Add(act);
            }
        }
    }
}