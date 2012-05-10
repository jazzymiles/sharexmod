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
        [Description("Capture screen"), Category("Capture")]
        CaptureScreen,
        [Description("Capture active monitor"), Category("Capture")]
        CaptureActiveMonitor,
        [Description("Capture active window"), Category("Capture")]
        CaptureActiveWindow,
        [Description("Capture window or rectangle region"), Category("Capture")]
        CaptureWindowRectangle,
        [Description("Capture rectangle region"), Category("Capture")]
        CaptureRectangleRegion,
        [Description("Capture rounded rectangle region"), Category("Capture")]
        CaptureRoundedRectangleRegion,
        [Description("Capture ellipse region"), Category("Capture")]
        CaptureEllipseRegion,
        [Description("Capture triangle region"), Category("Capture")]
        CaptureTriangleRegion,
        [Description("Capture diamond region"), Category("Capture")]
        CaptureDiamondRegion,
        [Description("Capture polygon region"), Category("Capture")]
        CapturePolygonRegion,
        [Description("Capture freehand region"), Category("Capture")]
        CaptureFreeHandRegion,

        [Description("Upload clipboard content"), Category("Capture")]
        UploadClipboard,
        [Description("Upload file"), Category("Capture")]
        UploadFile,

        [Description("Copy image to clipboard"), Category("After Capture")]
        ClipboardCopyImage,
        [Description("Annotate image"), Category("After Capture")]
        ImageAnnotate,
        [Description("Save to file"), Category("After Capture")]
        SaveToFile,
        [Description("Save to file with dialog"), Category("After Capture")]
        SaveToFileWithDialog,
        [Description("Perform after capture tasks"), Category("After Capture")]
        AfterCaptureTasks,
        [Description("Upload to remote host"), Category("After Capture")]
        UploadToRemoteHost,
        [Description("Send to printer"), Category("After Capture")]
        Printer
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