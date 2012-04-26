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

        public Workflow()
        {
        }
    }
}