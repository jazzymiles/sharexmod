#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2012 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using HelpersLib;
using HelpersLibMod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UploadersLib
{
    [Serializable]
    public class DestConfig
    {
        public List<OutputEnum> Outputs = new List<OutputEnum>();
        public List<ClipboardContentEnum> TaskClipboardContent = new List<ClipboardContentEnum>();
        public List<LinkFormatEnum> LinkFormat = new List<LinkFormatEnum>();

        public List<ImageDestination> ImageUploaders = new List<ImageDestination>();
        public List<TextDestination> TextUploaders = new List<TextDestination>();
        public List<FileDestination> FileUploaders = new List<FileDestination>();
        public List<UrlShortenerType> LinkUploaders = new List<UrlShortenerType>();
        public List<SocialNetworkingService> SocialNetworkingServices = new List<SocialNetworkingService>();

        public List<ImageDestination> ImageUploaders2 = new List<ImageDestination>();
        public List<TextDestination> TextUploaders2 = new List<TextDestination>();
        public List<FileDestination> FileUploaders2 = new List<FileDestination>();

        [Category(ComponentModelStrings.ActivitiesUploadersText), DefaultValue("text"), Description("Text format e.g. csharp, cpp, etc.")]
        public string TextFormat { get; set; }

        [Browsable(false)]
        public bool IsEmptyAny
        {
            get
            {
                return FileUploaders.Count == 0 || TextUploaders.Count == 0 || ImageUploaders.Count == 0;
            }
        }

        [Browsable(false)]
        public bool IsEmptyAll
        {
            get
            {
                return FileUploaders.Count == 0 && TextUploaders.Count == 0 && ImageUploaders.Count == 0;
            }
        }

        #region Add uploader

        public void AddUploader(FileDestination uploader)
        {
            if (!FileUploaders.Contains(uploader))
                FileUploaders.Add(uploader);
        }

        public void AddUploader(ImageDestination uploader)
        {
            if (!ImageUploaders.Contains(uploader))
                ImageUploaders.Add(uploader);
        }

        public void AddUploader(TextDestination uploader)
        {
            if (!TextUploaders.Contains(uploader))
                TextUploaders.Add(uploader);
        }

        public void AddUploader(UrlShortenerType uploader)
        {
            if (!LinkUploaders.Contains(uploader))
                LinkUploaders.Add(uploader);
        }

        public void AddUploader(SocialNetworkingService uploader)
        {
            if (!SocialNetworkingServices.Contains(uploader))
                SocialNetworkingServices.Add(uploader);
        }

        #endregion Add uploader

        #region Add Secondary Uploader

        public void AddUploader2(FileDestination uploader)
        {
            if (!FileUploaders2.Contains(uploader))
                FileUploaders2.Add(uploader);
        }

        public void AddUploader2(ImageDestination uploader)
        {
            if (!ImageUploaders2.Contains(uploader))
                ImageUploaders2.Add(uploader);
        }

        public void AddUploader2(TextDestination uploader)
        {
            if (!TextUploaders2.Contains(uploader))
                TextUploaders2.Add(uploader);
        }

        #endregion Add Secondary Uploader

        #region Remove uploader

        public void RemoveUploader(FileDestination uploader)
        {
            if (FileUploaders.Contains(uploader))
                FileUploaders.Remove(uploader);
        }

        public void RemoveUploader(ImageDestination uploader)
        {
            if (ImageUploaders.Contains(uploader))
                ImageUploaders.Remove(uploader);
        }

        public void RemoveUploader(TextDestination uploader)
        {
            if (TextUploaders.Contains(uploader))
                TextUploaders.Remove(uploader);
        }

        public void RemoveUploader(UrlShortenerType uploader)
        {
            if (LinkUploaders.Contains(uploader))
                LinkUploaders.Remove(uploader);
        }

        public void RemoveUploader(SocialNetworkingService uploader)
        {
            if (SocialNetworkingServices.Contains(uploader))
                SocialNetworkingServices.Remove(uploader);
        }

        #endregion Remove uploader

        #region Remove Secondary Uploader

        public void RemoveUploader2(FileDestination uploader)
        {
            if (FileUploaders2.Contains(uploader))
                FileUploaders2.Remove(uploader);
        }

        public void RemoveUploader2(ImageDestination uploader)
        {
            if (ImageUploaders2.Contains(uploader))
                ImageUploaders2.Remove(uploader);
        }

        public void RemoveUploader2(TextDestination uploader)
        {
            if (TextUploaders2.Contains(uploader))
                TextUploaders2.Remove(uploader);
        }

        #endregion Remove Secondary Uploader

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string temp = ToStringOutputs();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(temp);
            }

            temp = ToStringImageUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(" using ");
                sb.Append(temp);
            }

            temp = ToStringTextUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(", ");
                sb.Append(temp);
            }

            temp = ToStringFileUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(", ");
                sb.Append(temp);
            }

            temp = ToStringLinkUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(", ");
                sb.Append(temp);
            }

            return sb.ToString();
        }

        private string ToString<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T ut in list)
            {
                Enum en = (Enum)Convert.ChangeType(ut, typeof(Enum));
                sb.Append(en.GetDescription());
                sb.Append(", ");
            }
            if (sb.Length > 2)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public string ToStringOutputs()
        {
            return ToString<OutputEnum>(Outputs);
        }

        public string ToStringImageUploaders()
        {
            return ToString<ImageDestination>(ImageUploaders);
        }

        public string ToStringTextUploaders()
        {
            return ToString<TextDestination>(TextUploaders);
        }

        public string ToStringFileUploaders()
        {
            return ToString<FileDestination>(FileUploaders);
        }

        public string ToStringLinkUploaders()
        {
            return ToString<UrlShortenerType>(LinkUploaders);
        }

        public string ToStringSocialNetworkingServices()
        {
            return ToString<SocialNetworkingService>(SocialNetworkingServices);
        }

        public DestConfig Clone()
        {
            DestConfig dc = new DestConfig();

            dc.FileUploaders.AddRange(this.FileUploaders);
            dc.ImageUploaders.AddRange(this.ImageUploaders);
            dc.TextUploaders.AddRange(this.TextUploaders);
            dc.LinkUploaders.AddRange(this.LinkUploaders);
            dc.SocialNetworkingServices.AddRange(this.SocialNetworkingServices);

            dc.FileUploaders2.AddRange(this.FileUploaders2);
            dc.ImageUploaders2.AddRange(this.ImageUploaders2);
            dc.TextUploaders2.AddRange(this.TextUploaders2);

            return dc;
        }
    }
}