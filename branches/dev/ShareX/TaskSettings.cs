#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2008-2013 ShareX Developers

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

using HelpersLibMod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UploadersLib;

namespace ShareX
{
    public class TaskSettings
    {
        [Browsable(false)] 
        public bool UseDefaultAfterCaptureJob { get; set; }
        [Browsable(false)]
        public AfterCaptureTasks AfterCaptureJob { get; set; }

        [Browsable(false)] 
        public bool UseDefaultAfterUploadJob { get; set; }
        [Browsable(false)] 
        public AfterUploadTasks AfterUploadJob { get; set; }

        [Browsable(false)] 
        public bool UseDefaultDestinations { get; set; }
        [Browsable(false)] 
        public ImageDestination ImageDestination { get; set; }
        [Browsable(false)]
        public TextDestination TextDestination { get; set; }
        [Browsable(false)] 
        public FileDestination FileDestination { get; set; }
        [Browsable(false)]
        public UrlShortenerType URLShortenerDestination { get; set; }
        [Browsable(false)]
        public SocialNetworkingService SocialNetworkingServiceDestination { get; set; }
        [Browsable(false)] 
        public bool DisableNotifications { get; set; }

        [Category(ComponentModelStrings.ActivitiesUploadersText), DefaultValue("text"), Description("Text format e.g. csharp, cpp, etc.")]
        public string TextFormat { get; set; }

        [Category(ComponentModelStrings.ActivitiesUploadersText), DefaultValue("txt"), Description("File extension when saving text to the local hard disk.")]
        public string TextFileExtension { get; set; }

        public TaskSettings()
        {
            ApplyDefaultValues(this);
        }

        public TaskSettings(bool useDefaultSettings = false)
        {
            SetDefaultSettings(true);
            UseDefaultAfterCaptureJob = useDefaultSettings;
            UseDefaultAfterUploadJob = useDefaultSettings;
            UseDefaultDestinations = useDefaultSettings;
        }

        public bool SetDefaultSettings(bool forceDefaultSettings = false)
        {
            if (Program.Settings != null)
            {
                if (UseDefaultAfterCaptureJob || forceDefaultSettings)
                {
                    AfterCaptureJob = Program.Settings.AfterCaptureTasks;
                }

                if (UseDefaultAfterUploadJob || forceDefaultSettings)
                {
                    AfterUploadJob = Program.Settings.AfterUploadTasks;
                }

                if (UseDefaultDestinations || forceDefaultSettings)
                {
                    ImageDestination = Program.Settings.ImageUploaderDestination;
                    TextDestination = Program.Settings.TextUploaderDestination;
                    FileDestination = Program.Settings.FileUploaderDestination;
                    URLShortenerDestination = Program.Settings.URLShortenerDestination;
                    SocialNetworkingServiceDestination = Program.Settings.SocialServiceDestination;
                }

                return true;
            }

            return false;
        }

        public TaskSettings Clone()
        {
            return (TaskSettings)MemberwiseClone();
        }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }
    }
}