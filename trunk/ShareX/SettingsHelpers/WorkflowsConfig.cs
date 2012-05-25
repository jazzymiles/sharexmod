using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;
using HelpersLib.Hotkeys2;

namespace ShareX.SettingsHelpers
{
    public class WorkflowsConfig : SettingsBase<WorkflowsConfig>
    {
        public List<Workflow> Workflows = new List<Workflow>();
    }
}