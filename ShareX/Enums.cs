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
using System.ComponentModel;

namespace ShareX
{
    public enum EImageFormat
    {
        PNG, JPEG, GIF, BMP, TIFF
    }

    public enum TaskJob
    {
        DataUpload, FileUpload, ImageUpload, TextUpload, ShortenURL
    }

    [Flags]
    public enum Subtask
    {
        None = 0,
        [Description("Annotate image")]
        AnnotateImage = 1 << 0,
        [Description("Add torn paper effect")]
        AnnotateImageAddTornEffect = 1 << 1,
        [Description("Add shadow border")]
        AnnotateImageAddShadowBorder = 1 << 2,
        [Description("Add watermark")]
        AddWatermark = 1 << 3,
        [Description("Show image effects studio")]
        ShowImageEffectsStudio = 1 << 4,
        [Description("Copy image to clipboard")]
        CopyImageToClipboard = 1 << 5,
        [Description("Save to file")]
        SaveImageToFile = 1 << 6,
        [Description("Upload to remote host")]
        UploadImageToHost = 1 << 7,
        [Description("Save to file with dialog")]
        SaveImageToFileWithDialog = 1 << 8,
        [Description("Send to Printer")]
        Print = 1 << 9,
    }

    [Flags]
    public enum TaskTextJob
    {
        None = 0,
        [Description("Copy to clipboard")]
        CopyToClipboard = 2,
        [Description("Save to file")]
        SaveToFile = 4,
        [Description("Upload to remote host")]
        UploadToHost = 8,
        [Description("Save to file with dialog")]
        SaveToFileWithDialog = 16,
        [Description("Send to Printer")]
        Print = 32,
    }

    [Flags]
    public enum TaskFileJob
    {
        None = 0,
        [Description("Upload to remote host")]
        UploadToHost = 8,
    }

    public enum ImageScaleType
    {
        Percentage, Width, Height, Specific
    }
}