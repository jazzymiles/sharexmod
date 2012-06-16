using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HelpersLib.Hotkeys2
{
    /// <summary>
    /// This Enum must not be restructured. New items must append at the end to avoid mapping to the wrong item.
    /// </summary>
    [TypeConverter(typeof(EnumToStringUsingDescription))]
    public enum EActivity
    {
        [Description("Copy image to clipboard"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        ClipboardCopyImage,

        [Description("Annotate image"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ImageAnnotate,

        [Description("Run external program"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        RunExternalProgram,

        [Description("Save to file"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFile,

        [Description("Save to file with dialog"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFileWithDialog,

        [Description("Perform after capture tasks"), Category(ComponentModelStrings.ActivitiesUploaders)]
        AfterCaptureTasks,

        [Description("Upload to default remote host"), Category(ComponentModelStrings.ActivitiesUploaders)]
        UploadToRemoteHost,

        [Description("Send to printer"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        Printer,

        [Description("Open with Image Effects Studio"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ShowImageEffectsStudio,

        [Description("Add torn paper effect"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ImageAnnotateAddTornEffect,

        [Description("Add shadow border"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ImageAnnotateAddShadowBorder,
    }
}