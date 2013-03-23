using Greenshot.Configuration;
using Greenshot.Controls;
using Greenshot.Destinations;
using Greenshot.Helpers;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using GreenshotPlugin.UnmanagedHelpers;

/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2013  Thomas Braun, Jens Klingen, Robin Krom
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
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Greenshot
{
    /// <summary>
    /// Description of SettingsForm.
    /// </summary>
    public partial class SettingsForm : BaseForm
    {
        private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(SettingsForm));
        private static EditorConfiguration editorConfiguration = IniConfig.GetIniSection<EditorConfiguration>();
        private ToolTip toolTip = new ToolTip();
        private bool inHotkey = false;

        public SettingsForm()
            : base()
        {
            InitializeComponent();

            // Make sure the store isn't called to early, that's why we do it manually
            ManualStoreFields = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Icon = GreenshotPlugin.Core.GreenshotResources.getGreenshotIcon();

            UpdateUI();
            ExpertSettingsEnableState(false);
            DisplaySettings();
            CheckSettings();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (!inHotkey)
                    {
                        DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                    break;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        /// <summary>
        /// This is a method to popululate the ComboBox
        /// with the items from the enumeration
        /// </summary>
        /// <param name="comboBox">ComboBox to populate</param>
        /// <param name="enumeration">Enum to populate with</param>
        private void PopulateComboBox<ET>(ComboBox comboBox) where ET : struct
        {
            ET[] availableValues = (ET[])Enum.GetValues(typeof(ET));
            PopulateComboBox<ET>(comboBox, availableValues, availableValues[0]);
        }

        /// <summary>
        /// This is a method to popululate the ComboBox
        /// with the items from the enumeration
        /// </summary>
        /// <param name="comboBox">ComboBox to populate</param>
        /// <param name="enumeration">Enum to populate with</param>
        private void PopulateComboBox<ET>(ComboBox comboBox, ET[] availableValues, ET selectedValue) where ET : struct
        {
            comboBox.Items.Clear();
            string enumTypeName = typeof(ET).Name;
            foreach (ET enumValue in availableValues)
            {
                comboBox.Items.Add(Language.Translate(enumValue));
            }
            comboBox.SelectedItem = Language.Translate(selectedValue);
        }

        /// <summary>
        /// Get the selected enum value from the combobox, uses generics
        /// </summary>
        /// <param name="comboBox">Combobox to get the value from</param>
        /// <returns>The generics value of the combobox</returns>
        private ET GetSelected<ET>(ComboBox comboBox)
        {
            string enumTypeName = typeof(ET).Name;
            string selectedValue = comboBox.SelectedItem as string;
            ET[] availableValues = (ET[])Enum.GetValues(typeof(ET));
            ET returnValue = availableValues[0];
            foreach (ET enumValue in availableValues)
            {
                string translation = Language.GetString(enumTypeName + "." + enumValue.ToString());
                if (translation.Equals(selectedValue))
                {
                    returnValue = enumValue;
                    break;
                }
            }
            return returnValue;
        }

        private void SetWindowCaptureMode(WindowCaptureMode selectedWindowCaptureMode)
        {
            WindowCaptureMode[] availableModes;
            if (!DWM.isDWMEnabled())
            {
                // Remove DWM from configuration, as DWM is disabled!
                if (coreConfiguration.WindowCaptureMode == WindowCaptureMode.Aero || coreConfiguration.WindowCaptureMode == WindowCaptureMode.AeroTransparent)
                {
                    coreConfiguration.WindowCaptureMode = WindowCaptureMode.GDI;
                }
                availableModes = new WindowCaptureMode[] { WindowCaptureMode.Auto, WindowCaptureMode.Screen, WindowCaptureMode.GDI };
            }
            else
            {
                availableModes = new WindowCaptureMode[] { WindowCaptureMode.Auto, WindowCaptureMode.Screen, WindowCaptureMode.GDI, WindowCaptureMode.Aero, WindowCaptureMode.AeroTransparent };
            }
            PopulateComboBox<WindowCaptureMode>(combobox_window_capture_mode, availableModes, selectedWindowCaptureMode);
        }

        /// <summary>
        /// Update the UI to reflect the language and other text settings
        /// </summary>
        private void UpdateUI()
        {
            if (coreConfiguration.HideExpertSettings)
            {
                tabcontrol.Controls.Remove(tab_expert);
            }

            // Removing, otherwise we keep getting the event multiple times!

            // Initialize the Language ComboBox
            // Set datasource last to prevent problems
            // See: http://www.codeproject.com/KB/database/scomlistcontrolbinding.aspx?fid=111644

            UpdateClipboardFormatDescriptions();
        }

        // Check the settings and somehow visibly mark when something is incorrect
        private bool CheckSettings()
        {
            bool settingsOk = true;

            return settingsOk;
        }

        private void StorageLocationChanged(object sender, System.EventArgs e)
        {
            CheckSettings();
        }

        /// <summary>
        /// Show all clipboard format descriptions in the current language
        /// </summary>
        private void UpdateClipboardFormatDescriptions()
        {
            foreach (ListViewItem item in listview_clipboardformats.Items)
            {
                ClipboardFormat cf = (ClipboardFormat)item.Tag;
                item.Text = Language.Translate(cf);
            }
        }

        private void DisplaySettings()
        {
            colorButton_window_background.SelectedColor = coreConfiguration.DWMBackgroundColor;

            // Expert mode, the clipboard formats
            foreach (ClipboardFormat clipboardFormat in Enum.GetValues(typeof(ClipboardFormat)))
            {
                ListViewItem item = listview_clipboardformats.Items.Add(Language.Translate(clipboardFormat));
                item.Tag = clipboardFormat;
                item.Checked = coreConfiguration.ClipboardFormats.Contains(clipboardFormat);
            }

            SetWindowCaptureMode(coreConfiguration.WindowCaptureMode);

            // Disable editing when the value is fixed
            combobox_window_capture_mode.Enabled = !coreConfiguration.Values["WindowCaptureMode"].IsFixed;

            numericUpDownWaitTime.Value = coreConfiguration.CaptureDelay >= 0 ? coreConfiguration.CaptureDelay : 0;
            numericUpDownWaitTime.Enabled = !coreConfiguration.Values["CaptureDelay"].IsFixed;

            // Autostart checkbox logic.
        }

        private void SaveSettings()
        {
            // retrieve the set clipboard formats
            List<ClipboardFormat> clipboardFormats = new List<ClipboardFormat>();
            foreach (int index in listview_clipboardformats.CheckedIndices)
            {
                ListViewItem item = listview_clipboardformats.Items[index];
                if (item.Checked)
                {
                    clipboardFormats.Add((ClipboardFormat)item.Tag);
                }
            }
            coreConfiguration.ClipboardFormats = clipboardFormats;

            coreConfiguration.WindowCaptureMode = GetSelected<WindowCaptureMode>(combobox_window_capture_mode);

            List<string> destinations = new List<string>();

            coreConfiguration.OutputDestinations = destinations;
            coreConfiguration.CaptureDelay = (int)numericUpDownWaitTime.Value;
            coreConfiguration.DWMBackgroundColor = colorButton_window_background.SelectedColor;
        }

        private void Settings_cancelClick(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Settings_okayClick(object sender, System.EventArgs e)
        {
            if (CheckSettings())
            {
                SaveSettings();
                StoreFields();

                // Make sure the current language & settings are reflected in the Main-context menu
                DialogResult = DialogResult.OK;
            }
        }

        private void BtnPatternHelpClick(object sender, EventArgs e)
        {
            string filenamepatternText = Language.GetString(LangKey.settings_message_filenamepattern);

            // Convert %NUM% to ${NUM} for old language files!
            filenamepatternText = Regex.Replace(filenamepatternText, "%([a-zA-Z_0-9]+)%", @"${$1}");
            MessageBox.Show(filenamepatternText, Language.GetString(LangKey.settings_filenamepattern));
        }

        private void Combobox_languageSelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the combobox values BEFORE changing the language
            //EmailFormat selectedEmailFormat = GetSelected<EmailFormat>(combobox_emailformat);
            WindowCaptureMode selectedWindowCaptureMode = GetSelected<WindowCaptureMode>(combobox_window_capture_mode);

            // Reflect language changes to the settings form
            UpdateUI();

            // Reflect Language changes form
            ApplyLanguage();

            // Update the email & windows capture mode
            //SetEmailFormat(selectedEmailFormat);
            SetWindowCaptureMode(selectedWindowCaptureMode);
        }

        private void Combobox_window_capture_modeSelectedIndexChanged(object sender, EventArgs e)
        {
            int windowsVersion = Environment.OSVersion.Version.Major;
            WindowCaptureMode mode = GetSelected<WindowCaptureMode>(combobox_window_capture_mode);
            if (windowsVersion >= 6)
            {
                switch (mode)
                {
                    case WindowCaptureMode.Aero:
                        colorButton_window_background.Visible = true;
                        return;
                }
            }
            colorButton_window_background.Visible = false;
        }

        protected override void OnFieldsFilled()
        {
            // the color radio button is not actually bound to a setting, but checked when monochrome/grayscale are not checked
            if (!radioBtnGrayScale.Checked && !radioBtnMonochrome.Checked)
            {
                radioBtnColorPrint.Checked = true;
            }
        }

        /// <summary>
        /// Set the enable state of the expert settings
        /// </summary>
        /// <param name="state"></param>
        private void ExpertSettingsEnableState(bool state)
        {
            listview_clipboardformats.Enabled = state;
            checkbox_autoreducecolors.Enabled = state;
            checkbox_optimizeforrdp.Enabled = state;
            checkbox_thumbnailpreview.Enabled = state;
            textbox_footerpattern.Enabled = state;
            textbox_counter.Enabled = state;
            checkbox_suppresssavedialogatclose.Enabled = state;
            checkbox_checkunstableupdates.Enabled = state;
            checkbox_minimizememoryfootprint.Enabled = state;
            checkbox_reuseeditor.Enabled = state;
        }

        /// <summary>
        /// Called if the "I know what I am doing" on the settings form is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_enableexpert_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            ExpertSettingsEnableState(checkBox.Checked);
        }
    }

    public class ListviewWithDestinationComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is ListViewItem))
            {
                return (0);
            }
            if (!(y is ListViewItem))
            {
                return (0);
            }

            ListViewItem l1 = (ListViewItem)x;
            ListViewItem l2 = (ListViewItem)y;

            IDestination firstDestination = l1.Tag as IDestination;
            IDestination secondDestination = l2.Tag as IDestination;

            if (secondDestination == null)
            {
                return 1;
            }
            if (firstDestination.Priority == secondDestination.Priority)
            {
                return firstDestination.Description.CompareTo(secondDestination.Description);
            }
            return firstDestination.Priority - secondDestination.Priority;
        }
    }
}