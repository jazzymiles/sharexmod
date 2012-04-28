using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib.Hotkey;

namespace HelpersLib
{
    public class Workflow
    {
        public HotkeySetting HotkeyConfig = new HotkeySetting();
        public List<EActivity> Activities = new List<EActivity>();
        public EHotkey Hotkey;

        public Workflow() { }

        public Workflow(EHotkey hotkey, HotkeySetting hotkeyConfig)
            : this(hotkey.GetDescription(), hotkeyConfig, true)
        {
            this.Hotkey = hotkey;
        }

        public Workflow(string description, HotkeySetting hotkeyConfig, bool bProtected)
        {
            this.HotkeyConfig = hotkeyConfig;
            this.HotkeyConfig.SystemHotkey = bProtected;
            this.HotkeyConfig.Tag = Helpers.GetRandomAlphanumeric(12);
            this.HotkeyConfig.Description = description;
        }
    }
}