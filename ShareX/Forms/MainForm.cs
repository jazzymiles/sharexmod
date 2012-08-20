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

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using HistoryLib;
using ScreenCapture;
using ShareX.Forms;
using ShareX.HelperClasses;
using ShareX.Properties;
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
            FolderWatcher folderWatcher = new FolderWatcher(this);
            folderWatcher.FolderPath = SettingsManager.ConfigCore.FolderMonitorPath;
            if (SettingsManager.ConfigCore.FolderMonitoring)
            {
                folderWatcher.StartWatching();
            }
            else
            {
                folderWatcher.StopWatching();
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

        public void LoadSettings()
        {
            niTray.Visible = SettingsManager.ConfigCore.ShowTray;

            for (int x = 0; x < SettingsManager.ConfigCore.ColumnWidths.Length; x++)
            {
                lvUploads.Columns[x].Width = SettingsManager.ConfigCore.ColumnWidths[x];
            }

            ReloadOutputsMenu();

            #region Upload Destinations

            int imageUploaderIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.ImageUploaderDestination);
            ((ToolStripMenuItem)tsmiImageUploaders.DropDownItems[imageUploaderIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayImageUploaders.DropDownItems[imageUploaderIndex]).Checked = true;
            UploadManager.ImageUploader = SettingsManager.ConfigCore.ImageUploaderDestination;

            int textUploaderIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.TextUploaderDestination);
            ((ToolStripMenuItem)tsmiTextUploaders.DropDownItems[textUploaderIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayTextUploaders.DropDownItems[textUploaderIndex]).Checked = true;
            UploadManager.TextUploader = SettingsManager.ConfigCore.TextUploaderDestination;

            int fileUploaderIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.FileUploaderDestination);
            ((ToolStripMenuItem)tsmiFileUploaders.DropDownItems[fileUploaderIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayFileUploaders.DropDownItems[fileUploaderIndex]).Checked = true;
            UploadManager.FileUploader = SettingsManager.ConfigCore.FileUploaderDestination;

            int urlShortenerIndex = Helpers.GetEnumMemberIndex(SettingsManager.ConfigCore.URLShortenerDestination);
            ((ToolStripMenuItem)tsmiURLShorteners.DropDownItems[urlShortenerIndex]).Checked = true;
            ((ToolStripMenuItem)tsmiTrayURLShorteners.DropDownItems[urlShortenerIndex]).Checked = true;
            UploadManager.URLShortener = SettingsManager.ConfigCore.URLShortenerDestination;

            UpdateUploaderMenuNames();

            #endregion Upload Destinations

            UploadManager.UpdateProxySettings();
        }

        public void ReloadOutputsMenu()
        {
            #region Outputs

            var outputs = Enum.GetValues(typeof(OutputEnum)).Cast<OutputEnum>().Select(x => new
    {
        Description = x.GetDescription(),
        Enum = x
    });

            tsddbOutputs.DropDownItems.Clear();

            foreach (var output in outputs)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(output.Description);
                tsmi.Checked = SettingsManager.ConfigCore.Outputs.HasFlag(output.Enum);
                tsmi.Tag = output.Enum;
                tsmi.CheckOnClick = true;
                tsmi.CheckedChanged += new EventHandler(tsmiOutputs_CheckedChanged);
                tsddbOutputs.DropDownItems.Add(tsmi);
            }

            #endregion Outputs

            #region After Capture Tasks

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
                tsmi.Checked = SettingsManager.ConfigCore.AfterCaptureTasks.HasFlag(task.Enum);
                tsmi.Tag = task.Enum;
                tsmi.CheckOnClick = true;
                tsmi.CheckedChanged += new EventHandler(tsmiAfterCaptureTask_CheckedChanged);
                tsddbAfterCaptureTasks.DropDownItems.Add(tsmi);
            }

            #endregion After Capture Tasks

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
                tsmi.Checked = SettingsManager.ConfigCore.AfterUploadTasks.HasFlag(task.Enum);
                tsmi.Tag = task.Enum;
                tsmi.CheckOnClick = true;
                tsmi.CheckedChanged += new EventHandler(tsmiAfterUploadTasks_CheckedChanged);
                tsddbAfterUploadTasks.DropDownItems.Add(tsmi);
            }
        }

        private void tsmiAfterUploadTasks_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
                SettingsManager.ConfigCore.AfterUploadTasks |= (AfterUploadTasks)tsmi.Tag;
            else
                SettingsManager.ConfigCore.AfterUploadTasks &= ~(AfterUploadTasks)tsmi.Tag;
        }

        private void tsmiAfterCaptureTask_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
                SettingsManager.ConfigCore.AfterCaptureTasks |= (Subtask)tsmi.Tag;
            else
                SettingsManager.ConfigCore.AfterCaptureTasks &= ~(Subtask)tsmi.Tag;
        }

        private void tsmiOutputs_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
                SettingsManager.ConfigCore.Outputs |= (OutputEnum)tsmi.Tag;
            else
                SettingsManager.ConfigCore.Outputs &= ~(OutputEnum)tsmi.Tag;

            tsddbDestinations.Visible = SettingsManager.ConfigCore.Outputs.HasFlag(OutputEnum.RemoteHost);
        }

        private void InitControls()
        {
            InitializeComponent();

            this.Text = Program.Title;
            this.Icon = Resources.ShareX;
            niTray.Text = this.Text;
            niTray.Icon = Resources.ShareXSmallIcon;

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
            false, tsmiContextMenuShare);

            foreach (ToolStripMenuItem tsmi in tsmiContextMenuShare.DropDownItems)
            {
                tsmi.Click += new EventHandler(tsmiContextMenuShare_Click);
            }

            #endregion Uploaders

            lvUploads.FillLastColumn();

            TaskManager.ListViewControl = lvUploads;

            lvUploads.ColumnWidthChanged += new ColumnWidthChangedEventHandler(lvUploads_ColumnWidthChanged);

            if (Program.IsDebug)
                tsbDebug.Visible = true;
        }

        private void tsmiContextMenuShare_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    UploadResult result = lvUploads.Items[index].Tag as UploadResult;
                    AfterCaptureActivity act = new AfterCaptureActivity();
                    act.Workflow.Settings.DestConfig.SocialNetworkingServices.Add((SocialNetworkingService)tsmi.Tag);
                    act.Workflow.AfterUploadTasks = AfterUploadTasks.ShareUsingSocialNetworkingService;
                    UploadManager.ShareUsingSocialNetworkingService(result, act);
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
            int itemsCount = lvUploads.SelectedItems.Count;

            if (itemsCount > 0)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.URL))
                    {
                        if (itemsCount > 1)
                        {
                            copyURLToolStripMenuItem.Text = string.Format("Copy URLs ({0})", itemsCount);
                        }
                        else
                        {
                            copyURLToolStripMenuItem.Text = "Copy URL";
                        }
                    }

                    if (!string.IsNullOrEmpty(result.ThumbnailURL))
                    {
                        copyThumbnailURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.DeletionURL))
                    {
                        copyDeletionURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.ShortenedURL))
                    {
                        copyShortenedURLToolStripMenuItem.Visible = true;
                    }

                    if (result.IsError)
                    {
                        showErrorsToolStripMenuItem.Visible = true;
                        copyErrorsToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.Source))
                    {
                        showResponseToolStripMenuItem.Visible = true;
                    }

                    showInWindowsExplorerToolStripMenuItem.Visible = tsmiContextMenuShare.Visible = File.Exists(result.LocalFilePath);
                    copyImageToClipboardToolStripMenuItem.Visible = viewInFullscreenToolStripMenuItem.Visible = File.Exists(result.LocalFilePath) && Helpers.IsImageFile(result.LocalFilePath);
                    tsmiUpload.Visible = File.Exists(result.LocalFilePath);
                }

                int index = lvUploads.SelectedIndices[0];
                tsmiStopUpload.Visible = TaskManager.Tasks[index].Status != TaskStatus.Completed;
            }
            else
            {
                uploadFileToolStripMenuItem.Visible = true;
                showInWindowsExplorerToolStripMenuItem.Visible = false;
                tsmiUpload.Visible = false;
            }
        }

        private void UpdateUploaderMenuFileUploaderName(ToolStripMenuItem tsmi)
        {
            tsmi.DropDownItems[tsmi.DropDownItems.Count - 1].Text = UploadManager.FileUploader.GetDescription();
        }

        private void UpdateUploaderMenuNames()
        {
            UpdateUploaderMenuFileUploaderName(tsmiImageUploaders);
            UpdateUploaderMenuFileUploaderName(tsmiTextUploaders);
            UpdateUploaderMenuFileUploaderName(tsmiTrayImageUploaders);
            UpdateUploaderMenuFileUploaderName(tsmiTrayTextUploaders);

            tsmiImageUploaders.Text = "Image uploader: ";
            if (UploadManager.ImageUploader == ImageDestination.FileUploader)
                tsmiTrayImageUploaders.Text = tsmiImageUploaders.Text += UploadManager.FileUploader.GetDescription();
            else
                tsmiTrayImageUploaders.Text = tsmiImageUploaders.Text += UploadManager.ImageUploader.GetDescription();

            tsmiTextUploaders.Text = "Text uploader: ";
            if (UploadManager.TextUploader == TextDestination.FileUploader)
                tsmiTrayTextUploaders.Text = tsmiTextUploaders.Text += UploadManager.FileUploader.GetDescription();
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

        private UploadResult GetCurrentUploadResult()
        {
            UploadResult result = null;

            if (lvUploads.SelectedItems.Count > 0)
            {
                result = lvUploads.SelectedItems[0].Tag as UploadResult;
            }

            return result;
        }

        private void OpenItem()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.URL))
            {
                OpenItem(SettingsManager.ConfigUser.ItemsWithUrlOnItemDoubleClick, result.URL, result.LocalFilePath);
            }
            else if (result != null && File.Exists(result.LocalFilePath))
            {
                OpenItem(SettingsManager.ConfigUser.ItemsWithoutUrlOnItemDoubleClick, result.URL, result.LocalFilePath);
            }
        }

        private void OpenItem(EListItemDoubleClickBehavior behavior, string link, string filepath)
        {
            switch (behavior)
            {
                case EListItemDoubleClickBehavior.DoNothing:
                    break;

                case EListItemDoubleClickBehavior.OpenDirectory:
                    if (Directory.Exists(Path.GetDirectoryName(filepath)))
                        Helpers.OpenFolderWithFile(filepath);
                    break;

                case EListItemDoubleClickBehavior.OpenFile:
                    if (File.Exists(filepath))
                        Process.Start(filepath);
                    break;

                case EListItemDoubleClickBehavior.OpenFileOrUrl:
                    if (File.Exists(filepath))
                        Process.Start(filepath);
                    else if (!string.IsNullOrEmpty(link))
                        Helpers.LoadBrowserAsync(link);
                    break;

                case EListItemDoubleClickBehavior.OpenUrl:
                    if (!string.IsNullOrEmpty(link))
                        Helpers.LoadBrowserAsync(link);
                    break;

                case EListItemDoubleClickBehavior.OpenUrlOrFile:
                    if (!string.IsNullOrEmpty(link))
                        Helpers.LoadBrowserAsync(link);
                    else if (File.Exists(filepath))
                        Process.Start(filepath);
                    break;
            }
        }

        private void CopyURL()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                string[] array = lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploadResult).
                    Where(x => x != null && !string.IsNullOrEmpty(x.URL)).Select(x => x.URL).ToArray();

                if (array != null && array.Length > 0)
                {
                    string urls = string.Join("\r\n", array);

                    if (!string.IsNullOrEmpty(urls))
                    {
                        Helpers.CopyTextSafely(urls);
                    }
                }
            }
        }

        private void CopyShortenedURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.ShortenedURL))
            {
                Helpers.CopyTextSafely(result.ShortenedURL);
            }
        }

        private void CopyThumbnailURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.ThumbnailURL))
            {
                Helpers.CopyTextSafely(result.ThumbnailURL);
            }
        }

        private void CopyDeletionURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.DeletionURL))
            {
                Helpers.CopyTextSafely(result.DeletionURL);
            }
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

            if (result != null && !string.IsNullOrEmpty(result.Source))
            {
                ResponseForm form = new ResponseForm(result.Source);
                form.Icon = this.Icon;
                form.Show();
            }
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
            using (HistoryForm historyForm = new HistoryForm(SettingsManager.HistoryFilePath, Application.ProductName + " History: " + SettingsManager.HistoryFilePath))
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
                cmsUploads.Show(lvUploads, e.X + 1, e.Y + 1);
            }
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenItem();
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void copyShortenedURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyShortenedURL();
        }

        private void copyThumbnailURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyThumbnailURL();
        }

        private void copyDeletionURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyDeletionURL();
        }

        private void showErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowErrors();
        }

        private void copyErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyErrors();
        }

        private void showResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowResponse();
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
                    UploadManager.UploadFile(result.LocalFilePath, new HelperClasses.AfterCaptureActivity()
                    {
                        Workflow = new Workflow() { Subtasks = Subtask.UploadToRemoteHost }
                    });
                }
            }
        }

        private void viewInFullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;
            if (File.Exists(result.LocalFilePath))
            {
                using (ImageViewer viewer = new ImageViewer(HelpersMod.ImageFromFile(result.LocalFilePath)))
                {
                    viewer.ShowDialog();
                }
            }
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
            OpenItem();
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
    }
}