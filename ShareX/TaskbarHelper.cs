using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX
{
    public static class TaskbarHelper
    {
        #region Windows 7 Taskbar

        public static readonly string appId = Application.ProductName;  // need for Windows 7 Taskbar
        private static readonly string progId = Application.ProductName; // need for Windows 7 Taskbar

        public static IntPtr WindowHandle = IntPtr.Zero;
        public static TaskbarManager WindowsTaskbar;

        #endregion Windows 7 Taskbar

        public static void TaskbarSetProgressState(TaskbarProgressBarState tbps)
        {
            TaskbarSetProgressState(FormsHelper.Main, tbps);
        }

        private static void TaskbarSetProgressState(Form form, TaskbarProgressBarState tbps)
        {
            if (form != null && WindowHandle != IntPtr.Zero && TaskbarManager.IsPlatformSupported && WindowsTaskbar != null)
            {
                WindowsTaskbar.SetProgressState(tbps, windowHandle: WindowHandle);
            }
        }

        public static void TaskbarSetProgressValue(int progress)
        {
            TaskbarSetProgressValue(FormsHelper.Main, progress);
        }

        private static void TaskbarSetProgressValue(Form form, int progress)
        {
            if (form != null && WindowHandle != IntPtr.Zero && TaskbarManager.IsPlatformSupported && WindowsTaskbar != null)
            {
                WindowsTaskbar.SetProgressValue(progress, 100, windowHandle: WindowHandle);
            }
        }
    }
}