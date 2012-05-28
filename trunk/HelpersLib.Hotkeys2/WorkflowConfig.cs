using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib;

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
            this.pgSettings.SelectedObject = wf.DestConfig;
            this.txtDescription.Text = wf.HotkeyConfig.Description;

            lvActivitiesAll.Groups.AddRange(new[]
            {
                lvgCapture,
                lvgAfterCapture,lvgAfterCaptureEffects, lvgUploaders,
                lvgUploadersImages, lvgUploadersText, lvgUploadersFiles, lvgUploadersLinks
            });
            lvActivitiesUser.Groups.AddRange(lvActivitiesAll.Groups);

            FillListActivitiesAll();
            FillListActivitiesUser();

            lvActivitiesAll.Columns[0].Width = 240;
            lvActivitiesUser.Columns[0].Width = 240;
        }

        private void FillListActivitiesUser()
        {
            foreach (ImageDestination uploader in Workflow.DestConfig.ImageUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (TextDestination uploader in Workflow.DestConfig.TextUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (UrlShortenerType uploader in Workflow.DestConfig.LinkUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (FileDestination uploader in Workflow.DestConfig.FileUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (EActivity act in Workflow.Activities)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(act));
            }
        }

        private void FillListActivitiesAll()
        {
            foreach (ImageDestination uploader in Enum.GetValues(typeof(ImageDestination)))
            {
                lvActivitiesAll.Items.Add(GetListViewItem(uploader));
            }

            foreach (TextDestination uploader in Enum.GetValues(typeof(TextDestination)))
            {
                lvActivitiesAll.Items.Add(GetListViewItem(uploader));
            }

            foreach (FileDestination uploader in Enum.GetValues(typeof(FileDestination)))
            {
                lvActivitiesAll.Items.Add(GetListViewItem(uploader));
            }

            foreach (UrlShortenerType uploader in Enum.GetValues(typeof(UrlShortenerType)))
            {
                lvActivitiesAll.Items.Add(GetListViewItem(uploader));
            }

            foreach (EActivity act in Enum.GetValues(typeof(EActivity)))
            {
                lvActivitiesAll.Items.Add(GetListViewItem(act));
            }
        }

        private ListViewItem GetListViewItem(UrlShortenerType dest)
        {
            ListViewItem lvi = new ListViewItem(dest.GetDescription());
            lvi.Group = lvgUploadersLinks;
            lvi.Tag = dest;
            return lvi;
        }

        private ListViewItem GetListViewItem(TextDestination dest)
        {
            ListViewItem lvi = new ListViewItem(dest.GetDescription());
            lvi.Group = lvgUploadersText;
            lvi.Tag = dest;
            return lvi;
        }

        private ListViewItem GetListViewItem(ImageDestination dest)
        {
            ListViewItem lvi = new ListViewItem(dest.GetDescription());
            lvi.Group = lvgUploadersImages;
            lvi.Tag = dest;
            return lvi;
        }

        private ListViewItem GetListViewItem(FileDestination dest)
        {
            ListViewItem lvi = new ListViewItem(dest.GetDescription());
            lvi.Group = lvgUploadersFiles;
            lvi.Tag = dest;
            return lvi;
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
                ListViewItem lvi2 = new ListViewItem();
                if (lvi.Tag.GetType() == typeof(FileDestination))
                    lvi2 = GetListViewItem((FileDestination)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(EActivity))
                    lvi2 = GetListViewItem((EActivity)lvi.Tag);
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

            Workflow.DestConfig.ImageUploaders.Clear();
            Workflow.DestConfig.TextUploaders.Clear();
            Workflow.DestConfig.FileUploaders.Clear();
            Workflow.DestConfig.LinkUploaders.Clear();
            Workflow.Activities.Clear();

            foreach (ListViewItem lvi in lvActivitiesUser.Items)
            {
                if (lvi.Tag.GetType() == typeof(ImageDestination))
                    Workflow.DestConfig.ImageUploaders.Add((ImageDestination)lvi.Tag);
                if (lvi.Tag.GetType() == typeof(TextDestination))
                    Workflow.DestConfig.TextUploaders.Add((TextDestination)lvi.Tag);
                if (lvi.Tag.GetType() == typeof(FileDestination))
                    Workflow.DestConfig.FileUploaders.Add((FileDestination)lvi.Tag);
                if (lvi.Tag.GetType() == typeof(UrlShortenerType))
                    Workflow.DestConfig.LinkUploaders.Add((UrlShortenerType)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(EActivity))
                    Workflow.Activities.Add((EActivity)lvi.Tag);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}