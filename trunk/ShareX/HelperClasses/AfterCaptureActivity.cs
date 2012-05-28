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
        public Workflow Workflow { get; set; }

        public static bool IsNullOrEmpty(AfterCaptureActivity act)
        {
            return act == null || act.Workflow.DestConfig.IsEmptyAny;
        }

        public static bool IsEmpty(AfterCaptureActivity act)
        {
            return act != null && act.Workflow.DestConfig.IsEmptyAll;
        }

        public static AfterCaptureActivity GetNew()
        {
            AfterCaptureActivity act = new AfterCaptureActivity();
            act.Workflow = new Workflow();
            act.Subtasks = SettingsManager.ConfigCore.AfterCaptureSubtasks;
            act.GetDefaults();

            return act;
        }

        internal void GetDefaults()
        {
            if (this.Subtasks.HasFlag(Subtask.UploadImageToHost))
            {
                if (this.Workflow.DestConfig.ImageUploaders.Count == 0)
                    this.Workflow.DestConfig.ImageUploaders.Add(UploadManager.ImageUploader);
                if (this.Workflow.DestConfig.TextUploaders.Count == 0)
                    this.Workflow.DestConfig.TextUploaders.Add(UploadManager.TextUploader);
                if (this.Workflow.DestConfig.FileUploaders.Count == 0)
                    this.Workflow.DestConfig.FileUploaders.Add(UploadManager.FileUploader);
            }

            // LinkUploaders are only added if they are empty at ShortenURL method
            // if (this.Uploaders.LinkUploaders.Count == 0)
            //     this.Uploaders.LinkUploaders.Add(UploadManager.URLShortener);

            if (this.Subtasks == Subtask.None)
                this.Subtasks = SettingsManager.ConfigCore.AfterCaptureSubtasks;
        }
    }
}