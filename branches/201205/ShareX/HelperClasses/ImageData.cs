﻿#region License Information (GPL v3)

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
using System.Xml.Serialization;
using HelpersLib;

namespace ShareX.HelperClasses
{
    public class ImageData : IDisposable
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private EImageFormat imageFormat = EImageFormat.PNG;
        [XmlIgnore]
        public MemoryStream ImageStream { get; set; }
        [XmlIgnore]
        public Image Image = null;
        public string WindowText { get; set; }
        public string Filename { get; set; }
        public string FilePath { get; set; }

        public bool IsPrepared
        {
            get
            {
                return ImageStream != null;
            }
        }

        private ImageData()
        {
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

        private string PrepareFilenameWithoutExtension()
        {
            string windowText = this.WindowText;
            if (string.IsNullOrEmpty(windowText))
                windowText = "Screenshot";
            int fnweLenMax = Program.Settings.MaxFilenameLength - 5;
            int wtLenMax = fnweLenMax - 20;

            // Truncate window text
            if (wtLenMax > 0 && windowText.Length > wtLenMax)
                windowText = windowText.Substring(0, wtLenMax);

            NameParser parser = new NameParser { Picture = this.Image, WindowText = windowText };

            return parser.Convert(Program.Settings.NameFormatPattern);
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
            if (Program.Settings.ImageAutoResize)
            {
                img = ResizeImage(img, Program.Settings.ImageScaleType);
            }

            MemoryStream stream = img.SaveImage(Program.Settings.ImageFormat);

            int sizeLimit = Program.Settings.ImageSizeLimit * 1024;
            if (Program.Settings.ImageFormat != Program.Settings.ImageFormat2 && sizeLimit > 0 && stream.Length > sizeLimit)
            {
                stream = img.SaveImage(Program.Settings.ImageFormat2);
                imageFormat = Program.Settings.ImageFormat2;
            }
            else
            {
                imageFormat = Program.Settings.ImageFormat;
            }

            stream.Position = 0;

            return stream;
        }

        private Image ResizeImage(Image img, ImageScaleType scaleType)
        {
            float width = 0, height = 0;

            switch (scaleType)
            {
                case ImageScaleType.Percentage:
                    width = img.Width * (Program.Settings.ImageScalePercentageWidth / 100f);
                    height = img.Height * (Program.Settings.ImageScalePercentageHeight / 100f);
                    break;
                case ImageScaleType.Width:
                    width = Program.Settings.ImageScaleToWidth;
                    height = Program.Settings.ImageKeepAspectRatio ? img.Height * (width / img.Width) : img.Height;
                    break;
                case ImageScaleType.Height:
                    height = Program.Settings.ImageScaleToHeight;
                    width = Program.Settings.ImageKeepAspectRatio ? img.Width * (height / img.Height) : img.Width;
                    break;
                case ImageScaleType.Specific:
                    width = Program.Settings.ImageScaleSpecificWidth;
                    height = Program.Settings.ImageScaleSpecificHeight;
                    break;
            }

            if (width > 0 && height > 0)
            {
                return CaptureHelpers.ResizeImage(img, (int)width, (int)height, Program.Settings.ImageUseSmoothScaling);
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

        public string WriteToFile(string folderPath)
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

                FilePath = Path.Combine(folderPath, Filename);
                ImageStream.WriteToFile(FilePath);
                return FilePath;
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