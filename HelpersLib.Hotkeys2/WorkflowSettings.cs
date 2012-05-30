using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib;

namespace HelpersLib.Hotkeys2
{
    [Serializable]
    public class WorkflowSettings
    {
        public DestConfig DestConfig = new DestConfig();
        public List<FileAction> FileActions = new List<FileAction>();

        public void Clear()
        {
            DestConfig.ImageUploaders.Clear();
            DestConfig.TextUploaders.Clear();
            DestConfig.FileUploaders.Clear();
            DestConfig.LinkUploaders.Clear();
        }
    }
}