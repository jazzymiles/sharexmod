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
        DataUpload, FileUpload, ImageUpload, TextUpload, ShortenURL, ShareURL
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

    [TypeConverter(typeof(EnumToStringUsingDescription))]
    public enum EImageEditorOnKeyLock
    {
        [Description("None")]
        None,

        [Description("Caps Lock")]
        CapsLock,

        [Description("Num Lock")]
        NumLock,

        [Description("Scroll Lock")]
        ScrollLock
    }

    public enum EScreencastFileType
    {
        [Description("Windows Media Video")]
        wmv,

        [Description("Expression Encoder Screen Capture")]
        xesc,

        [Description("Graphics Interchange Format")]
        gif,

        [Description("Audio Video Interleave")]
        avi,
    }

    public enum EBitrateType
    {
        [Description("Used to specify a constant bitrate")]
        ConstantBitrate,

        [Description("Used to specify a variable constrained bitrate")]
        VariableConstrainedBitrate,

        [Description("Used to specify a variable quality bitrate")]
        VariableQualityBitrate,

        [Description("Used to specify a variable unconstrained bitrate")]
        VariableUnconstrainedBitrate,
    }
}