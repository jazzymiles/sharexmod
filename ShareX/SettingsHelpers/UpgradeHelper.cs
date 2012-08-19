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
using System.Linq;
using System.Text;
using HelpersLib.Hotkeys2;
using HelpersLibMod;

namespace ShareX.SettingsHelpers
{
    public static class UpgradeHelper
    {
        public static void Upgrade_r125()
        {
            if (SettingsManager.ConfigCore.Workflows1.Count > 0)
            {
                Upgrade(SettingsManager.ConfigCore.Workflows1);
                SettingsManager.ConfigWorkflows.Workflows = SettingsManager.ConfigCore.Workflows1;
            }
        }

        public static void Upgrade_r175()
        {
            if (SettingsManager.ConfigWorkflows.Workflows.Count > 0)
            {
                Upgrade(SettingsManager.ConfigWorkflows.Workflows);
            }
        }

        private static void Upgrade(List<Workflow> workflows)
        {
            foreach (Workflow wf in workflows)
            {
                foreach (EActivity act in wf.Activities)
                {
                    switch (act)
                    {
                        case EActivity.AfterCaptureTasks:
                            wf.Settings.PerformGlobalAfterCaptureTasks = true;
                            break;

                        case EActivity.CaptureActiveMonitor:
                            wf.Hotkey = EHotkey.ActiveMonitor;
                            break;

                        case EActivity.CaptureActiveWindow:
                            wf.Hotkey = EHotkey.ActiveWindow;
                            break;

                        case EActivity.CaptureDiamondRegion:
                            wf.Hotkey = EHotkey.DiamondRegion;
                            break;

                        case EActivity.CaptureEllipseRegion:
                            wf.Hotkey = EHotkey.EllipseRegion;
                            break;

                        case EActivity.CaptureFreeHandRegion:
                            wf.Hotkey = EHotkey.FreeHandRegion;
                            break;

                        case EActivity.CapturePolygonRegion:
                            wf.Hotkey = EHotkey.PolygonRegion;
                            break;

                        case EActivity.CaptureRectangleRegion:
                            wf.Hotkey = EHotkey.RectangleRegion;
                            break;

                        case EActivity.CaptureRoundedRectangleRegion:
                            wf.Hotkey = EHotkey.RoundedRectangleRegion;
                            break;

                        case EActivity.CaptureScreen:
                            wf.Hotkey = EHotkey.FullScreen;
                            break;

                        case EActivity.CaptureTriangleRegion:
                            wf.Hotkey = EHotkey.TriangleRegion;
                            break;

                        case EActivity.CaptureWindowRectangle:
                            wf.Hotkey = EHotkey.WindowRectangle;
                            break;

                        case EActivity.ClipboardCopyImage:
                            wf.Subtasks |= Subtask.CopyImageToClipboard;
                            break;

                        case EActivity.ImageAnnotate:
                            wf.Subtasks |= Subtask.AnnotateImage;
                            break;

                        case EActivity.ImageAnnotateAddShadowBorder:
                            wf.Subtasks |= Subtask.AnnotateImageAddShadowBorder;
                            break;

                        case EActivity.ImageAnnotateAddTornEffect:
                            wf.Subtasks |= Subtask.AnnotateImageAddTornEffect;
                            break;

                        case EActivity.Printer:
                            wf.Subtasks |= Subtask.Print;
                            break;

                        case EActivity.SaveToFile:
                            wf.Subtasks |= Subtask.SaveToFile;
                            break;

                        case EActivity.SaveToFileWithDialog:
                            wf.Subtasks |= Subtask.SaveImageToFileWithDialog;
                            break;

                        case EActivity.ShortenLinkUsingBitly:
                            wf.Settings.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.BITLY);
                            break;

                        case EActivity.ShortenLinkUsingGoogle:
                            wf.Settings.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.Google);
                            break;

                        case EActivity.ShortenLinkUsingIsgd:
                            wf.Settings.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.ISGD);
                            break;

                        case EActivity.ShortenLinkUsingJmp:
                            wf.Settings.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.Jmp);
                            break;

                        case EActivity.ShortenLinkUsingTinyUrl:
                            wf.Settings.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.TINYURL);
                            break;

                        case EActivity.ShortenLinkUsingTurl:
                            wf.Settings.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.TURL);
                            break;

                        case EActivity.ShowImageEffectsStudio:
                            wf.Subtasks |= Subtask.ShowImageEffectsStudio;
                            break;

                        case EActivity.UploadClipboard:
                            wf.Hotkey = EHotkey.ClipboardUpload;
                            break;

                        case EActivity.UploadFile:
                            wf.Hotkey = EHotkey.FileUpload;
                            break;

                        case EActivity.UploadToDropbox:
                            wf.Settings.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.Dropbox);
                            break;

                        case EActivity.UploadToFlickr:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Flickr);
                            break;

                        case EActivity.UploadToFTP:
                            wf.Settings.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.FTP);
                            break;

                        case EActivity.UploadToImageShack:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.ImageShack);
                            break;

                        case EActivity.UploadToImgur:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Imgur);
                            break;

                        case EActivity.UploadToImmio:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Immio);
                            break;

                        case EActivity.UploadToMinus:
                            wf.Settings.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.Minus);
                            break;

                        case EActivity.UploadToPaste2:
                            wf.Settings.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.Paste2);
                            break;

                        case EActivity.UploadToPastebin:
                            wf.Settings.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.Pastebin);
                            break;

                        case EActivity.UploadToPastebinCa:
                            wf.Settings.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.PastebinCA);
                            break;

                        case EActivity.UploadToPhotobucket:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Photobucket);
                            break;

                        case EActivity.UploadToRapidShare:
                            wf.Settings.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.RapidShare);
                            break;

                        case EActivity.UploadToRemoteHost:
                            wf.Subtasks |= Subtask.UploadToRemoteHost;
                            break;

                        case EActivity.UploadToSendSpace:
                            wf.Settings.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.SendSpace);
                            break; ;
                        case EActivity.UploadToSlexy:
                            wf.Settings.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.Slexy);
                            break;

                        case EActivity.UploadToTinyPic:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.TinyPic);
                            break;

                        case EActivity.UploadToTwitPic:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Twitpic);
                            break;

                        case EActivity.UploadToTwitSnaps:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Twitsnaps);
                            break;

                        case EActivity.UploadToUploadScreenshot:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.UploadScreenshot);
                            break;

                        case EActivity.UploadToYfrog:
                            wf.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.yFrog);
                            break;
                    }
                }

                wf.Activities.Clear();
            }
        }
    }
}