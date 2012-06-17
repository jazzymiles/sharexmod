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
        FreeHandRegion
    }
}