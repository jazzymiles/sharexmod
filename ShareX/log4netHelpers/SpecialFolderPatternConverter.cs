using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Util;

namespace ShareX
{
    public class SpecialFolderPatternConverter : PatternConverter
    {
        override protected void Convert(System.IO.TextWriter writer, object state)
        {
            Environment.SpecialFolder specialFolder =
              (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), base.Option, true);

            writer.Write(Environment.GetFolderPath(specialFolder));
        }
    }
}