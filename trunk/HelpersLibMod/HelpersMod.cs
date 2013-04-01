using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

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
    }
}