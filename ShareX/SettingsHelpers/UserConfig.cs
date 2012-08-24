using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HelpersLib;
using HelpersLibMod;
using HelpersLibWatermark;
using IndexersLib;

namespace ShareX.SettingsHelpers
{
    [Serializable]
    public class UserConfig : SettingsBase<UserConfig>
    {
        // Image - Quality

        public EImageFormat ImageFormat = EImageFormat.PNG;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        // Image - Resize

        public bool ImageAutoResize = false;
        public bool ImageKeepAspectRatio = true;
        public bool ImageUseSmoothScaling = true;
        public ImageScaleType ImageScaleType = ImageScaleType.Percentage;
        public int ImageScalePercentageWidth = 100;
        public int ImageScalePercentageHeight = 100;
        public int ImageScaleToWidth = 100;
        public int ImageScaleToHeight = 100;
        public int ImageScaleSpecificWidth = 100;
        public int ImageScaleSpecificHeight = 100;

        public PrintSettings PrintSettings = new PrintSettings();
        public IndexerConfig ConfigIndexer = new IndexerConfig();
        public WatermarkConfig ConfigWatermark = new WatermarkConfig();

        [Category(ComponentModelStrings.App), DefaultValue(EListItemDoubleClickBehavior.OpenUrlOrFile), Description("Mouse double click behavior for items with a URL")]
        public EListItemDoubleClickBehavior ItemsWithUrlOnItemDoubleClick { get; set; }

        [Category(ComponentModelStrings.App), DefaultValue(EListItemDoubleClickBehavior.OpenDirectory), Description("Mouse double click behavior for items without a URL")]
        public EListItemDoubleClickBehavior ItemsWithoutUrlOnItemDoubleClick { get; set; }

        // Image Editor
        [Category(ComponentModelStrings.Screenshots), DefaultValue(EImageEditorOnKeyLock.None), Description("Automatically start Image Editor on a key press.")]
        public EImageEditorOnKeyLock ImageEditorOnKeyPress { get; set; }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public UserConfig()
        {
            ApplyDefaultValues(this);
        }
    }
}