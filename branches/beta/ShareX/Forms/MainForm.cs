#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2012 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using HelpersLib;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using HelpersLibWatermark;
using HistoryLib;
using Microsoft.WindowsAPICodePack.Taskbar;
using ScreenCapture;
using ShareX.Forms;
using ShareX.HelperClasses;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UpdateCheckerLib;
using UploadersLib;
using UploadersLib.HelperClasses;

namespace ShareX
{
    public partial class MainForm : HelpersLib.Hotkeys2.HotkeyForm
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool IsReady { get; private set; }

        public HotkeyManager HotkeyManager { get; private set; }

        private bool trayClose;
        private UploadInfoManager uim;
        private ListViewColumnSorter lvwColumnSorter;

        public MainForm()
        {
            InitControls();
            UpdateControls();
        }

        private void AfterLoadJobs()
        {
            LoadSettings();
            ReloadConfig();

            if (Program.IsHotkeysAllowed)
                InitHotkeys();

            if (SettingsManager.ConfigCore.DropboxSync)
                new DropboxSyncHelper().Sync();

            if (SettingsManager.ConfigCore.AutoCheckUpdate)
            {
                new Thread(CheckUpdate).Start();
            }

            IsReady = true;

            log.Info(string.Format("Startup time: {0} ms", Program.StartTimer.ElapsedMilliseconds));

            UseCommandLineArgs(Environment.GetCommandLineArgs());
        }

        private void AfterShownJobs()
        {
            ShowActivate();
            AfterUploadersConfigClosed();
            TaskbarHelper.Init(this);
        }

        internal void AfterUploadersConfigClosed()
        {
            if (SettingsManager.ConfigUploaders == null)
                SettingsManager.LoadUploadersConfig();

            EnableDisableToolStripMenuItems<ImageDestination>(tsmiImageUploaders, tsmiTrayImageUploaders);
            EnableDisableToolStripMenuItems<TextDestination>(tsmiTextUploaders, tsmiTrayTextUploaders);
            EnableDisableToolStripMenuItems<FileDestination>(tsmiFileUploaders, tsmiTrayFileUploaders);
            EnableDisableToolStripMenuItems<UrlShortenerType>(tsmiURLShorteners, tsmiTrayURLShorteners);
        }

        /// <summary>
        /// Executes when:
        ///     Main Window loads for the first time
        ///     Whenever Options Window is closed
        /// </summary>
        public void ReloadConfig()
        {
            WatchFolder folderWatcher = new WatchFolder()
            {
                IncludeSubdirectories = true,
                Filter = "*.*",
                FolderPath = SettingsManager.ConfigCore.FolderMonitorPath
            };

            folderWatcher.FolderPath = SettingsManager.ConfigCore.FolderMonitorPath;
            if (SettingsManager.ConfigCore.FolderMonitoring)
            {
                log.Info("Monitoring folder: " + folderWatcher.FolderPath);
                folderWatcher.FileWatcherTrigger += path => UploadManager.UploadFile(path);
                folderWatcher.Enable();
            }
            else
            {
                folderWatcher.Dispose();
            }

            lvUploads.View = SettingsManager.ConfigCore.ListViewMode;

            ListViewManager.Initialize();
        }

        private void EnableDisableToolStripMenuItems<T>(params ToolStripDropDownItem[] parents)
        {
            foreach (ToolStripDropDownItem parent in parents)
            {
                for (int i = 0; i < parent.DropDownItems.Count; i++)
                {
                    parent.DropDownItems[i].Enabled = SettingsManager.ConfigUploaders.IsActive<T>(i);
                }
            }
        }

        /// <summary>
        /// This method is used to clear all checks in Uploader drop down items before DropBoxSync settings apply
        /// </summary>
        /// <param name="tsmi_parent"></param>
        private void ClearToolStripMenuItems(ToolStripMenuItem tsmi_parent)
        {
            foreach (ToolStripItem tsmi in tsmi_parent.DropDownItems)
            {
                if (tsmi is ToolStripMenuItem)
                    ((ToolStripMenuItem)tsmi).Checked = false;
            }
        }

        public void LoadSettings()
        {
            niTray.Visible = SettingsManager.ConfigCore.ShowTray;

            for (int x = 0; x < SettingsManager.ConfigCore.ColumnWidths.Length; x++)
            {
                lvUploads.Columns[x].Width = SettingsManager.ConfigCore.ColumnWidths[x];
            }

            ReloadOutputsMenu();

            #region Upload Destinations

            ClearToolStripMenuItems(tsmiImageUploaders);
            int imageUploaderIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.ImageUploaderDestination);
            ((ToolStripMenuItem)tsmiImageUploaders.DropDownItems[imageUploaderIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayImageUploaders.DropDownItems[imageUploaderIndex]).Checked = true;
            UploadManager.ImageUploader = SettingsManager.ConfigCore.ImageUploaderDestination;

            ClearToolStripMenuItems(tsmiTextUploaders);
            int textUploaderIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.TextUploaderDestination);
            ((ToolStripMenuItem)tsmiTextUploaders.DropDownItems[textUploaderIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayTextUploaders.DropDownItems[textUploaderIndex]).Checked = true;
            UploadManager.TextUploader = SettingsManager.ConfigCore.TextUploaderDestination;

            ClearToolStripMenuItems(tsmiFileUploaders);
            int fileUploaderIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.FileUploaderDestination);
            ((ToolStripMenuItem)tsmiFileUploaders.DropDownItems[fileUploaderIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayFileUploaders.DropDownItems[fileUploaderIndex]).Checked = true;
            UploadManager.FileUploader = SettingsManager.ConfigCore.FileUploaderDestination;

            ClearToolStripMenuItems(tsmiURLShorteners);
            int urlShortenerIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.URLShortenerDestination);
            ((ToolStripMenuItem)tsmiURLShorteners.DropDownItems[urlShortenerIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayURLShorteners.DropDownItems[urlShortenerIndex]).Checked = true;
            UploadManager.URLShortener = SettingsManager.ConfigCore.URLShortenerDestination;

            UpdateUploaderMenuNames();

            if (SettingsManager.ConfigCore.Workflow.Settings.DestConfig.IsEmptyAll)
            {
                SettingsManager.ConfigCore.Workflow.Settings.DestConfig.ImageUploaders.Add(UploadManager.ImageUploader);
                SettingsManager.ConfigCore.Workflow.Settings.DestConfig.TextUploaders.Add(UploadManager.TextUploader);
                SettingsManager.ConfigCore.Workflow.Settings.DestConfig.FileUploaders.Add(UploadManager.FileUploader);
                SettingsManager.ConfigCore.Workflow.Settings.DestConfig.LinkUploaders.Add(UploadManager.URLShortener);
                SettingsManager.ConfigCore.Workflow.Settings.DestConfig.SocialNetworkingServices.Add(UploadManager.SocialNetworkingService);

                SettingsManager.ConfigCore.Workflow.Settings.DestConfig.ImageUploaders2.Add(ImageDestination.FileUploader);
                SettingsManager.ConfigCore.Workflow.Settings.DestConfig.TextUploaders2.Add(TextDestination.FileUploader);

                SettingsManager.ConfigCore.Workflow.Subtasks = Subtask.CopyImageToClipboard | Subtask.SaveToFile | Subtask.UploadToRemoteHost;
                SettingsManager.ConfigCore.Workflow.AfterUploadTasks = SettingsManager.ConfigCore.AfterUploadTasks;
            }

            #endregion Upload Destinations

            UploadManager.UpdateProxySettings();
        }

        public void GetAddressBook(ToolStripMenuItem tsmiOutputEmail)
        {
            if (tsmiOutputEmail.DropDownItems != null && SettingsManager.ConfigUser != null)
            {
                tsmiOutputEmail.DropDownItems.Clear();
                if (SettingsManager.ConfigUser.AddressBook == null)
                    SettingsManager.ConfigUser.AddressBook = new List<string>();

                foreach (string email in SettingsManager.ConfigUser.AddressBook)
                {
                    ToolStripMenuItem tsmiEmailAddress = new ToolStripMenuItem(email);
                    tsmiEmailAddress.CheckOnClick = true;
                    tsmiEmailAddress.CheckedChanged += tsmiEmailAddress_CheckedChanged;
                    tsmiOutputEmail.DropDownItems.Add(tsmiEmailAddress);
                }
            }
        }

        void tsmiEmailAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsReady)
                return;

            List<string> emails = new List<string>();
            foreach (ToolStripMenuItem tsmiOutput in tsddbOutputs.DropDownItems)
            {
                HelpersLibMod.OutputEnum task = (HelpersLibMod.OutputEnum)tsmiOutput.Tag;
                if (task == HelpersLibMod.OutputEnum.Email)
                {
                    foreach (ToolStripMenuItem tsmiEmail in tsmiOutput.DropDownItems)
                    {
                        if (tsmiEmail.Checked)
                            emails.Add(tsmiEmail.Text);
                    }
                    break;
                }
            }

            if (emails.Count > 0)
                AddressBookHelper.CurrentRecipient = string.Join(",", emails.ToArray());
            else
                AddressBookHelper.CurrentRecipient = string.Empty;

            foreach (ToolStripMenuItem tsmiAfterUploadTask in tsddbAfterUploadTasks.DropDownItems)
            {
                AfterUploadTasks task = (AfterUploadTasks)tsmiAfterUploadTask.Tag;
                if (task == AfterUploadTasks.SendEmail)
                {
                    if (task == AfterUploadTasks.SendEmail)
                    {
                        tsmiAfterUploadTask.Enabled = tsmiAfterUploadTask.Checked = !string.IsNullOrEmpty(AddressBookHelper.CurrentRecipient);
                        if (!string.IsNullOrEmpty(AddressBookHelper.CurrentRecipient))
                            tsmiAfterUploadTask.Text = task.GetDescription() + " to " + AddressBookHelper.CurrentRecipient;
                        else
                            tsmiAfterUploadTask.Text = task.GetDescription();
                    }
                    break;
                }
            }
        }

        public void ReloadOutputsMenu()
        {
            #region Outputs

            var outputs = Enum.GetValues(typeof(HelpersLibMod.OutputEnum)).Cast<HelpersLibMod.OutputEnum>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            tsddbOutputs.DropDownItems.Clear();

            foreach (var output in outputs)
            {
                ToolStripMenuItem tsmiOutput = new ToolStripMenuItem(output.Description);
                tsmiOutput.Checked = SettingsManager.ConfigCore.Outputs.HasFlag(output.Enum);
                tsmiOutput.Tag = output.Enum;
                tsmiOutput.CheckOnClick = true;
                tsmiOutput.CheckedChanged += new EventHandler(tsmiOutputs_CheckedChanged);
                tsddbOutputs.DropDownItems.Add(tsmiOutput);

                if (output.Enum == HelpersLibMod.OutputEnum.Email)
                    GetAddressBook(tsmiOutput);
            }

            #endregion Outputs

            #region After Capture Tasks

            /*
            AddMultiEnumItems<Subtask>(x => SettingsManager.ConfigCore.Workflow.Subtasks = SettingsManager.ConfigCore.Workflow.Subtasks.Swap(x),
                tsddbAfterCaptureTasks);
            */

            var afterCaptureTasks = Enum.GetValues(typeof(Subtask)).Cast<Subtask>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            tsddbAfterCaptureTasks.DropDownItems.Clear();
            foreach (var task in afterCaptureTasks)
            {
                if (task.Enum == Subtask.None)
                    continue;

                ToolStripMenuItem tsmi = new ToolStripMenuItem(task.Description);
                tsmi.Checked = SettingsManager.ConfigCore.Workflow.Subtasks.HasFlag(task.Enum);
                tsmi.Tag = task.Enum;
                tsmi.CheckOnClick = true;
                tsmi.CheckedChanged += new EventHandler(tsmiAfterCaptureTask_CheckedChanged);
                tsddbAfterCaptureTasks.DropDownItems.Add(tsmi);
            }

            #endregion After Capture Tasks

            #region After Upload Tasks

            var afterUploadTasks = Enum.GetValues(typeof(AfterUploadTasks)).Cast<AfterUploadTasks>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            tsddbAfterUploadTasks.DropDownItems.Clear();

            foreach (var task in afterUploadTasks)
            {
                if (task.Enum == AfterUploadTasks.None)
                    continue;

                ToolStripMenuItem tsmi = new ToolStripMenuItem(task.Description);
                tsmi.Checked = SettingsManager.ConfigCore.Workflow.AfterUploadTasks.HasFlag(task.Enum);
                tsmi.Tag = task.Enum;
                tsmi.CheckOnClick = true;
                tsmi.CheckedChanged += new EventHandler(tsmiAfterUploadTasks_CheckedChanged);
                tsddbAfterUploadTasks.DropDownItems.Add(tsmi);
            }

            tsmiEmailAddress_CheckedChanged(null, null);

            #endregion
        }

        private void tsmiAfterUploadTasks_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
                SettingsManager.ConfigCore.Workflow.AfterUploadTasks |= (AfterUploadTasks)tsmi.Tag;
            else
                SettingsManager.ConfigCore.Workflow.AfterUploadTasks &= ~(AfterUploadTasks)tsmi.Tag;
        }

        private void tsmiAfterCaptureTask_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
                SettingsManager.ConfigCore.Workflow.Subtasks |= (Subtask)tsmi.Tag;
            else
                SettingsManager.ConfigCore.Workflow.Subtasks &= ~(Subtask)tsmi.Tag;
        }

        private void tsmiOutputs_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
                SettingsManager.ConfigCore.Outputs |= (HelpersLibMod.OutputEnum)tsmi.Tag;
            else
                SettingsManager.ConfigCore.Outputs &= ~(HelpersLibMod.OutputEnum)tsmi.Tag;

            tsddbDestinations.Visible = SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.RemoteHost);
        }

        private void InitControls()
        {
            InitializeComponent();

            this.Text = Program.Title;
            this.Icon = Resources.ShareX;
            niTray.Text = this.Text;
            niTray.Icon = Resources.ShareXSmallIcon;
            lvwColumnSorter = new ListViewColumnSorter();


            #region Uploaders

            AddEnumItems<ImageDestination>(x => SettingsManager.ConfigCore.ImageUploaderDestination = UploadManager.ImageUploader = (ImageDestination)x,
             true, tsmiImageUploaders, tsmiTrayImageUploaders);
            AddEnumItems<TextDestination>(x => SettingsManager.ConfigCore.TextUploaderDestination = UploadManager.TextUploader = (TextDestination)x,
             true, tsmiTextUploaders, tsmiTrayTextUploaders);
            AddEnumItems<FileDestination>(x => SettingsManager.ConfigCore.FileUploaderDestination = UploadManager.FileUploader = (FileDestination)x,
             true, tsmiFileUploaders, tsmiTrayFileUploaders);
            AddEnumItems<UrlShortenerType>(x => SettingsManager.ConfigCore.URLShortenerDestination = UploadManager.URLShortener = (UrlShortenerType)x,
             true, tsmiURLShorteners, tsmiTrayURLShorteners);
            AddEnumItems<SocialNetworkingService>(x => SettingsManager.ConfigCore.SocialNetworkingServiceDestination = UploadManager.SocialNetworkingService = (SocialNetworkingService)x,
            false, tsmiShare);

            foreach (ToolStripMenuItem tsmi in tsmiShare.DropDownItems)
            {
                tsmi.Click += new EventHandler(tsmiShare_Click);
            }

            #endregion Uploaders

            this.lvUploads.ListViewItemSorter = lvwColumnSorter;
            this.lvUploads.FillLastColumn();

            TaskManager.ListViewControl = lvUploads;
            uim = new UploadInfoManager(lvUploads);

            lvUploads.ColumnWidthChanged += new ColumnWidthChangedEventHandler(lvUploads_ColumnWidthChanged);

            if (Program.IsDebug)
                tsbDebug.Visible = true;
        }

        private void tsmiShare_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    UploadTask task = lvUploads.Items[index].Tag as UploadTask;
                    if (task != null && task.Info != null && task.Info.Result != null)
                    {
                        UploadResult result = task.Info.Result;
                        AfterCaptureActivity act = new AfterCaptureActivity();
                        act.Workflow.Settings.DestConfig.SocialNetworkingServices.Add((SocialNetworkingService)tsmi.Tag);
                        act.Workflow.AfterUploadTasks = AfterUploadTasks.ShareUsingSocialNetworkingService;
                        UploadManager.ShareUsingSocialNetworkingService(result, act);
                    }
                }
            }
        }

        private void AddMultiEnumItems<T>(Action<T> selectedEnum, params ToolStripDropDownItem[] parents)
        {
            string[] enums = Enum.GetValues(typeof(T)).Cast<Enum>().Skip(1).Select(x => x.GetDescription()).ToArray();

            foreach (ToolStripDropDownItem parent in parents)
            {
                parent.DropDownItems.Clear();

                for (int i = 0; i < enums.Length; i++)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(enums[i]);

                    int index = i;

                    tsmi.Tag = enums[i];
                    tsmi.Click += (sender, e) =>
                    {
                        foreach (ToolStripDropDownItem parent2 in parents)
                        {
                            ToolStripMenuItem tsmi2 = (ToolStripMenuItem)parent2.DropDownItems[index];
                            tsmi2.Checked = !tsmi2.Checked;
                        }

                        selectedEnum((T)Enum.ToObject(typeof(T), 1 << index));

                        UpdateUploaderMenuNames();
                    };

                    parent.DropDownItems.Add(tsmi);
                }
            }
        }


        private void AddEnumItems<T>(Action<int> selectedIndex, bool addEvent = true, params ToolStripMenuItem[] parents)
        {
            int enumLength = Helpers.GetEnumLength<T>();

            foreach (ToolStripMenuItem parent in parents)
            {
                for (int i = 0; i < enumLength; i++)
                {
                    Enum myEnum = (Enum)Enum.ToObject(typeof(T), i);
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(myEnum.GetDescription()) { Tag = myEnum };

                    if (addEvent)
                    {
                        int index = i;
                        tsmi.Click += (sender, e) =>
                        {
                            foreach (ToolStripMenuItem parent2 in parents)
                            {
                                for (int i2 = 0; i2 < enumLength; i2++)
                                {
                                    ToolStripMenuItem tsmi2 = (ToolStripMenuItem)parent2.DropDownItems[i2];
                                    tsmi2.Checked = index == i2;
                                }
                            }

                            selectedIndex(index);

                            UpdateUploaderMenuNames();
                        };
                    }
                    parent.DropDownItems.Add(tsmi);
                }
            }
        }

        private void UpdateControls()
        {
            // cmsUploadInfo.SuspendLayout();

            tsmiStopUpload.Visible = tsmiOpen.Visible = tsmiCopy.Visible = tsmiShowErrors.Visible = tsmiShowResponse.Visible =
              tsmiUploadSelectedFile.Visible = tsmiClearList.Visible = tssUploadInfo1.Visible = tsmiShare.Visible = false;

            uim.RefreshSelectedItems();

            if (lvUploads.SelectedItems.Count > 0)
            {
                if (GetCurrentTasks().Any(x => x.IsWorking))
                {
                    tsmiStopUpload.Visible = true;
                }
                else
                {
                    if (uim.IsSelectedItemsValid())
                    {
                        if (uim.SelectedItems[0].Info.Result.IsError)
                        {
                            tsmiShowErrors.Visible = true;
                        }
                        else
                        {
                            // Open
                            tsmiOpen.Visible = true;

                            tsmiOpenURL.Enabled = uim.SelectedItems[0].IsURLExist;
                            tsmiOpenShortenedURL.Enabled = uim.SelectedItems[0].IsShortenedURLExist;
                            tsmiOpenThumbnailURL.Enabled = uim.SelectedItems[0].IsThumbnailURLExist;
                            tsmiOpenDeletionURL.Enabled = uim.SelectedItems[0].IsDeletionURLExist;

                            tsmiOpenFile.Enabled = uim.SelectedItems[0].IsFileExist;
                            tsmiOpenFolder.Enabled = uim.SelectedItems[0].IsFileExist;

                            // Copy
                            tsmiCopy.Visible = true;

                            tsmiCopyURL.Enabled = uim.SelectedItems.Any(x => x.IsURLExist);
                            tsmiCopyShortenedURL.Enabled = uim.SelectedItems.Any(x => x.IsShortenedURLExist);
                            tsmiCopyThumbnailURL.Enabled = uim.SelectedItems.Any(x => x.IsThumbnailURLExist);
                            tsmiCopyDeletionURL.Enabled = uim.SelectedItems.Any(x => x.IsDeletionURLExist);

                            tsmiCopyFile.Enabled = uim.SelectedItems[0].IsFileExist;
                            tsmiCopyImage.Enabled = uim.SelectedItems[0].IsImageFile;
                            tsmiCopyText.Enabled = uim.SelectedItems[0].IsTextFile;

                            tsmiCopyHTMLLink.Enabled = uim.SelectedItems.Any(x => x.IsURLExist);
                            tsmiCopyHTMLImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL);
                            tsmiCopyHTMLLinkedImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL && x.IsThumbnailURLExist);

                            tsmiCopyForumLink.Enabled = uim.SelectedItems.Any(x => x.IsURLExist);
                            tsmiCopyForumImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL && x.IsURLExist);
                            tsmiCopyForumLinkedImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL && x.IsThumbnailURLExist);

                            tsmiCopyFilePath.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);
                            tsmiCopyFileName.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);
                            tsmiCopyFileNameWithExtension.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);
                            tsmiCopyFolder.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);

                            tsmiShare.Visible = uim.SelectedItems[0].IsURLExist;
                        }

                        if ((uim.SelectedItems[0].Info.Result.IsError || Program.IsDebug) && !string.IsNullOrEmpty(uim.SelectedItems[0].Info.Result.Response))
                        {
                            tsmiShowResponse.Visible = true;
                        }

                        tsmiViewInFullscreen.Visible = uim.SelectedItems[0].IsImageFile;
                        tsmiUploadSelectedFile.Visible = uim.SelectedItems[0].IsFileExist;
                    }
                }
            }
        }

        private void UpdateUploaderMenuFileUploaderName(ToolStripMenuItem tsmi)
        {
            tsmi.DropDownItems[tsmi.DropDownItems.Count - 1].Text = UploadManager.FileUploader.GetDescription() + " (file uploader)";
        }

        private void UpdateUploaderMenuNames()
        {
            UpdateUploaderMenuFileUploaderName(tsmiImageUploaders);
            UpdateUploaderMenuFileUploaderName(tsmiTextUploaders);
            UpdateUploaderMenuFileUploaderName(tsmiTrayImageUploaders);
            UpdateUploaderMenuFileUploaderName(tsmiTrayTextUploaders);

            tsmiImageUploaders.Text = "Image uploader: ";
            if (UploadManager.ImageUploader == ImageDestination.FileUploader)
                tsmiTrayImageUploaders.Text = tsmiImageUploaders.Text += UploadManager.FileUploader.GetDescription() + " (via file uploader)";
            else
                tsmiTrayImageUploaders.Text = tsmiImageUploaders.Text += UploadManager.ImageUploader.GetDescription();

            tsmiTextUploaders.Text = "Text uploader: ";
            if (UploadManager.TextUploader == TextDestination.FileUploader)
                tsmiTrayTextUploaders.Text = tsmiTextUploaders.Text += UploadManager.FileUploader.GetDescription() + " (via file uploader)";
            else
                tsmiTrayTextUploaders.Text = tsmiTextUploaders.Text += UploadManager.TextUploader.GetDescription();

            tsmiTrayFileUploaders.Text = tsmiFileUploaders.Text = "File uploader: " + UploadManager.FileUploader.GetDescription();
            tsmiTrayURLShorteners.Text = tsmiURLShorteners.Text = "URL shortener: " + UploadManager.URLShortener.GetDescription();
        }

        private void CheckUpdate()
        {
            UpdateChecker updateChecker = new UpdateChecker(Program.URL_UPDATE, Application.ProductName, new Version(Program.AssemblyVersion),
                ReleaseChannelType.Stable, Uploader.ProxySettings.GetWebProxy);
            updateChecker.CheckUpdate();

            if (updateChecker.UpdateInfo != null && updateChecker.UpdateInfo.Status == UpdateStatus.UpdateRequired && !string.IsNullOrEmpty(updateChecker.UpdateInfo.URL))
            {
                NewVersionWindowOptions nvwo = new NewVersionWindowOptions()
                {
                    MyIcon = this.Icon,
                    MyImage = Resources.ShareXLogo,
                    ProjectName = Application.ProductName,
                    UpdateInfo = updateChecker.UpdateInfo
                };

                UpdaterForm dlg = new UpdaterForm(nvwo);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    DownloaderForm downloader = new DownloaderForm(updateChecker.UpdateInfo.URL, updateChecker.Proxy, updateChecker.UpdateInfo.Summary);
                    downloader.ShowDialog();
                    if (downloader.Status == DownloaderFormStatus.InstallStarted) Application.Exit();
                }
            }
        }

        public void UseCommandLineArgs(string[] args)
        {
            if (args != null && args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].Equals("-clipboardupload", StringComparison.InvariantCultureIgnoreCase))
                    {
                        UploadManager.ClipboardUpload();
                    }
                    else if (args[i][0] != '-')
                    {
                        UploadManager.UploadFile(args[i]);
                    }
                }
            }
        }

        private UploadTask[] GetCurrentTasks()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                return lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploadTask).Where(x => x != null).ToArray();
            }

            return null;
        }

        private UploadInfo GetCurrentUploadInfo()
        {
            UploadInfo info = null;
            UploadTask[] tasks = GetCurrentTasks();

            if (tasks != null && tasks.Length > 0)
            {
                info = tasks[0].Info;
            }

            return info;
        }

        private UploadResult GetCurrentUploadResult()
        {
            UploadResult result = null;

            if (lvUploads.SelectedItems.Count > 0)
            {
                result = lvUploads.SelectedItems[0].Tag as UploadResult;
            }

            return result;
        }

        private string GetErrors()
        {
            string errors = string.Empty;
            UploadResult result = GetCurrentUploadResult();

            if (result != null && result.IsError)
            {
                errors = string.Join("\r\n\r\n", result.Errors.ToArray());
            }

            return errors;
        }

        private void ShowErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                Exception e = new Exception("Upload errors: " + errors);
                new ErrorForm(Application.ProductName, e, new Logger(), Program.LogFilePath, Program.URL_ISSUES).ShowDialog();
            }
        }

        private void CopyErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                Helpers.CopyTextSafely(errors);
            }
        }

        private void ShowResponse()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.Response))
            {
                ResponseForm form = new ResponseForm(result.Response);
                form.Icon = this.Icon;
                form.Show();
            }
        }

        private void RemoveSelectedItems()
        {
            lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploadTask).Where(x => x != null && !x.IsWorking).ForEach(x => TaskManager.Remove(x));
        }

        private void RemoveAllItems()
        {
            lvUploads.Items.Cast<ListViewItem>().Select(x => x.Tag as UploadTask).Where(x => x != null && !x.IsWorking).ForEach(x => TaskManager.Remove(x));
        }

        public void ShowActivate()
        {
            if (!Visible)
            {
                Show();
            }

            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            BringToFront();
            Activate();
        }

        #region Form events

        protected override void SetVisibleCore(bool value)
        {
            if (value && !IsHandleCreated)
            {
                if (Program.IsSilentRun && SettingsManager.ConfigCore.ShowTray)
                {
                    CreateHandle();
                    value = false;
                }

                AfterLoadJobs();
            }

            base.SetVisibleCore(value);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            AfterShownJobs();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && SettingsManager.ConfigCore.ShowTray && !trayClose)
            {
                e.Cancel = true;
                Hide();

                if (SettingsManager.ConfigCore.DropboxSync)
                {
                    new DropboxSyncHelper().Save();
                }
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) ||
                e.Data.GetDataPresent(DataFormats.Bitmap, false) ||
                e.Data.GetDataPresent(DataFormats.Text, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            UploadManager.DragDropUpload(e.Data);
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.ClipboardUploadWithContentViewer();
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
        }

        private void tsmiTestImageUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadImage(Resources.ShareXLogo);
        }

        private void tsmiTestTextUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadText(Application.ProductName + " - text upload test");
        }

        private void tsmiTestShapeCapture_Click(object sender, EventArgs e)
        {
            new RegionCapturePreview(SettingsManager.ConfigCore.SurfaceOptions).Show();
        }

        private void tsddbDestinations_DropDownOpening(object sender, EventArgs e)
        {
            UpdateUploaderMenuNames();
            AfterUploadersConfigClosed();
        }

        private void tsddbUploadersConfig_Click(object sender, EventArgs e)
        {
            FormsHelper.ShowUploadersConfig();
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            HistoryManager.ConvertHistoryToNewFormat(SettingsManager.HistoryFilePath, SettingsManager.OldHistoryFilePath);
            using (HistoryForm historyForm = new HistoryForm(SettingsManager.HistoryFilePath))
            {
                historyForm.Icon = Icon;
                historyForm.ShowDialog();
            }
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            new AboutForm() { Icon = this.Icon }.ShowDialog();
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            Helpers.LoadBrowserAsync(Program.URL_DONATE);
        }

        private void lvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void lvUploads_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UpdateControls();
                cmsUploadInfo.Show(lvUploads, e.X + 1, e.Y + 1);
            }
        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
        }

        private void stopUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    TaskManager.Tasks[index].Stop();
                }
            }
        }

        private void showInWindowsExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    UploadResult result = lvUploads.Items[index].Tag as UploadResult;
                    Helpers.OpenFolderWithFile(result.LocalFilePath);
                }
            }
        }

        private void tsmiContextMenuUpload_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    UploadResult result = lvUploads.Items[index].Tag as UploadResult;
                    AfterCaptureActivity actFileUpload = AfterCaptureActivity.GetNew();
                    actFileUpload.Workflow.Subtasks |= Subtask.UploadToRemoteHost;
                    UploadManager.UploadFile(result.LocalFilePath, actFileUpload);
                }
            }
        }

        private void viewInFullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uim.TryView();
        }

        private void copyImageToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;
            if (File.Exists(result.LocalFilePath))
            {
                Clipboard.SetImage(HelpersMod.ImageFromFile(result.LocalFilePath));
            }
        }

        private void lvUploads_DoubleClick(object sender, EventArgs e)
        {
            uim.TryOpen();
        }

        #region Tray events

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowActivate();
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            string url = niTray.Tag as string;

            if (!string.IsNullOrEmpty(url))
            {
                Helpers.LoadBrowserAsync(url);
            }
        }

        private void tsmiTrayExit_Click(object sender, EventArgs e)
        {
            trayClose = true;
            Close();
        }

        private void tsmiTraySettings_Click(object sender, EventArgs e)
        {
            FormsHelper.ShowOptions();
        }

        #endregion Tray events

        private void tsmiDebugOpen_Click(object sender, EventArgs e)
        {
            FormsHelper.ShowLog();
        }

        private void lvUploads_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (SettingsManager.ConfigCore.ColumnWidths.Length > e.ColumnIndex)
                SettingsManager.ConfigCore.ColumnWidths[e.ColumnIndex] = lvUploads.Columns[e.ColumnIndex].Width;
        }

        #endregion Form events

        private void tsmiWorkflows_Click(object sender, EventArgs e)
        {
            // Hotkeys
            if (Program.IsHotkeysAllowed)
            {
                FormsHelper.ShowWorkflowManager();
            }
        }

        #region Main Window Menu events

        private void tsmiFullscreen_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.FullScreen));
        }

        private void tsddbCapture_DropDownOpening(object sender, EventArgs e)
        {
            PrepareWindowsMenu(tsmiWindow, tsmiWindowItems_Click);
        }

        private void tsmiWindowItems_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            WindowInfo wi = tsi.Tag as WindowInfo;
            if (wi != null) AfterCapture(CaptureWindow(wi.Handle));
        }

        private void tsmiWindowRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.WindowRectangle));
        }

        private void tsmiRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.RectangleRegion));
        }

        private void tsmiRoundedRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.RoundedRectangleRegion));
        }

        private void tsmiEllipse_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.EllipseRegion));
        }

        private void tsmiTriangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.TriangleRegion));
        }

        private void tsmiDiamond_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.DiamondRegion));
        }

        private void tsmiPolygon_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.PolygonRegion));
        }

        private void tsmiFreeHand_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.FreeHandRegion));
        }

        private void tsmiLastRegion_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.LastRegion));
        }

        private void screencastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.Screencast));
        }

        #endregion Main Window Menu events

        #region Tray events

        private void tsmiTrayFullscreen_Click(object sender, EventArgs e)
        {
            tsmiFullscreen_Click(sender, e);
        }

        private void tsmiCapture_DropDownOpening(object sender, EventArgs e)
        {
            PrepareWindowsMenu(tsmiTrayWindow, tsmiTrayWindowItems_Click);
        }

        private void tsmiTrayWindowItems_Click(object sender, EventArgs e)
        {
            tsmiWindowItems_Click(sender, e);
        }

        private void tsmiTrayWindowRectangle_Click(object sender, EventArgs e)
        {
            tsmiWindowRectangle_Click(sender, e);
        }

        private void tsmiTrayRectangle_Click(object sender, EventArgs e)
        {
            tsmiRectangle_Click(sender, e);
        }

        private void tsmiTrayRoundedRectangle_Click(object sender, EventArgs e)
        {
            tsmiRoundedRectangle_Click(sender, e);
        }

        private void tsmiTrayEllipse_Click(object sender, EventArgs e)
        {
            tsmiEllipse_Click(sender, e);
        }

        private void tsmiTrayTriangle_Click(object sender, EventArgs e)
        {
            tsmiTriangle_Click(sender, e);
        }

        private void tsmiTrayDiamond_Click(object sender, EventArgs e)
        {
            tsmiDiamond_Click(sender, e);
        }

        private void tsmiTrayPolygon_Click(object sender, EventArgs e)
        {
            tsmiPolygon_Click(sender, e);
        }

        private void tsmiTrayFreeHand_Click(object sender, EventArgs e)
        {
            tsmiFreeHand_Click(sender, e);
        }

        private void tsmiTrayLastRegion_Click(object sender, EventArgs e)
        {
            tsmiLastRegion_Click(sender, e);
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            tsmiTraySettings_Click(sender, e);
        }

        private void tsmiWatermark_Click(object sender, EventArgs e)
        {
            WatermarkUI ui = new WatermarkUI(SettingsManager.ConfigUser.ConfigWatermark)
            {
                Icon = this.Icon
            };
            ui.Show();
        }

        private void tsmiTrayScreencast_Click(object sender, EventArgs e)
        {
            screencastToolStripMenuItem_Click(sender, e);
        }

        private void screenshotsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helpers.OpenFolder(Program.ScreenshotsPath);
        }

        #endregion Tray events

        private void tsmiOpenURL_Click(object sender, EventArgs e)
        {
            uim.OpenURL();
        }

        private void tsmiOpenShortenedURL_Click(object sender, EventArgs e)
        {
            uim.OpenShortenedURL();
        }

        private void tsmiOpenThumbnailURL_Click(object sender, EventArgs e)
        {
            uim.OpenThumbnailURL();
        }

        private void tsmiOpenDeletionURL_Click(object sender, EventArgs e)
        {
            uim.OpenDeletionURL();
        }

        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            uim.OpenFile();
        }

        private void tsmiOpenFolder_Click(object sender, EventArgs e)
        {
            uim.OpenFolder();
        }

        private void tsmiCopyURL_Click(object sender, EventArgs e)
        {
            uim.CopyURL();
        }

        private void tsmiCopyShortenedURL_Click(object sender, EventArgs e)
        {
            uim.CopyShortenedURL();
        }

        private void tsmiCopyThumbnailURL_Click(object sender, EventArgs e)
        {
            uim.CopyThumbnailURL();
        }

        private void tsmiCopyDeletionURL_Click(object sender, EventArgs e)
        {
            uim.CopyDeletionURL();
        }

        private void tsmiCopyFile_Click(object sender, EventArgs e)
        {
            uim.CopyFile();
        }

        private void tsmiCopyImage_Click(object sender, EventArgs e)
        {
            uim.CopyImage();
        }

        private void tsmiCopyText_Click(object sender, EventArgs e)
        {
            uim.CopyText();
        }

        private void tsmiCopyHTMLLink_Click(object sender, EventArgs e)
        {
            uim.CopyHTMLLink();
        }

        private void tsmiCopyHTMLImage_Click(object sender, EventArgs e)
        {
            uim.CopyHTMLImage();
        }

        private void tsmiCopyHTMLLinkedImage_Click(object sender, EventArgs e)
        {
            uim.CopyHTMLLinkedImage();
        }

        private void tsmiCopyForumLink_Click(object sender, EventArgs e)
        {
            uim.CopyForumLink();
        }

        private void tsmiCopyForumImage_Click(object sender, EventArgs e)
        {
            uim.CopyForumImage();
        }

        private void tsmiCopyForumLinkedImage_Click(object sender, EventArgs e)
        {
            uim.CopyForumLinkedImage();
        }

        private void tsmiCopyFilePath_Click(object sender, EventArgs e)
        {
            uim.CopyFilePath();
        }

        private void tsmiCopyFileName_Click(object sender, EventArgs e)
        {
            uim.CopyFileName();
        }

        private void tsmiCopyFileNameWithExtension_Click(object sender, EventArgs e)
        {
            uim.CopyFileNameWithExtension();
        }

        private void tsmiCopyFolder_Click(object sender, EventArgs e)
        {
            uim.CopyFolder();
        }

        private void tsmiShowErrors_Click(object sender, EventArgs e)
        {
            uim.ShowErrors();
        }

        private void tsmiShowResponse_Click(object sender, EventArgs e)
        {
            uim.ShowResponse();
        }

        private void tsmiUploadSelectedFile_Click(object sender, EventArgs e)
        {
            uim.Upload();
        }

        private void tsmiClearList_Click(object sender, EventArgs e)
        {
            RemoveAllItems();
        }

        private void tsmiUploadFile_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
        }

        private void lvUploads_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                default: return;
                case Keys.Enter:
                    uim.TryOpen();
                    break;

                case Keys.Control | Keys.Enter:
                    uim.OpenFile();
                    break;

                case Keys.Control | Keys.C:
                    uim.CopyURL();
                    break;

                case Keys.Delete:
                    RemoveSelectedItems();
                    break;
            }

            e.Handled = true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaskManager.StopAllTasks();
        }

        private void lvUploads_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvUploads.Sort();
        }
    }
}