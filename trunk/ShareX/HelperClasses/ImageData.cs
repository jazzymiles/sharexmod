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

using System;
using System.Drawing;
using System.IO;
using HelpersLib;
using ShareX.SettingsHelpers;
using UploadersLib;

namespace ShareX.HelperClasses
{
    public class ImageData : IDisposable
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private EImageFormat imageFormat = EImageFormat.PNG;

        public MemoryStream ImageStream { get; private set; }

        public Image Image = null;

        public string WindowText { get; private set; }

        public string Filename { get; private set; }

        public UserConfig ConfigUser { get; set; }

        public bool IsPrepared
        {
            get
            {
                return ImageStream != null;
            }
        }

        private ImageData()
        {
            if (SettingsManager.UserConfigResetEvent == null)
                SettingsManager.UserConfigResetEvent.WaitOne();

            Filename = "Require image encoding...";
        }

        public ImageData(Image img, bool screenshot = false)
            : this()
        {
            this.Image = img;
            if (screenshot)
                this.WindowText = NativeMethods.GetForegroundWindowText();
            this.Filename = PrepareFilenameWithoutExtension();
        }

        public Image ImageExported
        {
            get
            {
                if (Image != null)
                    return (Bitmap)Image.Clone();

                return null;
            }
        }

        public static ImageData GetNew(string fp)
        {
            ImageData id = null;
            if (File.Exists(fp))
            {
                id = new ImageData(Helpers.ImageFromFile(fp));
            }
            return id;
        }

        private string PrepareFilenameWithoutExtension()
        {
            string windowText = this.WindowText;
            if (string.IsNullOrEmpty(windowText))
                windowText = "Screenshot";
            int fnweLenMax = SettingsManager.ConfigCore.MaxFilenameLength - 5;
            int wtLenMax = fnweLenMax - 20;

            // Truncate window text
            if (wtLenMax > 0 && windowText.Length > wtLenMax)
                windowText = windowText.Substring(0, wtLenMax);

            NameParser parser = new NameParser { Picture = this.Image, WindowText = windowText };

            return parser.Convert(SettingsManager.ConfigCore.NameFormatPattern);
        }

        private string PrepareFilename()
        {
            string ext = "png";

            switch (this.imageFormat)
            {
                case EImageFormat.PNG:
                    ext = "png";
                    break;
                case EImageFormat.JPEG:
                    ext = "jpg";
                    break;
                case EImageFormat.GIF:
                    ext = "gif";
                    break;
                case EImageFormat.BMP:
                    ext = "bmp";
                    break;
                case EImageFormat.TIFF:
                    ext = "tif";
                    break;
            }

            string fnwe = PrepareFilenameWithoutExtension();

            return string.Format("{0}.{1}", fnwe, ext);
        }

        private MemoryStream PrepareImage(Image img, out EImageFormat imageFormat)
        {
            if (ConfigUser == null)
                ConfigUser = SettingsManager.ConfigUser;

            if (ConfigUser.ImageAutoResize)
            {
                img = ResizeImage(img, ConfigUser.ImageScaleType);
            }

            MemoryStream stream = img.SaveImage(ConfigUser.ImageFormat);

            int sizeLimit = ConfigUser.ImageSizeLimit * 1024;
            if (ConfigUser.ImageFormat != ConfigUser.ImageFormat2 && sizeLimit > 0 && stream.Length > sizeLimit)
            {
                stream = img.SaveImage(ConfigUser.ImageFormat2);
                imageFormat = ConfigUser.ImageFormat2;
            }
            else
            {
                imageFormat = ConfigUser.ImageFormat;
            }

            stream.Position = 0;

            return stream;
        }

        private Image ResizeImage(Image img, ImageScaleType scaleType)
        {
            if (ConfigUser == null)
                ConfigUser = SettingsManager.ConfigUser;

            float width = 0, height = 0;

            switch (scaleType)
            {
                case ImageScaleType.Percentage:
                    width = img.Width * (ConfigUser.ImageScalePercentageWidth / 100f);
                    height = img.Height * (ConfigUser.ImageScalePercentageHeight / 100f);
                    break;
                case ImageScaleType.Width:
                    width = ConfigUser.ImageScaleToWidth;
                    height = ConfigUser.ImageKeepAspectRatio ? img.Height * (width / img.Width) : img.Height;
                    break;
                case ImageScaleType.Height:
                    height = ConfigUser.ImageScaleToHeight;
                    width = ConfigUser.ImageKeepAspectRatio ? img.Width * (height / img.Height) : img.Width;
                    break;
                case ImageScaleType.Specific:
                    width = ConfigUser.ImageScaleSpecificWidth;
                    height = ConfigUser.ImageScaleSpecificHeight;
                    break;
            }

            if (width > 0 && height > 0)
            {
                return CaptureHelpers.ResizeImage(img, (int)width, (int)height, ConfigUser.ImageUseSmoothScaling);
            }

            return img;
        }

        public void PrepareImageAndFilename()
        {
            if (Image != null)
                log.DebugFormat("Preparing image {0}x{1} and filename", Image.Width, Image.Height);

            this.ImageStream = PrepareImage(this.Image, out imageFormat);
            this.Filename = PrepareFilename();
        }

        /// <summary>
        /// Writes image to file.
        /// Prerequisites: Outputs -> LocalDisk
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public string WriteToFile(string folderPath)
        {
            if (SettingsManager.ConfigCore.Outputs.HasFlag(OutputEnum.LocalDisk))
            {
                if (ImageStream == null)
                {
                    log.Warn("ImageStream was null. Preparing image and filename.");
                    PrepareImageAndFilename();
                }

                if (ImageStream != null && !string.IsNullOrEmpty(Filename) && !string.IsNullOrEmpty(folderPath))
                {
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string filePath = Path.Combine(folderPath, Filename);
                    ImageStream.WriteToFile(filePath);
                    return filePath;
                }
            }
            else
            {
                log.Warn("WritoToFile method was called when Outputs do not specify to save to Local Disk.");
            }

            return string.Empty;
        }

        public void Dispose()
        {
            if (ImageStream != null) ImageStream.Dispose();
            if (Image != null) Image.Dispose();
        }
    }
}