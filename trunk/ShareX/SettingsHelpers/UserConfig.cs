using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;
using HelpersLibWatermark;
using IndexersLib;

namespace ShareX.SettingsHelpers
{
    public class UserConfig : SettingsBase<UserConfig>
    {
        public PrintSettings PrintSettings = new PrintSettings();
        public IndexerConfig ConfigIndexer = new IndexerConfig();
        public WatermarkConfig ConfigWatermark = new WatermarkConfig();
    }
}