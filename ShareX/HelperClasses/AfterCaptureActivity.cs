using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib.Hotkeys2;
using UploadersLib;

namespace ShareX.HelperClasses
{
    public class AfterCaptureActivity
    {
        public TaskImageJob ImageJobs { get; set; }
        public TaskTextJob TextJobs { get; set; }
        public TaskFileJob FileJobs { get; set; }
        public List<ImageDestination> ImageUploaders = new List<ImageDestination>(); // ShareX 7.1 - multiple destinations
        public List<FileDestination> FileUploaders = new List<FileDestination>();    // ShareX 7.1 - multiple destinations
        public List<TextDestination> TextUploaders = new List<TextDestination>();    // ShareX 7.1 - multiple destinations
        public UrlShortenerType URLShortener = UrlShortenerType.Google;

        public static bool IsNullOrEmpty(AfterCaptureActivity act)
        {
            return act == null || act.FileUploaders.Count == 0 || act.TextUploaders.Count == 0 || act.ImageUploaders.Count == 0;
        }

        public static AfterCaptureActivity GetNew()
        {
            AfterCaptureActivity act = new AfterCaptureActivity();
            act.GetDefaults();

            act.ImageJobs = Program.Settings.AfterCaptureTasks;

            return act;
        }

        internal void GetDefaults()
        {
            this.ImageUploaders.Add(UploadManager.ImageUploader);
            this.TextUploaders.Add(UploadManager.TextUploader);
            this.FileUploaders.Add(UploadManager.FileUploader);
            this.URLShortener = UploadManager.URLShortener;
        }
    }
}