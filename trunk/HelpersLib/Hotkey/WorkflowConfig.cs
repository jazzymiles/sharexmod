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

        private bool ValidateActivity(EActivity act_to_be_added, int index_to_new_act)
        {
            int index_to_capture = -1;
            bool hasCapture = false;
            EActivity act_capture = EActivity.CaptureActiveMonitor;

            foreach (EActivity act_existing in lbActivitiesUser.Items)
            {
                switch (act_existing)
                {
                    case EActivity.AfterCaptureTasks:
                    case EActivity.ImageAnnotate:
                    case EActivity.SaveToFile:
                    case EActivity.SaveToFileWithDialog:
                    case EActivity.UploadClipboard:
                    case EActivity.UploadFile:
                    case EActivity.UploadToRemoteHost:
                        break;
                    case EActivity.CaptureActiveMonitor:
                    case EActivity.CaptureActiveWindow:
                    case EActivity.CaptureDiamondRegion:
                    case EActivity.CaptureEllipseRegion:
                    case EActivity.CaptureFreeHandRegion:
                    case EActivity.CapturePolygonRegion:
                    case EActivity.CaptureRectangleRegion:
                    case EActivity.CaptureRoundedRectangleRegion:
                    case EActivity.CaptureScreen:
                    case EActivity.CaptureTriangleRegion:
                    case EActivity.CaptureWindowRectangle:
                        hasCapture = true;
                        index_to_capture = lbActivitiesUser.Items.IndexOf(act_existing);
                        act_capture = act_existing;
                        break;
                    default:
                        throw new Exception("Unhandled: " + act_existing.GetDescription());
                }
            }

            if (act_to_be_added == EActivity.SaveToFile)
            {
                if (hasCapture && index_to_capture >= index_to_new_act)
                {
                    MessageBox.Show(string.Format("{0} must occur after {1}",
                        EActivity.SaveToFile.GetDescription(),
                        act_capture.GetDescription()),
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (lbActivitiesUser.Items.Contains(EActivity.UploadToRemoteHost))
                {
                    int index_upload_to_remote_host = lbActivitiesUser.Items.IndexOf(EActivity.UploadToRemoteHost);
                    if (index_to_new_act > index_upload_to_remote_host)
                    {
                        MessageBox.Show(string.Format("{0} must occur before {1}",
                            EActivity.SaveToFile.GetDescription(),
                            EActivity.UploadToRemoteHost.GetDescription()),
                            Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (EActivity act in lbActivitiesAll.SelectedItems)
            {
                if (ValidateActivity(act, lbActivitiesUser.Items.Count))
                    lbActivitiesUser.Items.Add(act);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (lbActivitiesUser.SelectedIndex > -1)
            {
                foreach (EActivity act in lbActivitiesAll.SelectedItems)
                {
                    if (ValidateActivity(act, lbActivitiesUser.SelectedIndex))
                        lbActivitiesUser.Items.Insert(lbActivitiesUser.SelectedIndex, act);
                }
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

            Workflow.Activities.Clear();
            foreach (EActivity act in lbActivitiesUser.Items)
            {
                Workflow.Activities.Add(act);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}