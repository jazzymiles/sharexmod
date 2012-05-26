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
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using HistoryLib;
using ScreenCapture;
using ShareX.Forms;
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

            if (!SettingsManager.ConfigCore.DropboxSync)
            {
                InitHotkeys();
            }
            else
            {
                new DropboxSyncHelper().InitHotkeys();
            }

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
                SettingsManager.UploaderSettingsResetEvent.WaitOne();

            EnableDisableToolStripMenuItems(tsmiImageUploaders);
            EnableDisableToolStripMenuItems(tsmiTextUploaders);
            EnableDisableToolStripMenuItems(tsmiFileUploaders);
        }

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
        }

        private void ClearToolStripMenuItemChecks(ToolStripMenuItem tsmi)
        {
            foreach (ToolStripItem tsi in tsmi.DropDownItems)
            {
                if (tsi.GetType() == typeof(ToolStripMenuItem))
                    ((ToolStripMenuItem)tsi).Checked = false;
            }
        }

        private void EnableDisableToolStripMenuItems(ToolStripMenuItem tsmi)
        {
            foreach (ToolStripItem tsi in tsmi.DropDownItems)
            {
                if (tsi.GetType() == typeof(ToolStripMenuItem))
                {
                    if (tsi.Tag is ImageDestination)
                        ((ToolStripMenuItem)tsi).Enabled = SettingsManager.ConfigUploaders.IsActive(((ImageDestination)tsi.Tag));
                    else if (tsi.Tag is TextDestination)
                        ((ToolStripMenuItem)tsi).Enabled = SettingsManager.ConfigUploaders.IsActive(((TextDestination)tsi.Tag));
                    else if (tsi.Tag is FileDestination)
                        ((ToolStripMenuItem)tsi).Enabled = SettingsManager.ConfigUploaders.IsActive(((FileDestination)tsi.Tag));
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

            if (Helpers.GetEnumLength<ImageDestination>() <= SettingsManager.ConfigCore.SelectedImageUploaderDestination)
            {
                SettingsManager.ConfigCore.SelectedImageUploaderDestination = 0;
            }

            ClearToolStripMenuItemChecks(tsmiImageUploaders);
            ((ToolStripMenuItem)tsmiImageUploaders.DropDownItems[SettingsManager.ConfigCore.SelectedImageUploaderDestination]).Checked = true;
            UploadManager.ImageUploader = (ImageDestination)SettingsManager.ConfigCore.SelectedImageUploaderDestination;

            if (Helpers.GetEnumLength<FileDestination>() <= SettingsManager.ConfigCore.SelectedFileUploaderDestination)
            {
                SettingsManager.ConfigCore.SelectedFileUploaderDestination = 0;
            }

            ClearToolStripMenuItemChecks(tsmiFileUploaders);
            ((ToolStripMenuItem)tsmiFileUploaders.DropDownItems[SettingsManager.ConfigCore.SelectedFileUploaderDestination]).Checked = true;
            UploadManager.FileUploader = (FileDestination)SettingsManager.ConfigCore.SelectedFileUploaderDestination;

            if (Helpers.GetEnumLength<TextDestination>() <= SettingsManager.ConfigCore.SelectedTextUploaderDestination)
            {
                SettingsManager.ConfigCore.SelectedTextUploaderDestination = 0;
            }

            ClearToolStripMenuItemChecks(tsmiTextUploaders);
            ((ToolStripMenuItem)tsmiTextUploaders.DropDownItems[SettingsManager.ConfigCore.SelectedTextUploaderDestination]).Checked = true;
            UploadManager.TextUploader = (TextDestination)SettingsManager.ConfigCore.SelectedTextUploaderDestination;

            if (Helpers.GetEnumLength<UrlShortenerType>() <= SettingsManager.ConfigCore.SelectedURLShortenerDestination)
            {
                SettingsManager.ConfigCore.SelectedURLShortenerDestination = 0;
            }

            ClearToolStripMenuItemChecks(tsmiURLShorteners);
            ((ToolStripMenuItem)tsmiURLShorteners.DropDownItems[SettingsManager.ConfigCore.SelectedURLShortenerDestination]).Checked = true;
            UploadManager.URLShortener = (UrlShortenerType)SettingsManager.ConfigCore.SelectedURLShortenerDestination;

            UpdateUploaderMenuNames();

            #endregion Upload Destinations

            UploadManager.UpdateProxySettings();
        }

        public void ReloadOutputsMenu()
        {
            var taskImageJobs = Enum.GetValues(typeof(Subtask)).Cast<Subtask>().Select(x => new
            {
                Description = x.GetDescription(),
                Enum = x
            });

            tsddbOutputs.DropDownItems.Clear();

            foreach (var job in taskImageJobs)
            {
                switch (job.Enum)
                {
                    case Subtask.None:
                        continue;
                }
                ToolStripMenuItem tsmi = new ToolStripMenuItem(job.Description);
                tsmi.Checked = SettingsManager.ConfigCore.AfterCaptureSubtasks.HasFlag(job.Enum);
                tsmi.Tag = job.Enum;
                tsmi.CheckOnClick = true;
                tsmi.CheckedChanged += new EventHandler(tsmiAfterCaptureTask_CheckedChanged);
                tsddbOutputs.DropDownItems.Add(tsmi);
            }
        }

        private void tsmiAfterCaptureTask_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Checked)
                SettingsManager.ConfigCore.AfterCaptureSubtasks |= (Subtask)tsmi.Tag;
            else
                SettingsManager.ConfigCore.AfterCaptureSubtasks &= ~(Subtask)tsmi.Tag;
        }

        private void InitControls()
        {
            InitializeComponent();

            this.Text = Program.Title;
            this.Icon = Resources.ShareX;
            niTray.Text = this.Text;
            niTray.Icon = Resources.ShareXSmallIcon;

            #region Uploaders

            foreach (ImageDestination imageUploader in Enum.GetValues(typeof(ImageDestination)))
            {
                tsmiImageUploaders.DropDownItems.Add(new ToolStripMenuItem(imageUploader.GetDescription()) { Tag = imageUploader });
            }
            tsmiImageUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbImageUploaders_DropDownItemClicked);

            foreach (FileDestination fileUploader in Enum.GetValues(typeof(FileDestination)))
            {
                tsmiFileUploaders.DropDownItems.Add(new ToolStripMenuItem(fileUploader.GetDescription()) { Tag = fileUploader });
            }
            tsmiFileUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbFileUploaders_DropDownItemClicked);

            foreach (TextDestination textUploader in Enum.GetValues(typeof(TextDestination)))
            {
                tsmiTextUploaders.DropDownItems.Add(new ToolStripMenuItem(textUploader.GetDescription()) { Tag = textUploader });
            }
            tsmiTextUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbTextUploaders_DropDownItemClicked);

            foreach (string urlShortener in Helpers.GetEnumDescriptions<UrlShortenerType>())
            {
                tsmiURLShorteners.DropDownItems.Add(new ToolStripMenuItem(urlShortener));
            }
            tsmiURLShorteners.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbURLShorteners_DropDownItemClicked);

            #endregion Uploaders

            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.Images.Add(Properties.Resources.navigation_090_button);
            il.Images.Add(Properties.Resources.cross_button);
            il.Images.Add(Properties.Resources.tick_button);
            il.Images.Add(Properties.Resources.navigation_000_button);
            lvUploads.SmallImageList = il;
            lvUploads.FillLastColumn();

            UploadManager.ListViewControl = lvUploads;

            lvUploads.ColumnWidthChanged += new ColumnWidthChangedEventHandler(lvUploads_ColumnWidthChanged);

#if DEBUG
            // Test button: Left click uploads test image. Right click opens capture test window.
            tsbDebug.Visible = true;
#endif
        }

        private void UpdateControls()
        {
            tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible =
                copyShortenedURLToolStripMenuItem.Visible = copyThumbnailURLToolStripMenuItem.Visible = copyDeletionURLToolStripMenuItem.Visible =
                showErrorsToolStripMenuItem.Visible = copyErrorsToolStripMenuItem.Visible = showResponseToolStripMenuItem.Visible =
                uploadFileToolStripMenuItem.Visible = stopUploadToolStripMenuItem.Visible = false;

            int itemsCount = lvUploads.SelectedItems.Count;

            if (itemsCount > 0)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.URL))
                    {
                        tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible = true;

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

                    showInWindowsExplorerToolStripMenuItem.Visible = File.Exists(result.LocalFilePath);
                    shareToolStripMenuItem.Visible = File.Exists(result.LocalFilePath);
                }

                int index = lvUploads.SelectedIndices[0];
                stopUploadToolStripMenuItem.Visible = UploadManager.Tasks[index].Status != TaskStatus.Completed;
            }
            else
            {
                uploadFileToolStripMenuItem.Visible = true;
                showInWindowsExplorerToolStripMenuItem.Visible = false;
                shareToolStripMenuItem.Visible = false;
            }
        }

        private void UpdateUploaderMenuNames()
        {
            tsmiImageUploaders.DropDownItems[tsmiImageUploaders.DropDownItems.Count - 1].Text = UploadManager.FileUploader.GetDescription();
            tsmiTextUploaders.DropDownItems[tsmiTextUploaders.DropDownItems.Count - 1].Text = UploadManager.FileUploader.GetDescription();

            tsmiImageUploaders.Text = "Image uploader: ";
            if (UploadManager.ImageUploader == ImageDestination.FileUploader)
                tsmiImageUploaders.Text += UploadManager.FileUploader.GetDescription();
            else
                tsmiImageUploaders.Text += UploadManager.ImageUploader.GetDescription();

            tsmiTextUploaders.Text = "Text uploader: ";
            if (UploadManager.TextUploader == TextDestination.FileUploader)
                tsmiTextUploaders.Text += UploadManager.FileUploader.GetDescription();
            else
                tsmiTextUploaders.Text += UploadManager.TextUploader.GetDescription();

            tsmiFileUploaders.Text = "File uploader: " + UploadManager.FileUploader.GetDescription();
            tsmiURLShorteners.Text = "URL shortener: " + UploadManager.URLShortener.GetDescription();
        }

        private void CheckUpdate()
        {
            UpdateChecker updateChecker = new UpdateChecker(Program.URL_UPDATE, Application.ProductName, new Version(Program.AssemblyVersion),
                ReleaseChannelType.Stable, Uploader.ProxySettings.GetWebProxy);
            updateChecker.CheckUpdate();

            if (updateChecker.UpdateInfo != null && updateChecker.UpdateInfo.Status == UpdateStatus.UpdateRequired && !string.IsNullOrEmpty(updateChecker.UpdateInfo.URL))
            {
                if (MessageBox.Show("Update found. Do you want to download it?", "Update check", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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

        private void OpenURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.URL))
            {
                Helpers.LoadBrowserAsync(result.URL);
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

        private void tsbDebug_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                UploadManager.UploadImage(Resources.ShareXLogo);
            }
            else if (e.Button == MouseButtons.Right)
            {
                new RegionCapturePreview(SettingsManager.ConfigCore.SurfaceOptions).Show();
            }
        }

        private void tsddbImageUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiImageUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiImageUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    SettingsManager.ConfigCore.SelectedImageUploaderDestination = i;
                    UploadManager.ImageUploader = (ImageDestination)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsddbDestinations_DropDownOpening(object sender, EventArgs e)
        {
            UpdateUploaderMenuNames();
        }

        private void tsddbFileUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiFileUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiFileUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    SettingsManager.ConfigCore.SelectedFileUploaderDestination = i;
                    UploadManager.FileUploader = (FileDestination)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsddbTextUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiTextUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiTextUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    SettingsManager.ConfigCore.SelectedTextUploaderDestination = i;
                    UploadManager.TextUploader = (TextDestination)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsddbURLShorteners_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiURLShorteners.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiURLShorteners.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    SettingsManager.ConfigCore.SelectedURLShortenerDestination = i;
                    UploadManager.URLShortener = (UrlShortenerType)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsddbUploadersConfig_Click(object sender, EventArgs e)
        {
            FormsHelper.ShowUploadersConfig();
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            new HistoryForm(Program.HistoryFilePath, SettingsManager.ConfigCore.HistoryMaxItemCount, "ShareX - History").ShowDialog();
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
            OpenURL();
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
                    UploadManager.Tasks[index].Stop();
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

        private void shareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    UploadResult result = lvUploads.Items[index].Tag as UploadResult;
                    UploadManager.UploadFile(result.LocalFilePath, new HelperClasses.AfterCaptureActivity()
                    {
                        Subtasks = Subtask.UploadImageToHost
                    });
                }
            }
        }

        private void lvUploads_DoubleClick(object sender, EventArgs e)
        {
            OpenURL();
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
    }
}