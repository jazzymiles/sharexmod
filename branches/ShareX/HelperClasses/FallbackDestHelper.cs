using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib;

namespace ShareX
{
    public class FallbackDestHelper
    {
        public ImageDestination GetNextImageUploader(ImageDestination curr_dest)
        {
            ImageDestination dest = curr_dest;

            while (dest == curr_dest || !SettingsManager.ConfigUploaders.IsActive(dest))
            {
                dest = (ImageDestination)new Random().Next(Enum.GetValues(typeof(ImageDestination)).Length - 1);
            }

            return dest;
        }
    }
}