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
using System.IO;
using HelpersLib;
using HelpersLib.Hotkeys2;
using HistoryLib;
using ShareX.HelperClasses;
using UploadersLib;
using UploadersLib.HelperClasses;

namespace ShareX
{
    public class UploadInfo
    {
        public int ID { get; set; }

        public string Status { get; set; }

        public TaskJob Job { get; set; }

        public Subtask Jobs { get; set; }

        public ProgressManager Progress { get; set; }

        private string _filePath;

        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _filePath = value;
                    FileName = Path.GetFileName(value);
                }
            }
        }

        public string FolderPath { get; set; }

        public string FileName { get; set; }

        public EDataType DataType { get; set; }

        public EDataType UploadDestination { get; set; }

        private string _destination;

        public string Destination
        {
            get
            {
                return _destination;
            }
        }

        public void SetDestination(DestConfig DestConfig)
        {
            switch (UploadDestination)
            {
                case EDataType.File:
                    _destination = DestConfig.ToStringFileUploaders();
                    break;
                case EDataType.Image:
                    _destination = DestConfig.ToStringImageUploaders();
                    break;
                case EDataType.Text:
                    _destination = DestConfig.ToStringTextUploaders();
                    break;
                case EDataType.URL:
                    _destination = DestConfig.ToStringLinkUploaders();
                    break;
                case EDataType.SocialNetworkingServiceRequest:
                    _destination = DestConfig.ToStringSocialNetworkingServices();
                    break;
            }
        }

        public DateTime StartTime { get; set; }

        public DateTime UploadTime { get; set; }

        public TimeSpan UploadDuration
        {
            get { return UploadTime - StartTime; }
        }

        public UploadResult Result { get; set; }

        public UploadInfo()
        {
            Result = new UploadResult();
            Jobs = Subtask.UploadToRemoteHost;
        }

        public HistoryItem GetHistoryItem()
        {
            return new HistoryItem
            {
                Filename = FileName,
                Filepath = FilePath,
                DateTimeUtc = UploadTime,
                Type = DataType.ToString(),
                Host = Destination,
                URL = Result.URL,
                ThumbnailURL = Result.ThumbnailURL,
                DeletionURL = Result.DeletionURL,
                ShortenedURL = Result.ShortenedURL
            };
        }
    }
}