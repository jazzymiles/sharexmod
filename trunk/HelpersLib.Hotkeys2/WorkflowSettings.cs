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

        public WorkflowSettings()
        {
            if (ExternalPrograms.Count == 0)
            {
                SoftwareCheck("Paint", "mspaint.exe", "edit");
                SoftwareCheck("Adobe Photoshop", "Photoshop.exe");
                SoftwareCheck("Paint.NET", "PaintDotNet.exe");
                SoftwareCheck("Irfan View", "i_view32.exe");
                SoftwareCheck("XnView", "xnview.exe");
                SoftwareCheck("Picasa", "PicasaPhotoViewer.exe");
            }
        }

        public void Clear()
        {
            DestConfig.ImageUploaders.Clear();
            DestConfig.TextUploaders.Clear();
            DestConfig.FileUploaders.Clear();
            DestConfig.LinkUploaders.Clear();
        }

        /// <summary>Registry path: HKEY_CLASSES_ROOT\Applications\{fileName}\shell\{command}\command</summary>
        private bool SoftwareCheck(string name, string fileName, string command = "open")
        {
            string path = string.Format(@"HKEY_CLASSES_ROOT\Applications\{0}\shell\{1}\command", fileName, command);
            string value = Registry.GetValue(path, null, null) as string;

            if (!string.IsNullOrEmpty(value))
            {
                string filePath = value.ParseQuoteString();

                if (File.Exists(filePath))
                {
                    ExternalPrograms.Add(new FileAction()
                    {
                        Name = name,
                        Path = filePath,
                        Args = "%filepath%",
                        IsActive = false
                    });
                    return true;
                }
            }

            return false;
        }
    }
}