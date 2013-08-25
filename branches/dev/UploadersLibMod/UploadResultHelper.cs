using HelpersLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UploadersLib;
using UploadersLib.HelperClasses;

namespace UploadersLibMod
{
    public class UploadResultHelper
    {
        private UploadResult Result = null;

        #region Links

        public UploadResultHelper(UploadResult result)
        {
            Result = result;
        }

        public string GetUrlByType(LinkFormatEnum type, string longUrl)
        {
            switch (type)
            {
                case LinkFormatEnum.FULL:
                    return longUrl;
                case LinkFormatEnum.FULL_TINYURL:
                    return this.Result.ShortenedURL;
                case LinkFormatEnum.FULL_IMAGE_FORUMS:
                    return GetFullImageForumsUrl(longUrl);
                case LinkFormatEnum.FULL_IMAGE_HTML:
                    return GetFullImageHTML(longUrl);
                case LinkFormatEnum.FULL_IMAGE_WIKI:
                    return GetFullImageWiki(longUrl);
                case LinkFormatEnum.FULL_IMAGE_MEDIAWIKI:
                    return GetFullImageMediaWikiInnerLink(longUrl);
                case LinkFormatEnum.LINKED_THUMBNAIL:
                    return GetLinkedThumbnailForumUrl(longUrl);
                case LinkFormatEnum.LinkedThumbnailHtml:
                    return GetLinkedThumbnailHtmlUrl(longUrl);
                case LinkFormatEnum.LINKED_THUMBNAIL_WIKI:
                    return GetLinkedThumbnailWikiUrl(longUrl);
                case LinkFormatEnum.THUMBNAIL:
                    return this.Result.ThumbnailURL;
            }

            return this.Result.URL;
        }

        public string GetFullImageUrl()
        {
            return this.Result.URL;
        }

        public string GetFullImageForumsUrl(string url)
        {
            if (!string.IsNullOrEmpty(url) && Helpers.IsImageFile(url))
            {
                return string.Format("[IMG]{0}[/IMG]", url);
            }
            return string.Empty;
        }

        public string GetFullImageHTML(string url)
        {
            if (!string.IsNullOrEmpty(url) && Helpers.IsImageFile(url))
            {
                return string.Format("<img src=\"{0}\"/>", url);
            }
            return string.Empty;
        }

        public string GetFullImageWiki(string url)
        {
            if (!string.IsNullOrEmpty(url) && Helpers.IsImageFile(url))
            {
                return string.Format("[{0}]", url);
            }
            return string.Empty;
        }

        public string GetFullImageMediaWikiInnerLink(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;
            int index = url.IndexOf("Image:");
            if (index < 0)
                return string.Empty;
            string name = url.Substring(index + "Image:".Length);
            return string.Format("[[Image:{0}]]", name);
        }

        public string GetThumbnailUrl()
        {
            return this.Result.ThumbnailURL;
        }

        public string GetLinkedThumbnailForumUrl(string full)
        {
            string thumb = GetThumbnailUrl();
            if (!string.IsNullOrEmpty(full) && !string.IsNullOrEmpty(thumb))
            {
                return string.Format("[URL={0}][IMG]{1}[/IMG][/URL]", full, thumb);
            }
            return string.Empty;
        }

        private string GetLinkedThumbnailHtmlUrl(string url)
        {
            string th = GetThumbnailUrl();
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(th))
            {
                return string.Format("<a target='_blank' href=\"{0}\"><img src=\"{1}\" border='0'/></a>", url, th);
            }
            return string.Empty;
        }

        public string GetLinkedThumbnailWikiUrl(string full)
        {
            // e.g. [http://code.google.com http://code.google.com/images/code_sm.png]
            string thumb = GetThumbnailUrl();
            if (!string.IsNullOrEmpty(full) && !string.IsNullOrEmpty(thumb))
            {
                return string.Format("[{0} {1}]", full, thumb);
            }
            return string.Empty;
        }

        public string GetLocalFilePathAsUri(string fp)
        {
            if (File.Exists(fp))
            {
                try
                {
                    return new Uri(fp).AbsoluteUri;
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteException(ex);
                }
            }
            return string.Empty;
        }

        #endregion Links
    }
}