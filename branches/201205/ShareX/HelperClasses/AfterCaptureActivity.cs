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
        public TaskImageJob ImageTasks { get; set; }
        public List<ImageDestination> ImageDestinations = new List<ImageDestination>();
    }
}