using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;

namespace HelpersLib.Hotkeys2
{
    public partial class WindowWorkflow : Form
    {
        public Workflow Workflow { get; set; }

        ListViewGroup lvgCapture = new ListViewGroup(ComponentModelStrings.ActivitiesCapture, HorizontalAlignment.Left);
        ListViewGroup lvgAfterCapture = new ListViewGroup(ComponentModelStrings.ActivitiesAfterCapture, HorizontalAlignment.Left);
        ListViewGroup lvgAfterCaptureEffects = new ListViewGroup(ComponentModelStrings.ActivitiesAfterCaptureEffects, HorizontalAlignment.Left);
        ListViewGroup lvgUploaders = new ListViewGroup(ComponentModelStrings.ActivitiesUploaders, HorizontalAlignment.Left);
        ListViewGroup lvgUploadersImages = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersImages, HorizontalAlignment.Left);
        ListViewGroup lvgUploadersText = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersText, HorizontalAlignment.Left);
        ListViewGroup lvgUploadersFiles = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersFiles, HorizontalAlignment.Left);
        ListViewGroup lvgUploadersLinks = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersLinks, HorizontalAlignment.Left);

        public WindowWorkflow(Workflow wf)
        {
            Workflow = wf;
            InitializeComponent();

            this.Text = "Workflow - " + wf.HotkeyConfig.Description;
            this.pgSettings.SelectedObject = wf;
            this.txtDescription.Text = wf.HotkeyConfig.Description;

            lvActivitiesAll.Groups.AddRange(new[]
            {
                lvgCapture,
                lvgAfterCapture,lvgAfterCaptureEffects, lvgUploaders,
                lvgUploadersImages, lvgUploadersText, lvgUploadersFiles, lvgUploadersLinks
            });
            lvActivitiesUser.Groups.AddRange(lvActivitiesAll.Groups);

            foreach (EActivity act in wf.Activities)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(act));
            }

            foreach (EActivity act in Enum.GetValues(typeof(EActivity)))
            {
                lvActivitiesAll.Items.Add(GetListViewItem(act));
            }

            lvActivitiesAll.Columns[0].Width = 240;
            lvActivitiesUser.Columns[0].Width = 240;
        }

        private ListViewItem GetListViewItem(EActivity act)
        {
            ListViewItem lvi = new ListViewItem(act.GetDescription());
            if (act.GetCategory() == lvgCapture.Header)
                lvi.Group = lvgCapture;
            else if (act.GetCategory() == lvgAfterCapture.Header)
                lvi.Group = lvgAfterCapture;
            else if (act.GetCategory() == lvgAfterCaptureEffects.Header)
                lvi.Group = lvgAfterCaptureEffects;
            else if (act.GetCategory() == lvgUploaders.Header)
                lvi.Group = lvgUploaders;
            else if (act.GetCategory() == lvgUploadersImages.Header)
                lvi.Group = lvgUploadersImages;
            else if (act.GetCategory() == lvgUploadersText.Header)
                lvi.Group = lvgUploadersText;
            else if (act.GetCategory() == lvgUploadersFiles.Header)
                lvi.Group = lvgUploadersFiles;
            else if (act.GetCategory() == lvgUploadersLinks.Header)
                lvi.Group = lvgUploadersLinks;

            lvi.Tag = act;
            lvi.ImageKey = act.GetDescription();
            return lvi;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Workflow.HotkeyConfig.Description = txtDescription.Text;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!txtDescription.Focused)
            {
                if (keyData == (Keys.ShiftKey | Keys.A))
                {
                    ActivityAdd();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ActivityAdd();
        }

        private void ActivityAdd()
        {
            foreach (ListViewItem lvi in lvActivitiesAll.SelectedItems)
            {
                EActivity act = (EActivity)lvi.Tag;
                ListViewItem lvi2 = GetListViewItem(act);
                lvActivitiesUser.Items.Add(lvi2);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<ListViewItem> tempActivities = new List<ListViewItem>();
            foreach (ListViewItem lvi in lvActivitiesUser.SelectedItems)
            {
                tempActivities.Add(lvi);
            }
            foreach (ListViewItem act in tempActivities)
            {
                lvActivitiesUser.Items.Remove(act);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            Workflow.Activities.Clear();
            foreach (ListViewItem lvi in lvActivitiesUser.Items)
            {
                EActivity act = (EActivity)lvi.Tag;
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