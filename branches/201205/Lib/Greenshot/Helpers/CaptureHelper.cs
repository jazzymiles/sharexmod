/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2012  Thomas Braun, Jens Klingen, Robin Krom
 *
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Greenshot.Configuration;
using Greenshot.Destinations;
using Greenshot.Drawing;
using Greenshot.Forms;
using Greenshot.Helpers;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Core;
using GreenshotPlugin.UnmanagedHelpers;

namespace Greenshot.Helpers
{
    /// <summary>
    /// CaptureHelper contains all the capture logic
    /// </summary>
    public class CaptureHelper
    {
        private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(CaptureHelper));
        private static CoreConfiguration conf = IniConfig.GetIniSection<CoreConfiguration>();
        private static ScreenCaptureHelper screenCapture = null;
        private List<WindowDetails> windows = new List<WindowDetails>();
        private WindowDetails selectedCaptureWindow = null;
        private Rectangle captureRect = Rectangle.Empty;
        private bool captureMouseCursor = false;
        private ICapture capture = null;
        private CaptureMode captureMode;
        private ScreenCaptureMode screenCaptureMode = ScreenCaptureMode.Auto;
        private Thread windowDetailsThread = null;

        public static void ImportCapture(ICapture captureToImport)
        {
            CaptureHelper captureHelper = new CaptureHelper(CaptureMode.File);
            captureHelper.capture = captureToImport;
            captureHelper.HandleCapture();
        }

        public CaptureHelper(CaptureMode captureMode)
        {
            this.captureMode = captureMode;
            capture = new Capture();
        }

        public CaptureHelper(CaptureMode captureMode, bool captureMouseCursor)
            : this(captureMode)
        {
            this.captureMouseCursor = captureMouseCursor;
        }

        public CaptureHelper(CaptureMode captureMode, bool captureMouseCursor, ScreenCaptureMode screenCaptureMode)
            : this(captureMode)
        {
            this.captureMouseCursor = captureMouseCursor;
            this.screenCaptureMode = screenCaptureMode;
        }

        public CaptureHelper(CaptureMode captureMode, bool captureMouseCursor, IDestination destination)
            : this(captureMode, captureMouseCursor)
        {
            capture.CaptureDetails.AddDestination(destination);
        }

        public WindowDetails SelectedCaptureWindow
        {
            get
            {
                return selectedCaptureWindow;
            }
            set
            {
                selectedCaptureWindow = value;
            }
        }

        private void DoCaptureFeedback()
        {
            if (conf.PlayCameraSound)
            {
                SoundHelper.Play();
            }
        }

        /// <summary>
        /// Pre-Initialization for CaptureWithFeedback, this will get all the windows before we change anything
        /// </summary>
        private Thread PrepareForCaptureWithFeedback()
        {
            windows = new List<WindowDetails>();

            Thread getWindowDetailsThread = new Thread(delegate()
            {
                // Start Enumeration of "active" windows
                foreach (WindowDetails window in WindowDetails.GetAllWindows())
                {
                    // Window should be visible and not ourselves
                    if (!window.Visible)
                    {
                        continue;
                    }

                    // Skip empty
                    Rectangle windowRectangle = window.WindowRectangle;
                    Size windowSize = windowRectangle.Size;
                    if (windowSize.Width == 0 || windowSize.Height == 0)
                    {
                        continue;
                    }

                    // Make sure the details are retrieved once
                    window.FreezeDetails();

                    // Force children retrieval, sometimes windows close on losing focus and this is solved by caching
                    int goLevelDeep = 3;
                    if (conf.WindowCaptureAllChildLocations)
                    {
                        goLevelDeep = 20;
                    }
                    window.GetChildren(goLevelDeep);
                    lock (windows)
                    {
                        windows.Add(window);
                    }

                    // Get window rectangle as capture Element
                    CaptureElement windowCaptureElement = new CaptureElement(windowRectangle);
                    if (capture == null)
                    {
                        break;
                    }
                    capture.Elements.Add(windowCaptureElement);

                    if (!window.HasParent)
                    {
                        // Get window client rectangle as capture Element, place all the other "children" in there
                        Rectangle clientRectangle = window.ClientRectangle;
                        CaptureElement windowClientCaptureElement = new CaptureElement(clientRectangle);
                        windowCaptureElement.Children.Add(windowClientCaptureElement);
                        AddCaptureElementsForWindow(windowClientCaptureElement, window, goLevelDeep);
                    }
                    else
                    {
                        AddCaptureElementsForWindow(windowCaptureElement, window, goLevelDeep);
                    }
                }
                lock (windows)
                {
                    windows = WindowDetails.SortByZOrder(IntPtr.Zero, windows);
                }
            });
            getWindowDetailsThread.Name = "Retrieve window details";
            getWindowDetailsThread.IsBackground = true;
            getWindowDetailsThread.Start();
            return getWindowDetailsThread;
        }

        private void AddCaptureElementsForWindow(ICaptureElement parentElement, WindowDetails parentWindow, int level)
        {
            foreach (WindowDetails childWindow in parentWindow.Children)
            {
                // Make sure the details are retrieved once
                childWindow.FreezeDetails();
                Rectangle childRectangle = childWindow.WindowRectangle;
                Size s1 = childRectangle.Size;
                childRectangle.Intersect(parentElement.Bounds);
                if (childRectangle.Width > 0 && childRectangle.Height > 0)
                {
                    CaptureElement childCaptureElement = new CaptureElement(childRectangle);
                    parentElement.Children.Add(childCaptureElement);
                    if (level > 0)
                    {
                        AddCaptureElementsForWindow(childCaptureElement, childWindow, level - 1);
                    }
                }
            }
        }

        private void HandleCapture()
        {
            // Flag to see if the image was "exported" so the FileEditor doesn't
            // ask to save the file as long as nothing is done.
            bool outputMade = false;

            // Make sure the user sees that the capture is made
            if (capture.CaptureDetails.CaptureMode == CaptureMode.File || capture.CaptureDetails.CaptureMode == CaptureMode.Clipboard)
            {
                // Maybe not "made" but the original is still there... somehow
                outputMade = true;
            }
            else
            {
                DoCaptureFeedback();
            }

            LOG.Debug("A capture of: " + capture.CaptureDetails.Title);

            // Create Surface with capture, this way elements can be added automatically (like the mouse cursor)
            Surface surface = new Surface(capture);
            surface.Modified = !outputMade;

            // As the surfaces copies the reference to the image, make sure the image is not being disposed (a trick to save memory)
            capture.Image = null;

            // Get CaptureDetails as we need it even after the capture is disposed
            ICaptureDetails captureDetails = capture.CaptureDetails;
            bool canDisposeSurface = true;

            if (captureDetails.HasDestination(Destinations.PickerDestination.DESIGNATION))
            {
                DestinationHelper.ExportCapture(false, Destinations.PickerDestination.DESIGNATION, surface, captureDetails);
                captureDetails.CaptureDestinations.Clear();
                canDisposeSurface = false;
            }

            // Disable capturing
            captureMode = CaptureMode.None;
            // Dispose the capture, we don't need it anymore (the surface copied all information and we got the title (if any)).
            capture.Dispose();
            capture = null;

            int destinationCount = captureDetails.CaptureDestinations.Count;
            if (destinationCount > 0)
            {
                // Flag to detect if we need to create a temp file for the email
                // or use the file that was written
                foreach (IDestination destination in captureDetails.CaptureDestinations)
                {
                    if (PickerDestination.DESIGNATION.Equals(destination.Designation))
                    {
                        continue;
                    }
                    LOG.InfoFormat("Calling destination {0}", destination.Description);

                    bool destinationOk = destination.ExportCapture(false, surface, captureDetails);
                    if (Destinations.EditorDestination.DESIGNATION.Equals(destination.Designation) && destinationOk)
                    {
                        canDisposeSurface = false;
                    }
                }
            }
            if (canDisposeSurface)
            {
                surface.Dispose();
            }
        }

        private bool CaptureActiveWindow()
        {
            bool presupplied = false;
            LOG.Debug("CaptureActiveWindow");
            if (selectedCaptureWindow != null)
            {
                LOG.Debug("Using supplied window");
                presupplied = true;
            }
            else
            {
                selectedCaptureWindow = WindowDetails.GetActiveWindow();
                if (selectedCaptureWindow != null)
                {
                    LOG.DebugFormat("Capturing window: {0} with {1}", selectedCaptureWindow.Text, selectedCaptureWindow.WindowRectangle);
                }
            }
            if (selectedCaptureWindow == null || (!presupplied && selectedCaptureWindow.Iconic))
            {
                LOG.Warn("No window to capture!");
                // Nothing to capture, code up in the stack will capture the full screen
                return false;
            }
            if (selectedCaptureWindow != null && selectedCaptureWindow.Iconic)
            {
                // Restore the window making sure it's visible!
                // This is done mainly for a screen capture, but some applications like Excel and TOAD have weird behaviour!
                selectedCaptureWindow.Restore();
            }
            selectedCaptureWindow.ToForeground();
            selectedCaptureWindow = SelectCaptureWindow(selectedCaptureWindow);
            if (selectedCaptureWindow == null)
            {
                LOG.Warn("No window to capture, after SelectCaptureWindow!");
                // Nothing to capture, code up in the stack will capture the full screen
                return false;
            }
            // Fix for Bug #3430560
            RuntimeConfig.LastCapturedRegion = selectedCaptureWindow.WindowRectangle;
            bool returnValue = CaptureWindow(selectedCaptureWindow, capture, conf.WindowCaptureMode) != null;
            return returnValue;
        }

        /// <summary>
        /// Select the window to capture, this has logic which takes care of certain special applications
        /// like TOAD or Excel
        /// </summary>
        /// <param name="windowToCapture">WindowDetails with the target Window</param>
        /// <returns>WindowDetails with the target Window OR a replacement</returns>
        public static WindowDetails SelectCaptureWindow(WindowDetails windowToCapture)
        {
            Rectangle windowRectangle = windowToCapture.WindowRectangle;
            if (windowToCapture.Iconic || windowRectangle.Width == 0 || windowRectangle.Height == 0)
            {
                LOG.WarnFormat("Window {0} has nothing to capture, using workaround to find other window of same process.", windowToCapture.Text);
                // Trying workaround, the size 0 arrises with e.g. Toad.exe, has a different Window when minimized
                WindowDetails linkedWindow = WindowDetails.GetLinkedWindow(windowToCapture);
                if (linkedWindow != null)
                {
                    windowRectangle = linkedWindow.WindowRectangle;
                    windowToCapture = linkedWindow;
                }
                else
                {
                    return null;
                }
            }
            return windowToCapture;
        }

        /// <summary>
        /// Check if Process uses PresentationFramework.dll -> meaning it uses WPF
        /// </summary>
        /// <param name="process">Proces to check for the presentation framework</param>
        /// <returns>true if the process uses WPF</returns>
        private static bool isWPF(Process process)
        {
            if (process != null)
            {
                try
                {
                    foreach (ProcessModule module in process.Modules)
                    {
                        if (module.ModuleName.StartsWith("PresentationFramework"))
                        {
                            LOG.InfoFormat("Found that Process {0} uses {1}, assuming it's using WPF", process.ProcessName, module.FileName);
                            return true;
                        }
                    }
                }
                catch (Exception)
                {
                    // Access denied on the modules
                    LOG.WarnFormat("No access on the modules from process {0}, assuming WPF is used.", process.ProcessName);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Capture the supplied Window
        /// </summary>
        /// <param name="windowToCapture">Window to capture</param>
        /// <param name="captureForWindow">The capture to store the details</param>
        /// <param name="windowCaptureMode">What WindowCaptureMode to use</param>
        /// <returns></returns>
        public static ICapture CaptureWindow(WindowDetails windowToCapture, ICapture captureForWindow, WindowCaptureMode windowCaptureMode)
        {
            if (captureForWindow == null)
            {
                captureForWindow = new Capture();
            }
            Rectangle windowRectangle = windowToCapture.WindowRectangle;
            if (windowToCapture.Iconic)
            {
                // Restore the window making sure it's visible!
                windowToCapture.Restore();
            }

            // When Vista & DWM (Aero) enabled
            bool dwmEnabled = DWM.isDWMEnabled();
            // get process name to be able to exclude certain processes from certain capture modes
            Process process = windowToCapture.Process;
            bool isAutoMode = windowCaptureMode == WindowCaptureMode.Auto;
            // For WindowCaptureMode.Auto we check:
            // 1) Is window IE, use IE Capture
            // 2) Is Windows >= Vista & DWM enabled: use DWM
            // 3) Otherwise use GDI (Screen might be also okay but might lose content)
            if (isAutoMode)
            {
                // Take default screen
                windowCaptureMode = WindowCaptureMode.Screen;

                // Change to GDI, if allowed
                if (conf.isGDIAllowed(process))
                {
                    if (!dwmEnabled && isWPF(process))
                    {
                        // do not use GDI, as DWM is not enabled and the application uses PresentationFramework.dll -> isWPF
                        LOG.InfoFormat("Not using GDI for windows of process {0}, as the process uses WPF", process.ProcessName);
                    }
                    else
                    {
                        windowCaptureMode = WindowCaptureMode.GDI;
                    }
                }

                // Change to DWM, if enabled and allowed
                if (dwmEnabled)
                {
                    if (conf.isDWMAllowed(process))
                    {
                        windowCaptureMode = WindowCaptureMode.Aero;
                    }
                }
            }
            else if (windowCaptureMode == WindowCaptureMode.Aero || windowCaptureMode == WindowCaptureMode.AeroTransparent)
            {
                if (!dwmEnabled || !conf.isDWMAllowed(process))
                {
                    // Take default screen
                    windowCaptureMode = WindowCaptureMode.Screen;
                    // Change to GDI, if allowed
                    if (conf.isGDIAllowed(process))
                    {
                        windowCaptureMode = WindowCaptureMode.GDI;
                    }
                }
            }
            else if (windowCaptureMode == WindowCaptureMode.GDI && !conf.isGDIAllowed(process))
            {
                // GDI not allowed, take screen
                windowCaptureMode = WindowCaptureMode.Screen;
            }

            LOG.InfoFormat("Capturing window with mode {0}", windowCaptureMode);
            bool captureTaken = false;
            // Try to capture
            while (!captureTaken)
            {
                if (windowCaptureMode == WindowCaptureMode.GDI)
                {
                    ICapture tmpCapture = null;
                    if (conf.isGDIAllowed(process))
                    {
                        tmpCapture = windowToCapture.CaptureWindow(captureForWindow);
                    }
                    if (tmpCapture != null)
                    {
                        captureForWindow = tmpCapture;
                        captureTaken = true;
                    }
                    else
                    {
                        // A problem, try Screen
                        windowCaptureMode = WindowCaptureMode.Screen;
                    }
                }
                else if (windowCaptureMode == WindowCaptureMode.Aero || windowCaptureMode == WindowCaptureMode.AeroTransparent)
                {
                    ICapture tmpCapture = null;
                    if (conf.isDWMAllowed(process))
                    {
                        tmpCapture = windowToCapture.CaptureDWMWindow(captureForWindow, windowCaptureMode, isAutoMode);
                    }
                    if (tmpCapture != null)
                    {
                        captureForWindow = tmpCapture;
                        captureTaken = true;
                    }
                    else
                    {
                        // A problem, try GDI
                        windowCaptureMode = WindowCaptureMode.GDI;
                    }
                }
                else
                {
                    // Screen capture
                    windowRectangle.Intersect(captureForWindow.ScreenBounds);
                    try
                    {
                        captureForWindow = WindowCapture.CaptureRectangle(captureForWindow, windowRectangle);
                        captureTaken = true;
                    }
                    catch (Exception e)
                    {
                        LOG.Error("Problem capturing", e);
                        return null;
                    }
                }
            }

            if (captureForWindow != null && windowToCapture != null)
            {
                captureForWindow.CaptureDetails.Title = windowToCapture.Text;
                ((Bitmap)captureForWindow.Image).SetResolution(captureForWindow.CaptureDetails.DpiX, captureForWindow.CaptureDetails.DpiY);
            }

            return captureForWindow;
        }

        #region capture with feedback

        private void CaptureWithFeedback()
        {
            using (CaptureForm captureForm = new CaptureForm(capture, windows))
            {
                DialogResult result = captureForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    selectedCaptureWindow = captureForm.SelectedCaptureWindow;
                    captureRect = captureForm.CaptureRectangle;
                    // Get title
                    if (selectedCaptureWindow != null)
                    {
                        capture.CaptureDetails.Title = selectedCaptureWindow.Text;
                    }

                    // Experimental code
                    if (capture.CaptureDetails.CaptureMode == CaptureMode.Video)
                    {
                        if (captureForm.UsedCaptureMode == CaptureMode.Window)
                        {
                            screenCapture = new ScreenCaptureHelper(selectedCaptureWindow);
                        }
                        else if (captureForm.UsedCaptureMode == CaptureMode.Region)
                        {
                            screenCapture = new ScreenCaptureHelper(captureRect);
                        }
                        if (screenCapture != null)
                        {
                            screenCapture.RecordMouse = capture.CursorVisible;
                            if (screenCapture.Start(25))
                            {
                                return;
                            }
                            // User clicked cancel or a problem occured
                            screenCapture.Stop();
                            screenCapture = null;
                            return;
                        }
                    }

                    if (captureRect.Height > 0 && captureRect.Width > 0)
                    {
                        if (windowDetailsThread != null)
                        {
                            windowDetailsThread.Join();
                        }
                        // Take the captureRect, this already is specified as bitmap coordinates
                        capture.Crop(captureRect);
                        // save for re-capturing later and show recapture context menu option
                        RuntimeConfig.LastCapturedRegion = captureRect;
                        HandleCapture();
                    }
                }
            }
        }

        #endregion capture with feedback
    }
}