using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HelpersLib;

namespace HelpersLibMod
{
    /// <summary>
    /// This Enum must not be restructured. New items must append at the end to avoid mapping to the wrong item.
    /// </summary>
    [Flags]
    [TypeConverter(typeof(EnumToStringUsingDescription))]
    public enum Subtask
    {
        None = 1 << 0,

        [Description("Annotate image"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        AnnotateImage = 1 << 1,

        [Description("Add torn paper effect"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        AnnotateImageAddTornEffect = 1 << 2,

        [Description("Add shadow border"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        AnnotateImageAddShadowBorder = 1 << 3,

        [Description("Add watermark"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        AddWatermark = 1 << 4,

        [Description("Open with Image Effects Studio"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ShowImageEffectsStudio = 1 << 5,

        [Description("Copy image to clipboard"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        CopyImageToClipboard = 1 << 6,

        [Description("Save to file"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFile = 1 << 7,

        [Description("Save to file with dialog"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveImageToFileWithDialog = 1 << 8,

        [Description("Run external program"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        RunExternalProgram = 1 << 9,

        [Description("Upload to remote host"), Category(ComponentModelStrings.ActivitiesUploaders)]
        UploadToRemoteHost = 1 << 10,

        [Description("Send to printer"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        Print = 1 << 11,
    }

    [Flags]
    public enum AfterUploadTasks
    {
        None = 0,

        [Description("Use URL shortener")]
        UseURLShortener = 1,

        [Description("Post URL to social networking service")]
        ShareUsingSocialNetworkingService = 1 << 1,

        [Description("Copy URL to clipboard")]
        CopyURLToClipboard = 1 << 2
    }


    [Flags]
    public enum OutputEnum
    {
        [Description("Clipboard")]
        Clipboard = 0,

        [Description("Local disk")]
        LocalDisk = 2 << 0,

        [Description("Remote host")]
        RemoteHost = 2 << 1,

        [Description("E-mail")]
        Email = 2 << 2,

        [Description("Printer")]
        Printer = 2 << 3,

        [Description("Shared folder")]
        SharedFolder = 2 << 4
    }
}