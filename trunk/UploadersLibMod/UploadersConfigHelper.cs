using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;
using UploadersLib;

namespace UploadersLibMod
{
   public  class UploadersConfigHelper
    {
       private UploadersConfig _config;

       public UploadersConfigHelper(UploadersConfig config)
       {
           _config = config;
       }

       public int GetFtpIndex(EDataType dataType)
       {
           switch (dataType)
           {
               case EDataType.Image:
                   return _config.FTPSelectedImage;
               case EDataType.Text:
                   return _config.FTPSelectedText;
               default:
                   return _config.FTPSelectedFile;
           }
       }
    }
}
