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
        public List<ImageDestination> ImageUploaders = new List<ImageDestination>(); // ShareX 7.1 - multiple destinations
        public List<FileDestination> FileUploaders = new List<FileDestination>();    // ShareX 7.1 - multiple destinations
        public List<TextDestination> TextUploaders = new List<TextDestination>();    // ShareX 7.1 - multiple destinations
        public UrlShortenerType URLShortener = UrlShortenerType.Google;

        public AfterCaptureActivity()
        {
            ImageJobs = TaskImageJob.None;
            URLShortener = UrlShortenerType.Google;
        }
    }
}