using HelpersLib;
using HelpersLibMod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;

namespace HelpersLib.Hotkeys2
{
    public partial class WindowWorkflow : Form
    {
        public Workflow Workflow { get; set; }

        public WindowWorkflow(Workflow wf)
        {
            Workflow = wf;
            wf.Settings.GetDefaults();

            InitializeComponent();
            OnLoad();
        }

        private void OnLoad()
        {
            #region Capture

            this.Text = "Workflow - " + Workflow.HotkeyConfig.Description;
            this.txtDescription.Text = Workflow.HotkeyConfig.Description;

            cboCapture.Items.Clear();
            foreach (EHotkey hotkey in Enum.GetValues(typeof(EHotkey)))
            {
                cboCapture.Items.Add(hotkey.GetDescription());
            }
            cboCapture.SelectedIndex = (int)Workflow.Hotkey;

            chkPerformGlobalAfterCaptureTasks.Checked = Workflow.Settings.ApplyDefaultSettings;
            txtTextFormat.Text = this.Workflow.Settings.DestConfig.TextFormat;

            #endregion Capture

            #region After Capture

            ucAfterCaptureTasks.ConfigUI(Workflow.Subtasks, chkAfterCaptureTask_CheckedChanged);

            #endregion After Capture

            ucAfterUploadTasks.ConfigUI(Workflow.AfterUploadTasks, chkAfterUploadTask_CheckedChanged);

            #region External Programs

            foreach (ExternalProgram fileAction in Workflow.Settings.ExternalPrograms)
            {
                AddFileAction(fileAction);
            }

            #endregion External Programs

            #region Share

            foreach (FileDestination uploader in Enum.GetValues(typeof(FileDestination)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.FileUploaders.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader_CheckedChanged);
                flpFileUploaders.Controls.Add(rbUploader);
            }

            foreach (ImageDestination uploader in Enum.GetValues(typeof(ImageDestination)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.ImageUploaders.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader_CheckedChanged);
                flpImageUploaders.Controls.Add(rbUploader);
            }

            foreach (TextDestination uploader in Enum.GetValues(typeof(TextDestination)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.TextUploaders.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader_CheckedChanged);
                flpTextUploaders.Controls.Add(rbUploader);
            }

            foreach (UrlShortenerType uploader in Enum.GetValues(typeof(UrlShortenerType)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.LinkUploaders.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader_CheckedChanged);
                flpUrlShorteners.Controls.Add(rbUploader);
            }

            foreach (SocialNetworkingService uploader in Enum.GetValues(typeof(SocialNetworkingService)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.SocialNetworkingServices.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader_CheckedChanged);
                flpSocialNetworkingServices.Controls.Add(rbUploader);
            }

            if (Workflow.Subtasks.HasFlag(Subtask.UploadToRemoteHost) || Workflow.Settings.ApplyDefaultSettings)
            {
                ShowTabUploadAndShare();
            }
            else
            {
                HideTabUploadAndShare();
            }

            #endregion Share

            #region Secondary Upload

            foreach (FileDestination uploader in Enum.GetValues(typeof(FileDestination)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.FileUploaders2.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader2_CheckedChanged);
                flpFileUploaders2.Controls.Add(rbUploader);
            }

            foreach (ImageDestination uploader in Enum.GetValues(typeof(ImageDestination)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.ImageUploaders2.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader2_CheckedChanged);
                flpImageUploaders2.Controls.Add(rbUploader);
            }

            foreach (TextDestination uploader in Enum.GetValues(typeof(TextDestination)))
            {
                RadioButton rbUploader = new RadioButton()
                {
                    AutoSize = true,
                    Text = uploader.GetDescription(),
                    Checked = Workflow.Settings.DestConfig.TextUploaders.Contains(uploader),
                    Tag = uploader
                };
                rbUploader.CheckedChanged += new EventHandler(rbUploader2_CheckedChanged);
                flpTextUploaders2.Controls.Add(rbUploader);
            }

            #endregion Secondary Upload
        }

        private void chkAfterUploadTask_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAfterUploadTask = sender as CheckBox;
            AfterUploadTasks task = (AfterUploadTasks)chkAfterUploadTask.Tag;

            if (chkAfterUploadTask.Checked)
            {
                Workflow.AfterUploadTasks |= task;
            }
            else
            {
                Workflow.AfterUploadTasks &= ~task;
            }
        }

        private void chkAfterCaptureTask_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAfterCaptureTask = sender as CheckBox;
            Subtask task = (Subtask)chkAfterCaptureTask.Tag;

            if (chkAfterCaptureTask.Checked)
            {
                Workflow.Subtasks |= task;
                if (task == Subtask.RunExternalProgram)
                    ShowTabRunExternalPrograms();
                else if (task == Subtask.UploadToRemoteHost)
                    ShowTabUploadAndShare();
            }
            else
            {
                Workflow.Subtasks &= ~task;
                if (task == Subtask.RunExternalProgram)
                    HideTabRunExternalPrograms();
                else if (task == Subtask.UploadToRemoteHost)
                    HideTabUploadAndShare();
            }
        }

        private void rbUploader_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbUploader = sender as RadioButton;
            if (rbUploader.Tag.GetType() == typeof(FileDestination))
            {
                FileDestination uploader = (FileDestination)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader(uploader);
            }
            else if (rbUploader.Tag.GetType() == typeof(ImageDestination))
            {
                ImageDestination uploader = (ImageDestination)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader(uploader);
            }
            else if (rbUploader.Tag.GetType() == typeof(TextDestination))
            {
                TextDestination uploader = (TextDestination)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader(uploader);
            }
            else if (rbUploader.Tag.GetType() == typeof(UrlShortenerType))
            {
                UrlShortenerType uploader = (UrlShortenerType)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader(uploader);
            }
            else if (rbUploader.Tag.GetType() == typeof(SocialNetworkingService))
            {
                SocialNetworkingService uploader = (SocialNetworkingService)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader(uploader);
            }
        }

        private void rbUploader2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbUploader = sender as RadioButton;
            if (rbUploader.Tag.GetType() == typeof(FileDestination))
            {
                FileDestination uploader = (FileDestination)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader2(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader2(uploader);
            }
            else if (rbUploader.Tag.GetType() == typeof(ImageDestination))
            {
                ImageDestination uploader = (ImageDestination)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader2(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader2(uploader);
            }
            else if (rbUploader.Tag.GetType() == typeof(TextDestination))
            {
                TextDestination uploader = (TextDestination)rbUploader.Tag;
                if (rbUploader.Checked)
                    Workflow.Settings.DestConfig.AddUploader2(uploader);
                else
                    Workflow.Settings.DestConfig.RemoveUploader2(uploader);
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            Workflow.HotkeyConfig.Description = txtDescription.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Workflow.Settings.DestConfig.TextFormat = txtTextFormat.Text;
            this.Workflow.Subtasks &= ~Subtask.None;
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

        private void chkPerformGlobalAfterCaptureTasks_CheckedChanged(object sender, EventArgs e)
        {
            Workflow.Settings.ApplyDefaultSettings = chkPerformGlobalAfterCaptureTasks.Checked;

            if (chkPerformGlobalAfterCaptureTasks.Checked)
                HideTabUploadAndShare();
            else
                ShowTabUploadAndShare();
        }

        #region Show/Hide Tabs

        private void ShowTabAfterCapture()
        {
            if (!tcWorkflow.TabPages.Contains(tpAfterCapture))
            {
                tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpCapture) + 1, tpAfterCapture);
            }
        }

        private void HideTabAfterCapture()
        {
            if (tcWorkflow.TabPages.Contains(tpAfterCapture))
            {
                tcWorkflow.TabPages.Remove(tpAfterCapture);
            }
        }

        private void ShowTabRunExternalPrograms()
        {
            if (!tcWorkflow.TabPages.Contains(tpRunExternalPrograms))
            {
                if (tcWorkflow.TabPages.Contains(tpAfterCapture))
                {
                    tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpAfterCapture) + 1, tpRunExternalPrograms);
                }
                else
                {
                    tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpCapture) + 1, tpRunExternalPrograms);
                }
            }
        }

        private void HideTabRunExternalPrograms()
        {
            if (tcWorkflow.TabPages.Contains(tpRunExternalPrograms))
            {
                tcWorkflow.TabPages.Remove(tpRunExternalPrograms);
            }
        }

        private void ShowTabUploadAndShare()
        {
            if (!tcWorkflow.TabPages.Contains(tpUpload))
            {
                if (tcWorkflow.TabPages.Contains(tpAfterCapture))
                {
                    if (tcWorkflow.TabPages.Contains(tpRunExternalPrograms))
                    {
                        tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpRunExternalPrograms) + 1, tpUpload);
                        tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpRunExternalPrograms) + 2, tpShare);
                    }
                    else
                    {
                        tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpAfterCapture) + 1, tpUpload);
                        tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpAfterCapture) + 2, tpShare);
                    }
                }
                else
                {
                    tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpCapture) + 1, tpUpload);
                    tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpCapture) + 2, tpShare);
                }
            }
        }

        private void HideTabUploadAndShare()
        {
            Workflow.Settings.Clear();
            if (tcWorkflow.TabPages.Contains(tpUpload))
            {
                tcWorkflow.TabPages.Remove(tpUpload);
            }
            if (tcWorkflow.TabPages.Contains(tpShare))
            {
                tcWorkflow.TabPages.Remove(tpShare);
            }
        }

        #endregion Show/Hide Tabs

        private void cboCapture_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCaptureTypeChanged();
        }

        private void OnCaptureTypeChanged()
        {
            EHotkey hotkey = (EHotkey)cboCapture.SelectedIndex;
            switch (hotkey)
            {
                case EHotkey.ClipboardUpload:
                    ShowAllUploaders();
                    gbSettings.Visible = true;
                    break;

                case EHotkey.FileUpload:
                    ShowAllUploaders();
                    gbSettings.Visible = false;
                    break;

                default:
                    HideTextUploaders();
                    ShowTabAfterCapture();
                    gbSettings.Visible = false;
                    break;
            }
            Workflow.Hotkey = hotkey;
        }

        private void ShowAllUploaders()
        {
            gbFileUploaders.Visible = gbFileUploaders2.Visible = true;
            gbImageUploaders.Visible = true;
            gbTextUploaders.Visible = true;
            ShowTabUploadAndShare();
            HideTabAfterCapture();
        }

        private void HideTextUploaders()
        {
            gbFileUploaders.Visible = true;
            gbImageUploaders.Visible = true;
            gbTextUploaders.Visible = gbTextUploaders2.Visible = false;
        }

        public string ToSummaryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(cboCapture.Text);

            if (Workflow.Subtasks != Subtask.None)
            {
                sb.AppendLine("Perform custom After Capture Tasks:");
                var tasks = Enum.GetValues(typeof(Subtask)).Cast<Subtask>().Select(x => new
                {
                    Description = x.GetDescription(),
                    Enum = x
                });

                foreach (var task in tasks)
                {
                    if (task.Enum == Subtask.None)
                        continue;

                    if (Workflow.Subtasks.HasFlag(task.Enum))
                    {
                        sb.AppendLine("---- " + task.Enum.GetDescription());

                        if (task.Enum.HasFlag(Subtask.RunExternalProgram))
                        {
                            foreach (ExternalProgram prg in Workflow.Settings.ExternalPrograms)
                            {
                                if (prg.IsActive)
                                    sb.AppendLine("-------- " + prg.Name);
                            }
                        }

                        else if (task.Enum.HasFlag(Subtask.UploadToRemoteHost))
                        {
                            foreach (FileDestination uploader in Workflow.Settings.DestConfig.FileUploaders)
                            {
                                sb.AppendLine("-------- Share file using " + uploader.GetDescription());
                            }

                            foreach (ImageDestination uploader in Workflow.Settings.DestConfig.ImageUploaders)
                            {
                                sb.AppendLine("-------- Share image using " + uploader.GetDescription());
                            }

                            foreach (TextDestination uploader in Workflow.Settings.DestConfig.TextUploaders)
                            {
                                sb.AppendLine("-------- Share text using " + uploader.GetDescription());
                                sb.AppendLine("-------- Text format: " + Workflow.Settings.DestConfig.TextFormat);
                            }
                        }
                    }
                }
            }

            if (Workflow.AfterUploadTasks != AfterUploadTasks.None)
            {
                sb.AppendLine("Perform custom After Upload Tasks:");
                var tasks = Enum.GetValues(typeof(AfterUploadTasks)).Cast<AfterUploadTasks>().Select(x => new
                {
                    Description = x.GetDescription(),
                    Enum = x
                });

                foreach (var task in tasks)
                {
                    if (task.Enum == AfterUploadTasks.None)
                        continue;

                    else if (Workflow.AfterUploadTasks.HasFlag(task.Enum))
                    {
                        sb.AppendLine("---- " + task.Enum.GetDescription());

                        if (task.Enum.HasFlag(AfterUploadTasks.UseURLShortener))
                        {
                            foreach (UrlShortenerType uploader in Workflow.Settings.DestConfig.LinkUploaders)
                            {
                                sb.AppendLine("-------- " + uploader.GetDescription());
                            }
                        }

                        else if (task.Enum.HasFlag(AfterUploadTasks.ShareUsingSocialNetworkingService))
                        {
                            foreach (SocialNetworkingService sns in Workflow.Settings.DestConfig.SocialNetworkingServices)
                            {
                                sb.AppendLine("-------- " + sns.GetDescription());
                            }
                        }
                    }
                }
            }

            if (Workflow.Settings.ApplyDefaultSettings)
            {
                sb.AppendLine("Apply default settings.");
            }

            return sb.ToString();
        }

        private void tcWorkflow_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSummary.Text = this.ToSummaryString();
        }

        private void WindowWorkflow_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}