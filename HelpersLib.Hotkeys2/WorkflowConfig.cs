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

        private ListViewGroup lvgCapture = new ListViewGroup(ComponentModelStrings.ActivitiesCapture, HorizontalAlignment.Left);
        private ListViewGroup lvgAfterCapture = new ListViewGroup(ComponentModelStrings.ActivitiesAfterCapture, HorizontalAlignment.Left);
        private ListViewGroup lvgAfterCaptureEffects = new ListViewGroup(ComponentModelStrings.ActivitiesAfterCaptureEffects, HorizontalAlignment.Left);
        private ListViewGroup lvgUploaders = new ListViewGroup(ComponentModelStrings.ActivitiesUploaders, HorizontalAlignment.Left);
        private ListViewGroup lvgUploadersImages = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersImages, HorizontalAlignment.Left);
        private ListViewGroup lvgUploadersText = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersText, HorizontalAlignment.Left);
        private ListViewGroup lvgUploadersFiles = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersFiles, HorizontalAlignment.Left);
        private ListViewGroup lvgUploadersLinks = new ListViewGroup(ComponentModelStrings.ActivitiesUploadersLinks, HorizontalAlignment.Left);

        public WindowWorkflow(Workflow wf)
        {
            Workflow = wf;
            wf.Settings.GetDefaults();

            InitializeComponent();

            this.Text = "Workflow - " + wf.HotkeyConfig.Description;
            this.txtDescription.Text = wf.HotkeyConfig.Description;

            this.pgSettings.SelectedObject = wf.Settings.DestConfig;

            foreach (ExternalProgram fileAction in Workflow.Settings.ExternalPrograms)
            {
                AddFileAction(fileAction);
            }

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
            lvActivitiesUser.Items.Add(GetListViewItem(Workflow.Hotkey));

            foreach (ImageDestination uploader in Workflow.Settings.DestConfig.ImageUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (TextDestination uploader in Workflow.Settings.DestConfig.TextUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (UrlShortenerType uploader in Workflow.Settings.DestConfig.LinkUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (FileDestination uploader in Workflow.Settings.DestConfig.FileUploaders)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(uploader));
            }

            foreach (EActivity act in Workflow.ActivitiesBeta)
            {
                lvActivitiesUser.Items.Add(GetListViewItem(act));
            }

            tcWorkflow.TabPages.Clear();
            tcWorkflow.TabPages.Add(tpActivities);
            UpdateTabsVisibility();
        }

        private void FillListActivitiesAll()
        {
            foreach (EHotkey hotkey in Enum.GetValues(typeof(EHotkey)))
            {
                lvActivitiesAll.Items.Add(GetListViewItem(hotkey));
            }

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

        private ListViewItem GetListViewItem(EHotkey hotkey)
        {
            ListViewItem lvi = new ListViewItem(hotkey.GetDescription());
            lvi.Group = lvgCapture;
            lvi.Tag = hotkey;
            return lvi;
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
                else if (keyData == Keys.Delete)
                {
                    ActivityRemove();
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

                if (lvi.Tag.GetType() == typeof(ImageDestination))
                    lvi2 = GetListViewItem((ImageDestination)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(TextDestination))
                    lvi2 = GetListViewItem((TextDestination)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(FileDestination))
                    lvi2 = GetListViewItem((FileDestination)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(UrlShortenerType))
                    lvi2 = GetListViewItem((UrlShortenerType)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(EActivity))
                    lvi2 = GetListViewItem((EActivity)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(EHotkey))
                    lvi2 = GetListViewItem((EHotkey)lvi.Tag);

                lvActivitiesUser.Items.Add(lvi2);
            }

            UpdateTabsVisibility();
        }

        private void UpdateTabsVisibility()
        {
            bool showSettings = false;
            bool showExternalPrograms = false;

            foreach (ListViewItem lvi in lvActivitiesUser.Items)
            {
                if (lvi.Tag.GetType() == typeof(EHotkey) && (EHotkey)lvi.Tag == EHotkey.ClipboardUpload)
                    showSettings = true;
                else if (lvi.Tag.GetType() == typeof(TextDestination))
                    showSettings = true;

                if (lvi.Tag.GetType() == typeof(EActivity) && (EActivity)lvi.Tag == EActivity.RunExternalProgram)
                    showExternalPrograms = true;
            }

            if (showSettings)
            {
                if (!tcWorkflow.TabPages.Contains(tpSettings))
                    tcWorkflow.TabPages.Add(tpSettings);
            }
            else
                tcWorkflow.TabPages.Remove(tpSettings);

            if (showExternalPrograms)
            {
                if (!tcWorkflow.TabPages.Contains(tpActions))
                    tcWorkflow.TabPages.Add(tpActions);
            }
            else
                tcWorkflow.TabPages.Remove(tpActions);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ActivityRemove();
        }

        private void ActivityRemove()
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
            UpdateTabsVisibility();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            Workflow.Settings.Clear();
            Workflow.ActivitiesBeta.Clear();

            foreach (ListViewItem lvi in lvActivitiesUser.Items)
            {
                if (lvi.Tag.GetType() == typeof(ImageDestination))
                    Workflow.Settings.DestConfig.ImageUploaders.Add((ImageDestination)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(TextDestination))
                    Workflow.Settings.DestConfig.TextUploaders.Add((TextDestination)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(FileDestination))
                    Workflow.Settings.DestConfig.FileUploaders.Add((FileDestination)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(UrlShortenerType))
                    Workflow.Settings.DestConfig.LinkUploaders.Add((UrlShortenerType)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(EActivity))
                    Workflow.ActivitiesBeta.Add((EActivity)lvi.Tag);
                else if (lvi.Tag.GetType() == typeof(EHotkey))
                    Workflow.Hotkey = (EHotkey)lvi.Tag;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region External Programs

        private void AddFileAction(ExternalProgram fileAction)
        {
            ListViewItem lvi = new ListViewItem(fileAction.Name ?? "");
            lvi.Tag = fileAction;
            lvi.Checked = fileAction.IsActive;
            lvi.SubItems.Add(fileAction.Path ?? "");
            lvi.SubItems.Add(fileAction.Args ?? "");
            lvActions.Items.Add(lvi);
        }

        private void btnActionsRemove_Click(object sender, EventArgs e)
        {
            if (lvActions.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvActions.SelectedItems[0];
                ExternalProgram fileAction = lvi.Tag as ExternalProgram;

                Workflow.Settings.ExternalPrograms.Remove(fileAction);
                lvActions.Items.Remove(lvi);
            }
        }

        private void btnActionsEdit_Click(object sender, EventArgs e)
        {
            if (lvActions.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvActions.SelectedItems[0];
                ExternalProgram fileAction = lvi.Tag as ExternalProgram;

                using (FileActionForm form = new FileActionForm(fileAction))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        lvi.Text = fileAction.Name ?? "";
                        lvi.SubItems[1].Text = fileAction.Path ?? "";
                        lvi.SubItems[2].Text = fileAction.Args ?? "";
                    }
                }
            }
        }

        private void btnActionsAdd_Click(object sender, EventArgs e)
        {
            using (FileActionForm form = new FileActionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ExternalProgram fileAction = form.FileAction;
                    fileAction.IsActive = true;
                    Workflow.Settings.ExternalPrograms.Add(fileAction);
                    AddFileAction(fileAction);
                }
            }
        }

        private void lvActions_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ExternalProgram fileAction = e.Item.Tag as ExternalProgram;
            fileAction.IsActive = e.Item.Checked;
        }

        #endregion External Programs
    }
}