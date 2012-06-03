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
using HelpersLib;

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
        AnnotateImage = 2 << 0,
        [Description("Add torn paper effect")]
        AnnotateImageAddTornEffect = 2 << 1,
        [Description("Add shadow border")]
        AnnotateImageAddShadowBorder = 2 << 2,
        [Description("Add watermark")]
        AddWatermark = 2 << 3,
        [Description("Show image effects studio")]
        ShowImageEffectsStudio = 2 << 4,
        [Description("Copy image to clipboard")]
        CopyImageToClipboard = 2 << 5,
        [Description("Save to file")]
        SaveImageToFile = 2 << 6,
        [Description("Upload to remote host")]
        UploadImageToHost = 2 << 7,
        [Description("Save to file with dialog")]
        SaveImageToFileWithDialog = 2 << 8,
        [Description("Send to Printer")]
        Print = 2 << 9,
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

    public enum EListItemDoubleClickBehavior
    {
        [Description("Open link or file path (if exists)")]
        OpenUrlOrFile,
        [Description("Open file path or link (if exists)")]
        OpenFileOrUrl,
        [Description("Open link (if exists)")]
        OpenUrl,
        [Description("Open file path (if exists)")]
        OpenFile,
        [Description("Open directory of the file (if exists)")]
        OpenDirectory,
        [Description("Do nothing")]
        DoNothing,
    }
}