using HelpersLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpersLibMod
{
    public class HelpersMod
    {
        public static bool HasExpressionEncoder()
        {
            using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Expression\Encoder"))
            {
                return regkey != null && regkey.SubKeyCount > 0;
            }
        }

        public static Image ImageFromFile(string fp)
        {
            return Image.FromStream(new MemoryStream(File.ReadAllBytes(fp)));
        }

        public static bool ManageImageFolders(string rootDir, string patternSubFolder)
        {
            if (!string.IsNullOrEmpty(rootDir) && Directory.Exists(rootDir))
            {
                string[] images = Directory.GetFiles(rootDir);

                List<string> imagesList = new List<string>();

                List<string> listExt = new List<string>();
                foreach (ImageFileExtensions ext in Enum.GetValues(typeof(ImageFileExtensions)))
                {
                    listExt.Add(ext.ToString());
                }
                foreach (VideoFileExtensions ext in Enum.GetValues(typeof(VideoFileExtensions)))
                {
                    listExt.Add(ext.ToString());
                }

                foreach (string image in images)
                {
                    foreach (string s in listExt)
                    {
                        if (Path.HasExtension(image) && Path.GetExtension(image.ToLower()) == "." + s)
                        {
                            imagesList.Add(image);
                            break;
                        }
                    }
                }

                DebugHelper.WriteLine(string.Format("Found {0} images to move to sub-folders", imagesList.Count));

                if (imagesList.Count > 0)
                {
                    if (MessageBox.Show(string.Format("{0} files found in {1}\n\nPlease wait until all the files are moved.",
                        imagesList.Count, rootDir), Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        return false;
                    }

                    DateTime time;
                    string movePath;

                    foreach (string image in imagesList)
                    {
                        if (File.Exists(image))
                        {
                            time = File.GetLastWriteTime(image);
                            string subDirName = new NameParser(NameParserType.FolderPath) { CustomDate = time }.Parse(patternSubFolder);
                            string subDirPath = Path.Combine(rootDir, subDirName);

                            if (!Directory.Exists(subDirPath))
                                Directory.CreateDirectory(subDirPath);

                            movePath = Helpers.GetUniqueFilePath(subDirPath, Path.GetFileName(image));
                            File.Move(image, movePath);
                        }
                    }
                }

                return true;
            }

            return false;
        }

  
    }
}