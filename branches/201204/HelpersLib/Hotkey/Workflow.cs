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

        public Workflow() { }

        public Workflow(string description, HotkeySetting hotkeyConfig, bool bProtected = false)
        {
            this.HotkeyConfig = hotkeyConfig;
            this.HotkeyConfig.Protected = bProtected;
            this.HotkeyConfig.Tag = Helpers.GetRandomAlphanumeric(12);
            this.HotkeyConfig.Description = description;

            if (this.Activities.Count == 0)
            {
                this.Activities.Add(EActivity.UploadToRemoteHost);
            }
        }
    }
}