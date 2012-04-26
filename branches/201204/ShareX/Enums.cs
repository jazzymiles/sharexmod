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

    public enum ImageScaleType
    {
        Percentage, Width, Height, Specific
    }

    public enum EActivity
    {
        [Description("Capture active window")]
        CaptureActiveWindow,
        [Description("Capture screen")]
        CaptureScreen,
        [Description("Copy image to clipboard")]
        ClipboardCopyImage,
        [Description("Copy URL to clipboard")]
        ClipboardCopyLink,
        [Description("Annotate image")]
        ImageAnnotate,
        [Description("Save to file")]
        SaveToFile,
        [Description("Save to file with dialog")]
        SaveToFileWithDialog,
        [Description("Upload clipboard content")]
        UploadClipboard,
        [Description("Upload file")]
        UploadFile,
        [Description("Upload to remote host")]
        UploadToRemoteHost
    }
}