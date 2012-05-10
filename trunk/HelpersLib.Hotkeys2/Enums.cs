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
        CaptureScreen = 1,
        [Description("Capture active monitor"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureActiveMonitor = 2,
        [Description("Capture active window"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureActiveWindow = 4,
        [Description("Capture window or rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureWindowRectangle = 8,
        [Description("Capture rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureRectangleRegion = 16,
        [Description("Capture rounded rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureRoundedRectangleRegion = 32,
        [Description("Capture ellipse region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureEllipseRegion = 64,
        [Description("Capture triangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureTriangleRegion = 128,
        [Description("Capture diamond region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureDiamondRegion = 256,
        [Description("Capture polygon region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CapturePolygonRegion = 512,
        [Description("Capture freehand region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureFreeHandRegion = 1024,

        [Description("Upload clipboard content"), Category(ComponentModelStrings.ActivitiesCapture)]
        UploadClipboard = 2048,
        [Description("Upload file"), Category(ComponentModelStrings.ActivitiesCapture)]
        UploadFile = 4096,

        [Description("Copy image to clipboard"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        ClipboardCopyImage = 8192,
        [Description("Annotate image"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        ImageAnnotate = 16384,
        [Description("Save to file"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFile = 32768,
        [Description("Save to file with dialog"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFileWithDialog = 65536,
        [Description("Perform after capture tasks"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        AfterCaptureTasks = 131072,
        [Description("Upload to remote host"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        UploadToRemoteHost = 262144,
        [Description("Send to printer"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        Printer = 524288,

        [Description("Upload to ImageShack"), Category(ComponentModelStrings.ActivitiesUpload)]
        UploadToImageShack,
        [Description("Upload to Dropbox"), Category(ComponentModelStrings.ActivitiesUpload)]
        UploadToDropbox
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