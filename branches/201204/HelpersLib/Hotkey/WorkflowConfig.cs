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
        public Workflow Workflow { get; set; }

        public WindowWorkflow(Workflow wf)
        {
            Workflow = wf;
            InitializeComponent();

            this.Text = "Workflow - " + wf.HotkeyConfig.Description;
            this.txtDescription.Text = wf.HotkeyConfig.Description;

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
            Workflow.HotkeyConfig.Description = txtDescription.Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (EActivity act in lbActivitiesAll.SelectedItems)
            {
                lbActivitiesUser.Items.Add(act);
            }
        }

        private void WindowWorkflow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Workflow.Activities.Clear();
            foreach (EActivity act in lbActivitiesUser.Items)
            {
                Workflow.Activities.Add(act);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<EActivity> tempActivities = new List<EActivity>();
            foreach (EActivity act in lbActivitiesUser.SelectedItems)
            {
                tempActivities.Add(act);
            }
            foreach (EActivity act in tempActivities)
            {
                lbActivitiesUser.Items.Remove(act);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}