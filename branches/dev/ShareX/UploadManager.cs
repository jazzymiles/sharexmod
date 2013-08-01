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
using HistoryLib;
using ShareX.Forms;
using ShareX.HelperClasses;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
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
        public static SocialNetworkingService SocialNetworkingService { get; set; }

        private static object uploadManagerLock = new object();

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
                    EDataType type = Helpers.FindDataType(path);
                    EDataType destination = EDataType.Default;

                    switch (type)
                    {
                        case EDataType.Image:
                            if (ImageUploader == ImageDestination.FileUploader)
                            {
                                destination = EDataType.File;
                            }
                            break;

                        case EDataType.Text:
                            if (TextUploader == TextDestination.FileUploader)
                            {
                                destination = EDataType.File;
                            }
                            break;
                    }

                    AfterCaptureActivity.Prepare(ref act);

                    foreach (FileDestination fileUploader in act.Workflow.Settings.DestConfig.FileUploaders)
                    {
                        UploadTask task = UploadTask.CreateFileUploaderTask(path, destination);
                        task.SetWorkflow(act.Workflow);
                        TaskManager.Start(task);
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
                UploadTask task = UploadTask.CreateImageUploaderTask(imageData, destination);
                task.SetWorkflow(act.Workflow);
                TaskManager.Start(task);
            }
        }

        public static void DoScreencast(ImageData imageData, AfterCaptureActivity act)
        {
            if (imageData != null)
            {
                FormsHelper.ScreencastUi = new ScreencastUI(imageData, act) { Icon = FormsHelper.Main.Icon };
                FormsHelper.ScreencastUi.Show();
            }
        }

        public static void RunImageTask(Image img, Subtask subtask = Subtask.UploadToRemoteHost)
        {
            if (img != null && subtask != Subtask.None)
            {
                UploadTask task = UploadTask.CreateImageUploaderTask(ImageData.FromScreenshot(img));
                task.SetWorkflow(new Workflow());
                TaskManager.Start(task);
            }
        }

        public static void UploadImage(Image img, AfterCaptureActivity act = null)
        {
            AfterCaptureActivity.Prepare(ref act);
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
            AfterCaptureActivity.Prepare(ref act);

            if (!string.IsNullOrEmpty(text))
            {
                EDataType destination = EDataType.Text;
                if (act.Workflow.Settings.DestConfig.TextUploaders.Count > 0)
                    destination = act.Workflow.Settings.DestConfig.TextUploaders[0] == TextDestination.FileUploader ? EDataType.File : EDataType.Text;
                UploadTask task = UploadTask.CreateTextUploaderTask(text, destination);
                task.SetWorkflow(act.Workflow);
                TaskManager.Start(task);
            }
        }

        public static void ShortenURL(string url, AfterCaptureActivity act = null)
        {
            AfterCaptureActivity.Prepare(ref act);

            if (!string.IsNullOrEmpty(url))
            {
                UploadTask task = UploadTask.CreateURLShortenerTask(url);
                task.SetWorkflow(act.Workflow);
                TaskManager.Start(task);
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

                if (SettingsManager.ConfigCore.ClipboardUploadAutoDetectURL && Helpers.IsValidURLRegex(text))
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

        public static void ShareUsingSocialNetworkingService(UploadResult result, AfterCaptureActivity act = null)
        {
            AfterCaptureActivity.Prepare(ref act);

            UploadTask task = UploadTask.CreatePostToSocialNetworkingServiceTask(result);
            task.SetWorkflow(act.Workflow);
            TaskManager.Start(task);
        }

        public static void UploadStream(Stream stream, string filePath, AfterCaptureActivity act = null, EDataType dataType = EDataType.File)
        {
            if (stream != null && stream.Length > 0 && !string.IsNullOrEmpty(filePath))
            {
                AfterCaptureActivity.Prepare(ref act);

                EDataType destination = ImageUploader == ImageDestination.FileUploader ? EDataType.File : dataType;
                UploadTask task = UploadTask.CreateDataUploaderTask(EDataType.Image, stream, filePath, destination);
                task.SetWorkflow(act.Workflow);
                TaskManager.Start(task);
            }
        }

        public static void UpdateProxySettings()
        {
            Uploader.ProxyInfo = SettingsManager.ConfigCore.ProxySettings;
        }

        #region Task Event Handler Methods

        private static string ProperTimeSpan(TimeSpan ts)
        {
            string time = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            int hours = (int)ts.TotalHours;
            if (hours > 0) time = hours + ":" + time;
            return time;
        }

        #endregion Task Event Handler Methods
    }
}