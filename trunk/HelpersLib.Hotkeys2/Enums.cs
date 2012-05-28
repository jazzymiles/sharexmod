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
    [Flags]
    [TypeConverter(typeof(EnumToStringUsingDescription))]
    public enum EActivity
    {
        [Description("Capture screen"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureScreen,
        [Description("Capture active monitor"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureActiveMonitor,
        [Description("Capture active window"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureActiveWindow,
        [Description("Capture window or rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureWindowRectangle,
        [Description("Capture rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureRectangleRegion,
        [Description("Capture rounded rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureRoundedRectangleRegion,
        [Description("Capture ellipse region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureEllipseRegion,
        [Description("Capture triangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureTriangleRegion,
        [Description("Capture diamond region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureDiamondRegion,
        [Description("Capture polygon region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CapturePolygonRegion,
        [Description("Capture freehand region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureFreeHandRegion,

        [Description("Upload clipboard content"), Category(ComponentModelStrings.ActivitiesCapture)]
        UploadClipboard,
        [Description("Upload file"), Category(ComponentModelStrings.ActivitiesCapture)]
        UploadFile,

        [Description("Copy image to clipboard"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        ClipboardCopyImage,
        [Description("Annotate image"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ImageAnnotate,
        [Description("Save to file"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFile,
        [Description("Save to file with dialog"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFileWithDialog,
        [Description("Perform after capture tasks"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
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

    public class EnumToStringUsingDescription : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType.Equals(typeof(Enum)));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType.Equals(typeof(String)));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (!destinationType.Equals(typeof(String)))
            {
                throw new ArgumentException("Can only convert to string.", "destinationType");
            }

            if (!value.GetType().BaseType.Equals(typeof(Enum)))
            {
                throw new ArgumentException("Can only convert an instance of enum.", "value");
            }

            string name = value.ToString();
            object[] attrs =
                value.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attrs.Length > 0) ? ((DescriptionAttribute)attrs[0]).Description : name;
        }
    }
}