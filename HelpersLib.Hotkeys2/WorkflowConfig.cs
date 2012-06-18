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

            chkPerformGlobalAfterCaptureTasks.Checked = Workflow.Settings.PerformGlobalAfterCaptureTasks;
            txtTextFormat.Text = this.Workflow.Settings.DestConfig.TextFormat;

            #endregion Capture

            #region After Capture

            ucAfterCaptureTasks.ConfigUI(Workflow.Subtasks, chkAfterCaptureTask_CheckedChanged);

            #endregion After Capture

            #region External Programs

            foreach (ExternalProgram fileAction in Workflow.Settings.ExternalPrograms)
            {
                AddFileAction(fileAction);
            }

            HideTabRunExternalPrograms();

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

            if (Workflow.Subtasks.HasFlag(Subtask.UploadToDefaultRemoteHost) ||
                Workflow.Settings.PerformGlobalAfterCaptureTasks)
            {
                HideTabShare();
            }
            else
            {
                ShowTabUploadAndShare();
            }

            #endregion Share
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
                else if (task == Subtask.UploadToDefaultRemoteHost)
                    HideTabShare();
            }
            else
            {
                Workflow.Subtasks &= ~task;
                if (task == Subtask.RunExternalProgram)
                    HideTabRunExternalPrograms();
                else if (task == Subtask.UploadToDefaultRemoteHost)
                    ShowTabUploadAndShare();
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
            Workflow.Settings.PerformGlobalAfterCaptureTasks = chkPerformGlobalAfterCaptureTasks.Checked;
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
                    tcWorkflow.TabPages.Insert(tcWorkflow.TabPages.IndexOf(tpCapture) + 2, tpUpload);
                }
            }
        }

        private void HideTabShare()
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
        }

        private void ShowAllUploaders()
        {
            gbFileUploaders.Visible = true;
            gbImageUploaders.Visible = true;
            gbTextUploaders.Visible = true;
            HideTabAfterCapture();
        }

        private void HideTextUploaders()
        {
            gbFileUploaders.Visible = true;
            gbImageUploaders.Visible = true;
            gbTextUploaders.Visible = false;
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

                        else if (task.Enum.HasFlag(Subtask.ShortenUrl))
                        {
                            foreach (UrlShortenerType uploader in Workflow.Settings.DestConfig.LinkUploaders)
                            {
                                sb.AppendLine("-------- " + uploader.GetDescription());
                            }
                        }

                        else if (task.Enum.HasFlag(Subtask.ShareUsingSocialNetworkingService))
                        {
                            foreach (SocialNetworkingService sns in Workflow.Settings.DestConfig.SocialNetworkingServices)
                            {
                                sb.AppendLine("-------- " + sns.GetDescription());
                            }
                        }
                    }
                }
            }

            if (Workflow.Settings.PerformGlobalAfterCaptureTasks)
            {
                sb.AppendLine("Perform global After Capture Tasks");
            }

            if (Workflow.Settings.DestConfig.FileUploaders.Count > 0)
            {
                sb.AppendLine("Share file using " + Workflow.Settings.DestConfig.ToStringFileUploaders());
            }

            if (Workflow.Settings.DestConfig.TextUploaders.Count > 0)
            {
                sb.AppendLine("Share text using " + Workflow.Settings.DestConfig.ToStringTextUploaders());
                sb.AppendLine("Text format: " + Workflow.Settings.DestConfig.TextFormat);
            }
            if (Workflow.Settings.DestConfig.ImageUploaders.Count > 0)
                sb.AppendLine("Share image using " + Workflow.Settings.DestConfig.ToStringImageUploaders());

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