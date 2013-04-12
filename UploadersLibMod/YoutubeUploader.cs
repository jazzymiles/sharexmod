using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib;
using Google.Apis.Youtube.v3;
using UploadersLib.HelperClasses;
using System.IO;

namespace UploadersLibMod
{
    public class YoutubeUploader : FileUploader, IOAuth
    {
        public OAuthInfo AuthInfo { get; set; }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            return null;
        }


        public string GetAuthorizationURL()
        {
            throw new NotImplementedException();
        }

        public bool GetAccessToken(string verificationCode)
        {
            throw new NotImplementedException();
        }
    }
}
