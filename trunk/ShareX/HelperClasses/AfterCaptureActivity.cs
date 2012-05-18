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
        public Subtask Subtasks { get; set; }
        public EInputType InputType { get; set; }
        public DestConfig Uploaders = new DestConfig();

        public static bool IsNullOrEmpty(AfterCaptureActivity act)
        {
            return act == null || act.Uploaders.IsEmptyAny;
        }

        public static bool IsEmpty(AfterCaptureActivity act)
        {
            return act != null && act.Uploaders.IsEmptyAll;
        }

        public static AfterCaptureActivity GetNew()
        {
            AfterCaptureActivity act = new AfterCaptureActivity();
            act.GetDefaults();

            return act;
        }

        internal void GetDefaults()
        {
            if (this.Subtasks.HasFlag(Subtask.UploadImageToHost))
            {
                if (this.Uploaders.ImageUploaders.Count == 0)
                    this.Uploaders.ImageUploaders.Add(UploadManager.ImageUploader);
                if (this.Uploaders.TextUploaders.Count == 0)
                    this.Uploaders.TextUploaders.Add(UploadManager.TextUploader);
                if (this.Uploaders.FileUploaders.Count == 0)
                    this.Uploaders.FileUploaders.Add(UploadManager.FileUploader);
            }

            // LinkUploaders are only added if they are empty at ShortenURL method
            // if (this.Uploaders.LinkUploaders.Count == 0)
            //     this.Uploaders.LinkUploaders.Add(UploadManager.URLShortener);

            if (this.Subtasks == Subtask.None)
                this.Subtasks = Program.Settings.AfterCaptureTasks;
        }
    }
}