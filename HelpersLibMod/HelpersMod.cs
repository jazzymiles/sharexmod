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
        public static Image ImageFromFile(string fp)
        {
            return Image.FromStream(new MemoryStream(File.ReadAllBytes(fp)));
        }
    }
}