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
        public EInputType InputType { get; set; }

        public Workflow Workflow = new Workflow();

        public static bool IsNullOrEmpty(AfterCaptureActivity act)
        {
            return act == null || act.Workflow.Settings.DestConfig.IsEmptyAny;
        }

        public static bool IsEmpty(AfterCaptureActivity act)
        {
            return act != null && act.Workflow.Settings.DestConfig.IsEmptyAll;
        }

        public static AfterCaptureActivity GetNew()
        {
            AfterCaptureActivity act = new AfterCaptureActivity();
            act.Workflow = new Workflow();
            act.Workflow.Subtasks = SettingsManager.ConfigCore.AfterCaptureTasks;
            act.GetDefaults();

            return act;
        }

        /// <summary>
        /// Creates a new object if the object is null or else sets defaults
        /// </summary>
        /// <param name="act"></param>
        public static void Prepare(AfterCaptureActivity act)
        {
            if (act == null)
                act = AfterCaptureActivity.GetNew();
            else if (AfterCaptureActivity.IsNullOrEmpty(act))
                act.GetDefaults();
        }

        internal void GetDefaults()
        {
            if (this.Workflow.Subtasks.HasFlag(Subtask.UploadToDefaultRemoteHost))
            {
                if (this.Workflow.Settings.DestConfig.ImageUploaders.Count == 0)
                    this.Workflow.Settings.DestConfig.ImageUploaders.Add(UploadManager.ImageUploader);
                if (this.Workflow.Settings.DestConfig.TextUploaders.Count == 0)
                    this.Workflow.Settings.DestConfig.TextUploaders.Add(UploadManager.TextUploader);
                if (this.Workflow.Settings.DestConfig.FileUploaders.Count == 0)
                    this.Workflow.Settings.DestConfig.FileUploaders.Add(UploadManager.FileUploader);
            }

            // LinkUploaders are only added if they are empty at ShortenURL method
            // if (this.Uploaders.LinkUploaders.Count == 0)
            //     this.Uploaders.LinkUploaders.Add(UploadManager.URLShortener);

            if (this.Workflow.Subtasks == Subtask.None)
                this.Workflow.Subtasks = SettingsManager.ConfigCore.AfterCaptureTasks;
        }
    }
}