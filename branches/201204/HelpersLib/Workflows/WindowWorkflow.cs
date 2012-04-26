using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;

namespace HelplersLib
{
    public partial class WindowWorkflow : Form
    {
        public string Description { get; set; }

        public WindowWorkflow(Workflow wf)
        {
            InitializeComponent();

            foreach (EActivity act in wf.Activities)
            {
                this.lbActivitiesUser.Items.Add(act);
            }

            foreach (EActivity act in Enum.GetValues(typeof(EActivity)))
            {
                this.lbActivitiesAll.Items.Add(act);
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Description = txtDescription.Text;
        }
    }
}