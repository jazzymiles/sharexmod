using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;
using HelpersLib.Hotkeys2;
using UploadersLib;

namespace ShareX.HelperClasses
{
    public class AfterCaptureActivity
    {
        public TaskImageJob ImageJobs { get; set; }
        public EInputType InputType { get; set; }
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

            return act;
        }

        internal void GetDefaults()
        {
            if (this.ImageUploaders.Count == 0)
                this.ImageUploaders.Add(UploadManager.ImageUploader);
            if (this.TextUploaders.Count == 0)
                this.TextUploaders.Add(UploadManager.TextUploader);
            if (this.FileUploaders.Count == 0)
                this.FileUploaders.Add(UploadManager.FileUploader);
            this.URLShortener = UploadManager.URLShortener;

            if (this.ImageJobs == TaskImageJob.None)
                this.ImageJobs = Program.Settings.AfterCaptureTasks;
        }
    }
}