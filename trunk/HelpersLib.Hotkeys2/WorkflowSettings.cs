using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using UploadersLib;

namespace HelpersLib.Hotkeys2
{
    [Serializable]
    public class WorkflowSettings
    {
        public DestConfig DestConfig = new DestConfig();
        public List<FileAction> ExternalPrograms = new List<FileAction>();

        public void Clear()
        {
            DestConfig.ImageUploaders.Clear();
            DestConfig.TextUploaders.Clear();
            DestConfig.FileUploaders.Clear();
            DestConfig.LinkUploaders.Clear();
        }

        public void GetDefaults()
        {
            if (ExternalPrograms.Count == 0)
            {
                AddExternalProgram("Paint", "mspaint.exe");
                AddExternalProgram("Paint.NET", "PaintDotNet.exe");
                AddExternalProgram("Adobe Photoshop", "Photoshop.exe");
                AddExternalProgram("IrfanView", "i_view32.exe");
                AddExternalProgram("XnView", "xnview.exe");
            }
        }

        private void AddExternalProgram(string name, string filename)
        {
            FileAction externalProgram = RegistryHelper.FindProgram(name, filename);

            if (externalProgram != null)
            {
                ExternalPrograms.Add(externalProgram);
            }
        }

    }
}