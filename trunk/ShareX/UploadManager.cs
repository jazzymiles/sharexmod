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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using HistoryLib;
using ShareX.HelperClasses;
using ShareX.Properties;
using UploadersLib;
using UploadersLib.HelperClasses;

namespace ShareX
{
    public static class UploadManager
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ImageDestination ImageUploader { get; set; }

        public static TextDestination TextUploader { get; set; }

        public static FileDestination FileUploader { get; set; }

        public static UrlShortenerType URLShortener { get; set; }

        public static MyListView ListViewControl { get; set; }

        public static List<Task> Tasks { get; private set; }

        private static object uploadManagerLock = new object();

        static UploadManager()
        {
            Tasks = new List<Task>();
        }

        #region Files

        public static void UploadFiles(string[] files, AfterCaptureActivity jobs = null)
        {
            if (files != null && files.Length > 0)
            {
                foreach (string file in files)
                {
                    UploadFile(file, jobs);
                }
            }
        }

        public static void UploadFile(AfterCaptureActivity jobs = null)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UploadFiles(ofd.FileNames, jobs);
                }
            }
        }

        public static void UploadFile(string path, AfterCaptureActivity act = null)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    EDataType type;
                    EDataType destination = EDataType.Default;

                    if (Helpers.IsImageFile(path))
                    {
                        type = EDataType.Image;

                        if (ImageUploader == ImageDestination.FileUploader)
                        {
                            destination = EDataType.File;
                        }
                    }
                    else if (Helpers.IsTextFile(path))
                    {
                        type = EDataType.Text;

                        if (TextUploader == TextDestination.FileUploader)
                        {
                            destination = EDataType.File;
                        }
                    }
                    else
                    {
                        type = EDataType.File;
                    }

                    AfterCaptureActivity.Prepare(act);

                    foreach (FileDestination fileUploader in act.Workflow.Settings.DestConfig.FileUploaders)
                    {
                        Task task = Task.CreateFileUploaderTask(type, path, destination);
                        task.SetWorkflow(act.Workflow);

                        StartUpload(task);
                        break;
                    }
                }
                else if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                    UploadFiles(files);
                }
            }
        }

        #endregion Files

        #region Images

        public static void DoImageWork(ImageData imageData, AfterCaptureActivity act)
        {
            if (imageData != null)
            {
                EDataType destination = EDataType.Image;
                if (act.Workflow.Settings.DestConfig.ImageUploaders.Count > 0)
                    destination = act.Workflow.Settings.DestConfig.ImageUploaders[0] == ImageDestination.FileUploader ? EDataType.File : EDataType.Image;
                Task task = Task.CreateImageUploaderTask(imageData, destination);
                task.Info.Jobs = act.Workflow.Subtasks;
                task.SetWorkflow(act.Workflow);
                StartUpload(task);
            }
        }

        public static void UploadImage(Image img, AfterCaptureActivity act = null)
        {
            AfterCaptureActivity.Prepare(act);
            DoImageWork(new ImageData(img), act);
        }

        #endregion Images

        #region Text

        /// <summary>
        /// Optionally takes AfterCaptureActivity to configure task specific text uploaders
        /// </summary>
        /// <param name="text"></param>
        /// <param name="act"></param>
        public static void UploadText(string text, AfterCaptureActivity act = null)
        {
            AfterCaptureActivity.Prepare(act);

            if (!string.IsNullOrEmpty(text))
            {
                EDataType destination = EDataType.Text;
                if (act.Workflow.Settings.DestConfig.TextUploaders.Count > 0)
                    destination = act.Workflow.Settings.DestConfig.TextUploaders[0] == TextDestination.FileUploader ? EDataType.File : EDataType.Text;
                Task task = Task.CreateTextUploaderTask(text, destination);
                task.Info.Jobs = act.Workflow.Subtasks;
                task.SetWorkflow(act.Workflow);
                StartUpload(task);
            }
        }

        public static void ShortenURL(string url, AfterCaptureActivity act = null)
        {
            AfterCaptureActivity.Prepare(act);

            if (!string.IsNullOrEmpty(url))
            {
                Task task = Task.CreateURLShortenerTask(url);
                task.SetWorkflow(act.Workflow);
                StartUpload(task);
            }
        }

        #endregion Text

        #region Clipboard Upload

        public static void ClipboardUpload(AfterCaptureActivity jobs = null)
        {
            if (Clipboard.ContainsImage())
            {
                Image img = Clipboard.GetImage();
                UploadImage(img, jobs);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string[] files = Clipboard.GetFileDropList().Cast<string>().ToArray();
                UploadFiles(files);
            }
            else if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();

                if (SettingsManager.ConfigCore.ClipboardUploadAutoDetectURL && Helpers.IsValidURL(text))
                {
                    ShortenURL(text.Trim(), jobs);
                }
                else
                {
                    UploadText(text, jobs);
                }
            }
        }

        public static void ClipboardUploadWithContentViewer()
        {
            if (SettingsManager.ConfigCore.ShowClipboardContentViewer)
            {
                using (ClipboardContentViewer ccv = new ClipboardContentViewer())
                {
                    if (ccv.ShowDialog() == DialogResult.OK && !ccv.IsClipboardEmpty)
                    {
                        UploadManager.ClipboardUpload();
                    }

                    SettingsManager.ConfigCore.ShowClipboardContentViewer = !ccv.DontShowThisWindow;
                }
            }
            else
            {
                UploadManager.ClipboardUpload();
            }
        }

        #endregion Clipboard Upload

        #region Drag n Drop

        public static void DragDropUpload(IDataObject data)
        {
            if (data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = data.GetData(DataFormats.FileDrop, false) as string[];
                UploadFiles(files);
            }
            else if (data.GetDataPresent(DataFormats.Bitmap, false))
            {
                Image img = data.GetData(DataFormats.Bitmap, false) as Image;
                UploadImage(img);
            }
            else if (data.GetDataPresent(DataFormats.Text, false))
            {
                string text = data.GetData(DataFormats.Text, false) as string;
                UploadText(text);
            }
        }

        #endregion Drag n Drop

        public static void UploadStream(Stream stream, string filePath, AfterCaptureActivity act = null, EDataType dataType = EDataType.File)
        {
            if (stream != null && stream.Length > 0 && !string.IsNullOrEmpty(filePath))
            {
                AfterCaptureActivity.Prepare(act);

                EDataType destination = ImageUploader == ImageDestination.FileUploader ? EDataType.File : dataType;
                Task task = Task.CreateDataUploaderTask(EDataType.Image, stream, filePath, destination);
                task.SetWorkflow(act.Workflow);
                StartUpload(task);
            }
        }

        private static void StartUpload(Task task)
        {
            Tasks.Add(task);
            task.Info.ID = Tasks.Count - 1;
            task.UploadPreparing += new Task.TaskEventHandler(task_UploadPreparing);
            task.UploadStarted += new Task.TaskEventHandler(task_UploadStarted);
            task.UploadProgressChanged += new Task.TaskEventHandler(task_UploadProgressChanged);
            task.UploadCompleted += new Task.TaskEventHandler(task_UploadCompleted);
            CreateListViewItem(task.Info);
            StartTasks();
            TrayIconManager.UpdateTrayIcon();
        }

        private static void StartTasks()
        {
            int workingTasksCount = Tasks.Count(x => x.IsWorking);
            Task[] inQueueTasks = Tasks.Where(x => x.Status == TaskStatus.InQueue).ToArray();

            if (inQueueTasks.Length > 0)
            {
                int len;

                if (SettingsManager.ConfigCore.UploadLimit == 0)
                {
                    len = inQueueTasks.Length;
                }
                else
                {
                    len = (SettingsManager.ConfigCore.UploadLimit - workingTasksCount).Between(0, inQueueTasks.Length);
                }

                for (int i = 0; i < len; i++)
                {
                    inQueueTasks[i].Start();
                }
            }
        }

        public static void StopUpload(int index)
        {
            if (Tasks.Count < index)
            {
                Tasks[index].Stop();
            }
        }

        public static void UpdateProxySettings()
        {
            ProxySettings proxy = new ProxySettings();
            if (!string.IsNullOrEmpty(SettingsManager.ConfigCore.ProxySettings.Host))
            {
                proxy.ProxyConfig = EProxyConfigType.ManualProxy;
            }
            proxy.ProxyActive = SettingsManager.ConfigCore.ProxySettings;
            Uploader.ProxySettings = proxy;
        }

        private static void ChangeListViewItemStatus(UploadInfo info)
        {
            if (ListViewControl != null && info.Jobs.HasFlag(Subtask.UploadToDefaultRemoteHost))
            {
                ListViewItem lvi = ListViewControl.Items[info.ID];
                lvi.SubItems[1].Text = info.Status;
            }
        }

        private static void CreateListViewItem(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                log.InfoFormat("Upload in queue. ID: {0}, Job: {1}, Type: {2}, Host: {3}", info.ID, info.Job, info.UploadDestination, info.Destination);

                ListViewItem lvi = new ListViewItem();

                lvi.Text = info.FileName;
                lvi.SubItems.Add("In queue");
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(info.DataType.ToString());

                var taskImageJobs = Enum.GetValues(typeof(Subtask)).Cast<Subtask>();
                foreach (Subtask job in taskImageJobs)
                {
                    switch (job)
                    {
                        case Subtask.None:
                            continue;
                    }

                    if (info.Jobs.HasFlag(Subtask.UploadToDefaultRemoteHost))
                    {
                        lvi.SubItems.Add(info.Destination);
                        break;
                    }
                    else if (info.Jobs.HasFlag(job))
                    {
                        lvi.SubItems.Add(job.GetDescription());
                        break;
                    }
                    else
                    {
                        lvi.SubItems.Add(string.Empty);
                        break;
                    }
                }

                lvi.SubItems.Add(string.Empty);
                lvi.BackColor = info.ID % 2 == 0 ? Color.White : Color.WhiteSmoke;
                ListViewManager.set_IconCreated(lvi);
                ListViewControl.Items.Add(lvi);
                lvi.EnsureVisible();
                ListViewControl.FillLastColumn();
            }
        }

        #region Task Event Handler Methods

        private static void task_UploadPreparing(UploadInfo info)
        {
            log.Info(string.Format("Upload preparing. ID: {0}", info.ID));
            ChangeListViewItemStatus(info);
        }

        private static void task_UploadStarted(UploadInfo info)
        {
            string status = string.Format("Upload started. ID: {0}, Filename: {1}", info.ID, info.FileName);
            if (!string.IsNullOrEmpty(info.FilePath)) status += ", Filepath: " + info.FilePath;
            log.Info(status);

            ListViewItem lvi = ListViewControl.Items[info.ID];
            lvi.Text = info.FileName;
            lvi.SubItems[1].Text = info.Status;

            ListViewManager.set_IconUploadStarted(lvi);
        }

        private static void task_UploadProgressChanged(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[info.ID];
                lvi.SubItems[1].Text = string.Format("{0:0.0}%", info.Progress.Percentage);
                lvi.SubItems[2].Text = string.Format("{0} / {1}", Helpers.ProperFileSize(info.Progress.Position, "", true), Helpers.ProperFileSize(info.Progress.Length, "", true));
                if (info.Progress.Speed > 0)
                    lvi.SubItems[3].Text = Helpers.ProperFileSize((long)info.Progress.Speed, "/s", true);
                lvi.SubItems[4].Text = ProperTimeSpan(info.Progress.Elapsed);
                lvi.SubItems[5].Text = ProperTimeSpan(info.Progress.Remaining);
            }
        }

        private static string ProperTimeSpan(TimeSpan ts)
        {
            string time = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            int hours = (int)ts.TotalHours;
            if (hours > 0) time = hours + ":" + time;
            return time;
        }

        /// <summary>
        /// Mod 01: Not just uploads, everything gets added to List e.g. Saving to file
        /// </summary>
        /// <param name="info">UploadInfo</param>
        private static void task_UploadCompleted(UploadInfo info)
        {
            try
            {
                if (ListViewControl != null && info != null && info.Result != null)
                {
                    info.Result.LocalFilePath = info.FilePath;
                    ListViewItem lvi = ListViewControl.Items[info.ID];
                    lvi.Tag = info.Result;

                    ListViewManager.AddThumbnail();

                    if (string.IsNullOrEmpty(lvi.SubItems[7].Text))
                        lvi.SubItems[7].Text = info.Destination; // update Destination if not empty; this applies for URL Shortening

                    if (info.Result.IsError)
                    {
                        string errors = string.Join("\r\n\r\n", info.Result.Errors.ToArray());

                        log.ErrorFormat("Upload failed. ID: {0}, Filename: {1}, Errors:\r\n{2}", info.ID, info.FileName, errors);

                        lvi.SubItems[1].Text = "Error";
                        lvi.SubItems[8].Text = string.Empty;

                        ListViewManager.set_IconError(lvi);

                        if (SettingsManager.ConfigCore.PlaySoundAfterUpload)
                            SystemSounds.Asterisk.Play();
                    }
                    else
                    {
                        log.InfoFormat("Upload completed. ID: {0}, Filename: {1}, URL: {2}, Duration: {3} ms", info.ID, info.FileName,
                            info.Result.URL, (int)info.UploadDuration.TotalMilliseconds);

                        lvi.SubItems[1].Text = info.Status;
                        ListViewManager.set_IconCompleted(lvi);

                        string url = string.IsNullOrEmpty(info.Result.ShortenedURL) ? info.Result.URL : info.Result.ShortenedURL;
                        if (string.IsNullOrEmpty(url))
                            url = info.FilePath;

                        lvi.SubItems[8].Text = url;

                        if (SettingsManager.ConfigCore.Outputs.HasFlag(OutputEnum.Clipboard) &&
                            SettingsManager.ConfigCore.ClipboardAutoCopy)
                        {
                            Helpers.CopyTextSafely(url);
                        }

                        if (!string.IsNullOrEmpty(info.Result.URL))
                        {
                            if (SettingsManager.ConfigCore.SaveHistory)
                            {
                                SettingsManager.ConfigHistory.AddHistoryItemAsync(info.GetHistoryItem());
                            }

                            if (FormsHelper.Main.niTray.Visible && SettingsManager.ConfigCore.ShowBalloonAfterUpload)
                            {
                                FormsHelper.Main.niTray.Tag = url;
                                FormsHelper.Main.niTray.ShowBalloonTip(5000, "ShareX - Upload completed", url, ToolTipIcon.Info);
                            }

                            if (SettingsManager.ConfigCore.ShowClipboardOptionsWizard)
                            {
                                WindowAfterUpload dlg = new WindowAfterUpload(info) { Icon = Resources.ShareX };
                                NativeMethods.ShowWindow(dlg.Handle, (int)WindowShowStyle.ShowNoActivate);
                            }
                        }

                        if (SettingsManager.ConfigCore.PlaySoundAfterUpload)
                            SystemSounds.Exclamation.Play();
                    }

                    lvi.EnsureVisible();
                }
            }
            finally
            {
                StartTasks();
            }
        }

        #endregion Task Event Handler Methods
    }
}