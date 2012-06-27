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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HelpersLib.Hotkeys2
{
    public enum EHotkey
    {
        [Description("Capture clipboard content (images, text or files)")]
        ClipboardUpload,

        [Description("File upload")]
        FileUpload,

        [Description("Capture fullscreen")]
        FullScreen,

        [Description("Capture active window")]
        ActiveWindow,

        [Description("Capture active Monitor")]
        ActiveMonitor,

        [Description("Capture window or rectangle")]
        WindowRectangle,

        [Description("Capture rectangle region")]
        RectangleRegion,

        [Description("Capture rounded rectangle region")]
        RoundedRectangleRegion,

        [Description("Capture ellipse region")]
        EllipseRegion,

        [Description("Capture triangle region")]
        TriangleRegion,

        [Description("Capture diamond region")]
        DiamondRegion,

        [Description("Capture polygon region")]
        PolygonRegion,

        [Description("Capture freehand region")]
        FreeHandRegion,

        [Description("Last region")]
        LastRegion
    }

    /// <summary>
    /// This Enum must not be restructured. New items must append at the end to avoid mapping to the wrong item.
    /// </summary>
    [Flags]
    [TypeConverter(typeof(EnumToStringUsingDescription))]
    public enum EActivity
    {
        [Description("Capture screen"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureScreen,

        [Description("Capture active monitor"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureActiveMonitor,

        [Description("Capture active window"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureActiveWindow,

        [Description("Capture window or rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureWindowRectangle,

        [Description("Capture rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureRectangleRegion,

        [Description("Capture rounded rectangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureRoundedRectangleRegion,

        [Description("Capture ellipse region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureEllipseRegion,

        [Description("Capture triangle region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureTriangleRegion,

        [Description("Capture diamond region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureDiamondRegion,

        [Description("Capture polygon region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CapturePolygonRegion,

        [Description("Capture freehand region"), Category(ComponentModelStrings.ActivitiesCapture)]
        CaptureFreeHandRegion,

        [Description("Upload clipboard content"), Category(ComponentModelStrings.ActivitiesCapture)]
        UploadClipboard,

        [Description("Upload file"), Category(ComponentModelStrings.ActivitiesCapture)]
        UploadFile,

        [Description("Copy image to clipboard"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        ClipboardCopyImage,

        [Description("Annotate image"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ImageAnnotate,

        [Description("Save to file"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFile,

        [Description("Save to file with dialog"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        SaveToFileWithDialog,

        [Description("Perform after capture tasks"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        AfterCaptureTasks,

        [Description("Upload to default remote host"), Category(ComponentModelStrings.ActivitiesUploaders)]
        UploadToRemoteHost,

        [Description("Send to printer"), Category(ComponentModelStrings.ActivitiesAfterCapture)]
        Printer,

        [Description("Upload to imageshack.us"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToImageShack,

        [Description("Upload to tinypic.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToTinyPic,

        [Description("Upload to imgur.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToImgur,

        [Description("Upload to flickr.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToFlickr,

        [Description("Upload to photobucket.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToPhotobucket,

        [Description("Upload to uploadscreenshot.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToUploadScreenshot,

        [Description("Upload to twitpic.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToTwitPic,

        [Description("Upload to twitsnaps.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToTwitSnaps,

        [Description("Upload to yfrog.com"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToYfrog,

        [Description("Upload to imm.io"), Category(ComponentModelStrings.ActivitiesUploadersImages)]
        UploadToImmio,

        [Description("Upload to pastebin.com"), Category(ComponentModelStrings.ActivitiesUploadersText)]
        UploadToPastebin,

        [Description("Upload to pastebin.ca"), Category(ComponentModelStrings.ActivitiesUploadersText)]
        UploadToPastebinCa,

        [Description("Upload to paste2.org"), Category(ComponentModelStrings.ActivitiesUploadersText)]
        UploadToPaste2,

        [Description("Upload to slexy.org"), Category(ComponentModelStrings.ActivitiesUploadersText)]
        UploadToSlexy,

        [Description("Upload to dropbox.com"), Category(ComponentModelStrings.ActivitiesUploadersFiles)]
        UploadToDropbox,

        [Description("Upload to rapidshare.com"), Category(ComponentModelStrings.ActivitiesUploadersFiles)]
        UploadToRapidShare,

        [Description("Upload to sendspace.com"), Category(ComponentModelStrings.ActivitiesUploadersFiles)]
        UploadToSendSpace,

        [Description("Upload to minus.com"), Category(ComponentModelStrings.ActivitiesUploadersFiles)]
        UploadToMinus,

        [Description("Upload to FTP Server"), Category(ComponentModelStrings.ActivitiesUploadersFiles)]
        UploadToFTP,

        [Description("Shorten URL using goo.gl"), Category(ComponentModelStrings.ActivitiesUploadersLinks)]
        ShortenLinkUsingGoogle,

        [Description("Shorten URL using bit.ly"), Category(ComponentModelStrings.ActivitiesUploadersLinks)]
        ShortenLinkUsingBitly,

        [Description("Shorten URL using j.mp"), Category(ComponentModelStrings.ActivitiesUploadersLinks)]
        ShortenLinkUsingJmp,

        [Description("Shorten URL using is.gd"), Category(ComponentModelStrings.ActivitiesUploadersLinks)]
        ShortenLinkUsingIsgd,

        [Description("Shorten URL using tinyurl.com"), Category(ComponentModelStrings.ActivitiesUploadersLinks)]
        ShortenLinkUsingTinyUrl,

        [Description("Shorten URL using turl.ca"), Category(ComponentModelStrings.ActivitiesUploadersLinks)]
        ShortenLinkUsingTurl,

        [Description("Open with Image Effects Studio"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ShowImageEffectsStudio,

        [Description("Add torn effect"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ImageAnnotateAddTornEffect,

        [Description("Add shadow border"), Category(ComponentModelStrings.ActivitiesAfterCaptureEffects)]
        ImageAnnotateAddShadowBorder,
    }
}