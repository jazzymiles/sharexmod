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
namespace Greenshot {
	partial class SettingsForm {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.settings_cancel = new GreenshotPlugin.Controls.GreenshotButton();
            this.settings_confirm = new GreenshotPlugin.Controls.GreenshotButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabcontrol = new System.Windows.Forms.TabControl();
            this.tab_capture = new GreenshotPlugin.Controls.GreenshotTabPage();
            this.groupbox_editor = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.checkbox_editor_match_capture_size = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.groupbox_iecapture = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.checkbox_ie_capture = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.groupbox_windowscapture = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.colorButton_window_background = new Greenshot.Controls.ColorButton();
            this.label_window_capture_mode = new GreenshotPlugin.Controls.GreenshotLabel();
            this.checkbox_capture_windows_interactive = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.combobox_window_capture_mode = new System.Windows.Forms.ComboBox();
            this.groupbox_capture = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.checkbox_notifications = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_playsound = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_capture_mousepointer = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.numericUpDownWaitTime = new System.Windows.Forms.NumericUpDown();
            this.label_waittime = new GreenshotPlugin.Controls.GreenshotLabel();
            this.tab_printer = new GreenshotPlugin.Controls.GreenshotTabPage();
            this.groupBoxColors = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.checkboxPrintInverted = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.radioBtnColorPrint = new GreenshotPlugin.Controls.GreenshotRadioButton();
            this.radioBtnGrayScale = new GreenshotPlugin.Controls.GreenshotRadioButton();
            this.radioBtnMonochrome = new GreenshotPlugin.Controls.GreenshotRadioButton();
            this.groupBoxPrintLayout = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.checkboxDateTime = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkboxAllowShrink = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkboxAllowEnlarge = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkboxAllowRotate = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkboxAllowCenter = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_alwaysshowprintoptionsdialog = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.tab_plugins = new GreenshotPlugin.Controls.GreenshotTabPage();
            this.groupbox_plugins = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.listview_plugins = new System.Windows.Forms.ListView();
            this.button_pluginconfigure = new GreenshotPlugin.Controls.GreenshotButton();
            this.tab_expert = new GreenshotPlugin.Controls.GreenshotTabPage();
            this.groupbox_expert = new GreenshotPlugin.Controls.GreenshotGroupBox();
            this.checkbox_reuseeditor = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_minimizememoryfootprint = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_checkunstableupdates = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_suppresssavedialogatclose = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.label_counter = new GreenshotPlugin.Controls.GreenshotLabel();
            this.textbox_counter = new GreenshotPlugin.Controls.GreenshotTextBox();
            this.label_footerpattern = new GreenshotPlugin.Controls.GreenshotLabel();
            this.textbox_footerpattern = new GreenshotPlugin.Controls.GreenshotTextBox();
            this.checkbox_thumbnailpreview = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_optimizeforrdp = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_autoreducecolors = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.label_clipboardformats = new GreenshotPlugin.Controls.GreenshotLabel();
            this.checkbox_enableexpert = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.listview_clipboardformats = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabcontrol.SuspendLayout();
            this.tab_capture.SuspendLayout();
            this.groupbox_editor.SuspendLayout();
            this.groupbox_iecapture.SuspendLayout();
            this.groupbox_windowscapture.SuspendLayout();
            this.groupbox_capture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).BeginInit();
            this.tab_printer.SuspendLayout();
            this.groupBoxColors.SuspendLayout();
            this.groupBoxPrintLayout.SuspendLayout();
            this.tab_plugins.SuspendLayout();
            this.groupbox_plugins.SuspendLayout();
            this.tab_expert.SuspendLayout();
            this.groupbox_expert.SuspendLayout();
            this.SuspendLayout();
            // 
            // settings_cancel
            // 
            this.settings_cancel.LanguageKey = "CANCEL";
            this.settings_cancel.Location = new System.Drawing.Point(455, 495);
            this.settings_cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.settings_cancel.Name = "settings_cancel";
            this.settings_cancel.Size = new System.Drawing.Size(94, 29);
            this.settings_cancel.TabIndex = 7;
            this.settings_cancel.Text = "Cancel";
            this.settings_cancel.UseVisualStyleBackColor = true;
            this.settings_cancel.Click += new System.EventHandler(this.Settings_cancelClick);
            // 
            // settings_confirm
            // 
            this.settings_confirm.LanguageKey = "OK";
            this.settings_confirm.Location = new System.Drawing.Point(354, 495);
            this.settings_confirm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.settings_confirm.Name = "settings_confirm";
            this.settings_confirm.Size = new System.Drawing.Size(94, 29);
            this.settings_confirm.TabIndex = 6;
            this.settings_confirm.Text = "Ok";
            this.settings_confirm.UseVisualStyleBackColor = true;
            this.settings_confirm.Click += new System.EventHandler(this.Settings_okayClick);
            // 
            // tabcontrol
            // 
            this.tabcontrol.Controls.Add(this.tab_capture);
            this.tabcontrol.Controls.Add(this.tab_printer);
            this.tabcontrol.Controls.Add(this.tab_plugins);
            this.tabcontrol.Controls.Add(this.tab_expert);
            this.tabcontrol.Location = new System.Drawing.Point(15, 16);
            this.tabcontrol.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabcontrol.Name = "tabcontrol";
            this.tabcontrol.SelectedIndex = 0;
            this.tabcontrol.Size = new System.Drawing.Size(539, 471);
            this.tabcontrol.TabIndex = 17;
            // 
            // tab_capture
            // 
            this.tab_capture.Controls.Add(this.groupbox_editor);
            this.tab_capture.Controls.Add(this.groupbox_iecapture);
            this.tab_capture.Controls.Add(this.groupbox_windowscapture);
            this.tab_capture.Controls.Add(this.groupbox_capture);
            this.tab_capture.LanguageKey = "settings_capture";
            this.tab_capture.Location = new System.Drawing.Point(4, 25);
            this.tab_capture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tab_capture.Name = "tab_capture";
            this.tab_capture.Size = new System.Drawing.Size(531, 442);
            this.tab_capture.TabIndex = 3;
            this.tab_capture.Text = "Capture";
            this.tab_capture.UseVisualStyleBackColor = true;
            // 
            // groupbox_editor
            // 
            this.groupbox_editor.Controls.Add(this.checkbox_editor_match_capture_size);
            this.groupbox_editor.LanguageKey = "settings_editor";
            this.groupbox_editor.Location = new System.Drawing.Point(5, 334);
            this.groupbox_editor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_editor.Name = "groupbox_editor";
            this.groupbox_editor.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_editor.Size = new System.Drawing.Size(520, 62);
            this.groupbox_editor.TabIndex = 27;
            this.groupbox_editor.TabStop = false;
            this.groupbox_editor.Text = "Editor";
            // 
            // checkbox_editor_match_capture_size
            // 
            this.checkbox_editor_match_capture_size.LanguageKey = "editor_match_capture_size";
            this.checkbox_editor_match_capture_size.Location = new System.Drawing.Point(8, 24);
            this.checkbox_editor_match_capture_size.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_editor_match_capture_size.Name = "checkbox_editor_match_capture_size";
            this.checkbox_editor_match_capture_size.PropertyName = "MatchSizeToCapture";
            this.checkbox_editor_match_capture_size.SectionName = "Editor";
            this.checkbox_editor_match_capture_size.Size = new System.Drawing.Size(496, 30);
            this.checkbox_editor_match_capture_size.TabIndex = 26;
            this.checkbox_editor_match_capture_size.Text = "Match capture size";
            this.checkbox_editor_match_capture_size.UseVisualStyleBackColor = true;
            // 
            // groupbox_iecapture
            // 
            this.groupbox_iecapture.Controls.Add(this.checkbox_ie_capture);
            this.groupbox_iecapture.LanguageKey = "settings_iecapture";
            this.groupbox_iecapture.Location = new System.Drawing.Point(5, 264);
            this.groupbox_iecapture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_iecapture.Name = "groupbox_iecapture";
            this.groupbox_iecapture.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_iecapture.Size = new System.Drawing.Size(520, 62);
            this.groupbox_iecapture.TabIndex = 2;
            this.groupbox_iecapture.TabStop = false;
            this.groupbox_iecapture.Text = "Internet Explorer capture";
            // 
            // checkbox_ie_capture
            // 
            this.checkbox_ie_capture.LanguageKey = "settings_iecapture";
            this.checkbox_ie_capture.Location = new System.Drawing.Point(8, 24);
            this.checkbox_ie_capture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_ie_capture.Name = "checkbox_ie_capture";
            this.checkbox_ie_capture.PropertyName = "IECapture";
            this.checkbox_ie_capture.Size = new System.Drawing.Size(505, 30);
            this.checkbox_ie_capture.TabIndex = 26;
            this.checkbox_ie_capture.Text = "Internet Explorer capture";
            this.checkbox_ie_capture.UseVisualStyleBackColor = true;
            // 
            // groupbox_windowscapture
            // 
            this.groupbox_windowscapture.Controls.Add(this.colorButton_window_background);
            this.groupbox_windowscapture.Controls.Add(this.label_window_capture_mode);
            this.groupbox_windowscapture.Controls.Add(this.checkbox_capture_windows_interactive);
            this.groupbox_windowscapture.Controls.Add(this.combobox_window_capture_mode);
            this.groupbox_windowscapture.LanguageKey = "settings_windowscapture";
            this.groupbox_windowscapture.Location = new System.Drawing.Point(5, 156);
            this.groupbox_windowscapture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_windowscapture.Name = "groupbox_windowscapture";
            this.groupbox_windowscapture.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_windowscapture.Size = new System.Drawing.Size(520, 100);
            this.groupbox_windowscapture.TabIndex = 1;
            this.groupbox_windowscapture.TabStop = false;
            this.groupbox_windowscapture.Text = "Window capture";
            // 
            // colorButton_window_background
            // 
            this.colorButton_window_background.AutoSize = true;
            this.colorButton_window_background.Image = ((System.Drawing.Image)(resources.GetObject("colorButton_window_background.Image")));
            this.colorButton_window_background.Location = new System.Drawing.Point(468, 46);
            this.colorButton_window_background.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorButton_window_background.Name = "colorButton_window_background";
            this.colorButton_window_background.SelectedColor = System.Drawing.Color.White;
            this.colorButton_window_background.Size = new System.Drawing.Size(36, 38);
            this.colorButton_window_background.TabIndex = 45;
            this.colorButton_window_background.UseVisualStyleBackColor = true;
            // 
            // label_window_capture_mode
            // 
            this.label_window_capture_mode.LanguageKey = "settings_window_capture_mode";
            this.label_window_capture_mode.Location = new System.Drawing.Point(8, 58);
            this.label_window_capture_mode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_window_capture_mode.Name = "label_window_capture_mode";
            this.label_window_capture_mode.Size = new System.Drawing.Size(256, 29);
            this.label_window_capture_mode.TabIndex = 26;
            this.label_window_capture_mode.Text = "Window capture mode";
            // 
            // checkbox_capture_windows_interactive
            // 
            this.checkbox_capture_windows_interactive.LanguageKey = "settings_capture_windows_interactive";
            this.checkbox_capture_windows_interactive.Location = new System.Drawing.Point(11, 24);
            this.checkbox_capture_windows_interactive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_capture_windows_interactive.Name = "checkbox_capture_windows_interactive";
            this.checkbox_capture_windows_interactive.PropertyName = "CaptureWindowsInteractive";
            this.checkbox_capture_windows_interactive.Size = new System.Drawing.Size(492, 22);
            this.checkbox_capture_windows_interactive.TabIndex = 19;
            this.checkbox_capture_windows_interactive.Text = "Use interactive window capture mode";
            this.checkbox_capture_windows_interactive.UseVisualStyleBackColor = true;
            // 
            // combobox_window_capture_mode
            // 
            this.combobox_window_capture_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_window_capture_mode.FormattingEnabled = true;
            this.combobox_window_capture_mode.Location = new System.Drawing.Point(271, 54);
            this.combobox_window_capture_mode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.combobox_window_capture_mode.MaxDropDownItems = 15;
            this.combobox_window_capture_mode.Name = "combobox_window_capture_mode";
            this.combobox_window_capture_mode.Size = new System.Drawing.Size(188, 24);
            this.combobox_window_capture_mode.TabIndex = 27;
            this.combobox_window_capture_mode.SelectedIndexChanged += new System.EventHandler(this.Combobox_window_capture_modeSelectedIndexChanged);
            // 
            // groupbox_capture
            // 
            this.groupbox_capture.Controls.Add(this.checkbox_notifications);
            this.groupbox_capture.Controls.Add(this.checkbox_playsound);
            this.groupbox_capture.Controls.Add(this.checkbox_capture_mousepointer);
            this.groupbox_capture.Controls.Add(this.numericUpDownWaitTime);
            this.groupbox_capture.Controls.Add(this.label_waittime);
            this.groupbox_capture.LanguageKey = "settings_capture";
            this.groupbox_capture.Location = new System.Drawing.Point(5, 5);
            this.groupbox_capture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_capture.Name = "groupbox_capture";
            this.groupbox_capture.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_capture.Size = new System.Drawing.Size(520, 144);
            this.groupbox_capture.TabIndex = 0;
            this.groupbox_capture.TabStop = false;
            this.groupbox_capture.Text = "Capture";
            // 
            // checkbox_notifications
            // 
            this.checkbox_notifications.LanguageKey = "settings_shownotify";
            this.checkbox_notifications.Location = new System.Drawing.Point(14, 74);
            this.checkbox_notifications.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_notifications.Name = "checkbox_notifications";
            this.checkbox_notifications.PropertyName = "ShowTrayNotification";
            this.checkbox_notifications.Size = new System.Drawing.Size(499, 30);
            this.checkbox_notifications.TabIndex = 26;
            this.checkbox_notifications.Text = "Show notifications";
            this.checkbox_notifications.UseVisualStyleBackColor = true;
            // 
            // checkbox_playsound
            // 
            this.checkbox_playsound.LanguageKey = "settings_playsound";
            this.checkbox_playsound.Location = new System.Drawing.Point(14, 49);
            this.checkbox_playsound.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_playsound.Name = "checkbox_playsound";
            this.checkbox_playsound.PropertyName = "PlayCameraSound";
            this.checkbox_playsound.Size = new System.Drawing.Size(499, 30);
            this.checkbox_playsound.TabIndex = 18;
            this.checkbox_playsound.Text = "Play camera sound";
            this.checkbox_playsound.UseVisualStyleBackColor = true;
            // 
            // checkbox_capture_mousepointer
            // 
            this.checkbox_capture_mousepointer.LanguageKey = "settings_capture_mousepointer";
            this.checkbox_capture_mousepointer.Location = new System.Drawing.Point(14, 24);
            this.checkbox_capture_mousepointer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_capture_mousepointer.Name = "checkbox_capture_mousepointer";
            this.checkbox_capture_mousepointer.PropertyName = "CaptureMousepointer";
            this.checkbox_capture_mousepointer.Size = new System.Drawing.Size(492, 30);
            this.checkbox_capture_mousepointer.TabIndex = 17;
            this.checkbox_capture_mousepointer.Text = "Capture mousepointer";
            this.checkbox_capture_mousepointer.UseVisualStyleBackColor = true;
            // 
            // numericUpDownWaitTime
            // 
            this.numericUpDownWaitTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownWaitTime.Location = new System.Drawing.Point(11, 105);
            this.numericUpDownWaitTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownWaitTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownWaitTime.Name = "numericUpDownWaitTime";
            this.numericUpDownWaitTime.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownWaitTime.TabIndex = 24;
            this.numericUpDownWaitTime.ThousandsSeparator = true;
            // 
            // label_waittime
            // 
            this.label_waittime.LanguageKey = "settings_waittime";
            this.label_waittime.Location = new System.Drawing.Point(90, 108);
            this.label_waittime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_waittime.Name = "label_waittime";
            this.label_waittime.Size = new System.Drawing.Size(414, 20);
            this.label_waittime.TabIndex = 25;
            this.label_waittime.Text = "Milliseconds to wait before capture";
            // 
            // tab_printer
            // 
            this.tab_printer.Controls.Add(this.groupBoxColors);
            this.tab_printer.Controls.Add(this.groupBoxPrintLayout);
            this.tab_printer.Controls.Add(this.checkbox_alwaysshowprintoptionsdialog);
            this.tab_printer.LanguageKey = "settings_printer";
            this.tab_printer.Location = new System.Drawing.Point(4, 25);
            this.tab_printer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tab_printer.Name = "tab_printer";
            this.tab_printer.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tab_printer.Size = new System.Drawing.Size(531, 442);
            this.tab_printer.TabIndex = 2;
            this.tab_printer.Text = "Printer";
            this.tab_printer.UseVisualStyleBackColor = true;
            // 
            // groupBoxColors
            // 
            this.groupBoxColors.AutoSize = true;
            this.groupBoxColors.Controls.Add(this.checkboxPrintInverted);
            this.groupBoxColors.Controls.Add(this.radioBtnColorPrint);
            this.groupBoxColors.Controls.Add(this.radioBtnGrayScale);
            this.groupBoxColors.Controls.Add(this.radioBtnMonochrome);
            this.groupBoxColors.LanguageKey = "printoptions_colors";
            this.groupBoxColors.Location = new System.Drawing.Point(8, 204);
            this.groupBoxColors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxColors.Name = "groupBoxColors";
            this.groupBoxColors.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxColors.Size = new System.Drawing.Size(414, 162);
            this.groupBoxColors.TabIndex = 34;
            this.groupBoxColors.TabStop = false;
            this.groupBoxColors.Text = "Color settings";
            // 
            // checkboxPrintInverted
            // 
            this.checkboxPrintInverted.AutoSize = true;
            this.checkboxPrintInverted.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxPrintInverted.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxPrintInverted.LanguageKey = "printoptions_inverted";
            this.checkboxPrintInverted.Location = new System.Drawing.Point(16, 110);
            this.checkboxPrintInverted.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkboxPrintInverted.Name = "checkboxPrintInverted";
            this.checkboxPrintInverted.PropertyName = "OutputPrintInverted";
            this.checkboxPrintInverted.Size = new System.Drawing.Size(184, 21);
            this.checkboxPrintInverted.TabIndex = 28;
            this.checkboxPrintInverted.Text = "Print with inverted colors";
            this.checkboxPrintInverted.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxPrintInverted.UseVisualStyleBackColor = true;
            // 
            // radioBtnColorPrint
            // 
            this.radioBtnColorPrint.AutoSize = true;
            this.radioBtnColorPrint.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnColorPrint.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnColorPrint.LanguageKey = "printoptions_printcolor";
            this.radioBtnColorPrint.Location = new System.Drawing.Point(16, 24);
            this.radioBtnColorPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioBtnColorPrint.Name = "radioBtnColorPrint";
            this.radioBtnColorPrint.PropertyName = "OutputPrintColor";
            this.radioBtnColorPrint.Size = new System.Drawing.Size(118, 21);
            this.radioBtnColorPrint.TabIndex = 29;
            this.radioBtnColorPrint.Text = "Full color print";
            this.radioBtnColorPrint.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnColorPrint.UseVisualStyleBackColor = true;
            // 
            // radioBtnGrayScale
            // 
            this.radioBtnGrayScale.AutoSize = true;
            this.radioBtnGrayScale.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnGrayScale.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnGrayScale.LanguageKey = "printoptions_printgrayscale";
            this.radioBtnGrayScale.Location = new System.Drawing.Point(16, 52);
            this.radioBtnGrayScale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioBtnGrayScale.Name = "radioBtnGrayScale";
            this.radioBtnGrayScale.PropertyName = "OutputPrintGrayscale";
            this.radioBtnGrayScale.Size = new System.Drawing.Size(181, 21);
            this.radioBtnGrayScale.TabIndex = 29;
            this.radioBtnGrayScale.Text = "Force grayscale printing";
            this.radioBtnGrayScale.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnGrayScale.UseVisualStyleBackColor = true;
            // 
            // radioBtnMonochrome
            // 
            this.radioBtnMonochrome.AutoSize = true;
            this.radioBtnMonochrome.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnMonochrome.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnMonochrome.LanguageKey = "printoptions_printmonochrome";
            this.radioBtnMonochrome.Location = new System.Drawing.Point(16, 81);
            this.radioBtnMonochrome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioBtnMonochrome.Name = "radioBtnMonochrome";
            this.radioBtnMonochrome.PropertyName = "OutputPrintMonochrome";
            this.radioBtnMonochrome.Size = new System.Drawing.Size(189, 21);
            this.radioBtnMonochrome.TabIndex = 30;
            this.radioBtnMonochrome.Text = "Force black/white printing";
            this.radioBtnMonochrome.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioBtnMonochrome.UseVisualStyleBackColor = true;
            // 
            // groupBoxPrintLayout
            // 
            this.groupBoxPrintLayout.AutoSize = true;
            this.groupBoxPrintLayout.Controls.Add(this.checkboxDateTime);
            this.groupBoxPrintLayout.Controls.Add(this.checkboxAllowShrink);
            this.groupBoxPrintLayout.Controls.Add(this.checkboxAllowEnlarge);
            this.groupBoxPrintLayout.Controls.Add(this.checkboxAllowRotate);
            this.groupBoxPrintLayout.Controls.Add(this.checkboxAllowCenter);
            this.groupBoxPrintLayout.LanguageKey = "printoptions_layout";
            this.groupBoxPrintLayout.Location = new System.Drawing.Point(8, 8);
            this.groupBoxPrintLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxPrintLayout.Name = "groupBoxPrintLayout";
            this.groupBoxPrintLayout.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxPrintLayout.Size = new System.Drawing.Size(414, 196);
            this.groupBoxPrintLayout.TabIndex = 33;
            this.groupBoxPrintLayout.TabStop = false;
            this.groupBoxPrintLayout.Text = "Page layout settings";
            // 
            // checkboxDateTime
            // 
            this.checkboxDateTime.AutoSize = true;
            this.checkboxDateTime.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxDateTime.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxDateTime.LanguageKey = "printoptions_timestamp";
            this.checkboxDateTime.Location = new System.Drawing.Point(16, 144);
            this.checkboxDateTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkboxDateTime.Name = "checkboxDateTime";
            this.checkboxDateTime.PropertyName = "OutputPrintFooter";
            this.checkboxDateTime.Size = new System.Drawing.Size(244, 21);
            this.checkboxDateTime.TabIndex = 26;
            this.checkboxDateTime.Text = "Print date / time at bottom of page";
            this.checkboxDateTime.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxDateTime.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowShrink
            // 
            this.checkboxAllowShrink.AutoSize = true;
            this.checkboxAllowShrink.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowShrink.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowShrink.LanguageKey = "printoptions_allowshrink";
            this.checkboxAllowShrink.Location = new System.Drawing.Point(16, 29);
            this.checkboxAllowShrink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkboxAllowShrink.Name = "checkboxAllowShrink";
            this.checkboxAllowShrink.PropertyName = "OutputPrintAllowShrink";
            this.checkboxAllowShrink.Size = new System.Drawing.Size(223, 21);
            this.checkboxAllowShrink.TabIndex = 21;
            this.checkboxAllowShrink.Text = "Shrink printout to fit paper size";
            this.checkboxAllowShrink.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowShrink.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowEnlarge
            // 
            this.checkboxAllowEnlarge.AutoSize = true;
            this.checkboxAllowEnlarge.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowEnlarge.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowEnlarge.LanguageKey = "printoptions_allowenlarge";
            this.checkboxAllowEnlarge.Location = new System.Drawing.Point(16, 58);
            this.checkboxAllowEnlarge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkboxAllowEnlarge.Name = "checkboxAllowEnlarge";
            this.checkboxAllowEnlarge.PropertyName = "OutputPrintAllowEnlarge";
            this.checkboxAllowEnlarge.Size = new System.Drawing.Size(232, 21);
            this.checkboxAllowEnlarge.TabIndex = 22;
            this.checkboxAllowEnlarge.Text = "Enlarge printout to fit paper size";
            this.checkboxAllowEnlarge.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowEnlarge.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowRotate
            // 
            this.checkboxAllowRotate.AutoSize = true;
            this.checkboxAllowRotate.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowRotate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowRotate.LanguageKey = "printoptions_allowrotate";
            this.checkboxAllowRotate.Location = new System.Drawing.Point(16, 86);
            this.checkboxAllowRotate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkboxAllowRotate.Name = "checkboxAllowRotate";
            this.checkboxAllowRotate.PropertyName = "OutputPrintAllowRotate";
            this.checkboxAllowRotate.Size = new System.Drawing.Size(247, 21);
            this.checkboxAllowRotate.TabIndex = 23;
            this.checkboxAllowRotate.Text = "Rotate printout to page orientation";
            this.checkboxAllowRotate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowRotate.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowCenter
            // 
            this.checkboxAllowCenter.AutoSize = true;
            this.checkboxAllowCenter.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowCenter.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowCenter.LanguageKey = "printoptions_allowcenter";
            this.checkboxAllowCenter.Location = new System.Drawing.Point(16, 115);
            this.checkboxAllowCenter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkboxAllowCenter.Name = "checkboxAllowCenter";
            this.checkboxAllowCenter.PropertyName = "OutputPrintCenter";
            this.checkboxAllowCenter.Size = new System.Drawing.Size(180, 21);
            this.checkboxAllowCenter.TabIndex = 24;
            this.checkboxAllowCenter.Text = "Center printout on page";
            this.checkboxAllowCenter.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowCenter.UseVisualStyleBackColor = true;
            // 
            // checkbox_alwaysshowprintoptionsdialog
            // 
            this.checkbox_alwaysshowprintoptionsdialog.LanguageKey = "settings_alwaysshowprintoptionsdialog";
            this.checkbox_alwaysshowprintoptionsdialog.Location = new System.Drawing.Point(24, 366);
            this.checkbox_alwaysshowprintoptionsdialog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_alwaysshowprintoptionsdialog.Name = "checkbox_alwaysshowprintoptionsdialog";
            this.checkbox_alwaysshowprintoptionsdialog.PropertyName = "OutputPrintPromptOptions";
            this.checkbox_alwaysshowprintoptionsdialog.Size = new System.Drawing.Size(492, 25);
            this.checkbox_alwaysshowprintoptionsdialog.TabIndex = 17;
            this.checkbox_alwaysshowprintoptionsdialog.Text = "Show print options dialog every time an image is printed";
            this.checkbox_alwaysshowprintoptionsdialog.UseVisualStyleBackColor = true;
            // 
            // tab_plugins
            // 
            this.tab_plugins.Controls.Add(this.groupbox_plugins);
            this.tab_plugins.LanguageKey = "settings_plugins";
            this.tab_plugins.Location = new System.Drawing.Point(4, 25);
            this.tab_plugins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tab_plugins.Name = "tab_plugins";
            this.tab_plugins.Size = new System.Drawing.Size(531, 442);
            this.tab_plugins.TabIndex = 2;
            this.tab_plugins.Text = "Plugins";
            this.tab_plugins.UseVisualStyleBackColor = true;
            // 
            // groupbox_plugins
            // 
            this.groupbox_plugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupbox_plugins.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupbox_plugins.Controls.Add(this.listview_plugins);
            this.groupbox_plugins.Controls.Add(this.button_pluginconfigure);
            this.groupbox_plugins.LanguageKey = "settings_plugins";
            this.groupbox_plugins.Location = new System.Drawing.Point(0, 0);
            this.groupbox_plugins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_plugins.Name = "groupbox_plugins";
            this.groupbox_plugins.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_plugins.Size = new System.Drawing.Size(529, 392);
            this.groupbox_plugins.TabIndex = 0;
            this.groupbox_plugins.TabStop = false;
            this.groupbox_plugins.Text = "Plugins";
            // 
            // listview_plugins
            // 
            this.listview_plugins.Dock = System.Windows.Forms.DockStyle.Top;
            this.listview_plugins.FullRowSelect = true;
            this.listview_plugins.Location = new System.Drawing.Point(4, 19);
            this.listview_plugins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listview_plugins.Name = "listview_plugins";
            this.listview_plugins.Size = new System.Drawing.Size(521, 328);
            this.listview_plugins.TabIndex = 2;
            this.listview_plugins.UseCompatibleStateImageBehavior = false;
            this.listview_plugins.View = System.Windows.Forms.View.Details;
            // 
            // button_pluginconfigure
            // 
            this.button_pluginconfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_pluginconfigure.AutoSize = true;
            this.button_pluginconfigure.Enabled = false;
            this.button_pluginconfigure.LanguageKey = "settings_configureplugin";
            this.button_pluginconfigure.Location = new System.Drawing.Point(8, 351);
            this.button_pluginconfigure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_pluginconfigure.Name = "button_pluginconfigure";
            this.button_pluginconfigure.Size = new System.Drawing.Size(99, 34);
            this.button_pluginconfigure.TabIndex = 1;
            this.button_pluginconfigure.Text = "Configure";
            this.button_pluginconfigure.UseVisualStyleBackColor = true;
            // 
            // tab_expert
            // 
            this.tab_expert.Controls.Add(this.groupbox_expert);
            this.tab_expert.LanguageKey = "expertsettings";
            this.tab_expert.Location = new System.Drawing.Point(4, 25);
            this.tab_expert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tab_expert.Name = "tab_expert";
            this.tab_expert.Size = new System.Drawing.Size(531, 442);
            this.tab_expert.TabIndex = 5;
            this.tab_expert.Text = "Expert";
            this.tab_expert.UseVisualStyleBackColor = true;
            // 
            // groupbox_expert
            // 
            this.groupbox_expert.Controls.Add(this.checkbox_reuseeditor);
            this.groupbox_expert.Controls.Add(this.checkbox_minimizememoryfootprint);
            this.groupbox_expert.Controls.Add(this.checkbox_checkunstableupdates);
            this.groupbox_expert.Controls.Add(this.checkbox_suppresssavedialogatclose);
            this.groupbox_expert.Controls.Add(this.label_counter);
            this.groupbox_expert.Controls.Add(this.textbox_counter);
            this.groupbox_expert.Controls.Add(this.label_footerpattern);
            this.groupbox_expert.Controls.Add(this.textbox_footerpattern);
            this.groupbox_expert.Controls.Add(this.checkbox_thumbnailpreview);
            this.groupbox_expert.Controls.Add(this.checkbox_optimizeforrdp);
            this.groupbox_expert.Controls.Add(this.checkbox_autoreducecolors);
            this.groupbox_expert.Controls.Add(this.label_clipboardformats);
            this.groupbox_expert.Controls.Add(this.checkbox_enableexpert);
            this.groupbox_expert.Controls.Add(this.listview_clipboardformats);
            this.groupbox_expert.LanguageKey = "expertsettings";
            this.groupbox_expert.Location = new System.Drawing.Point(6, 6);
            this.groupbox_expert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_expert.Name = "groupbox_expert";
            this.groupbox_expert.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox_expert.Size = new System.Drawing.Size(515, 389);
            this.groupbox_expert.TabIndex = 17;
            this.groupbox_expert.TabStop = false;
            this.groupbox_expert.Text = "Expert";
            // 
            // checkbox_reuseeditor
            // 
            this.checkbox_reuseeditor.LanguageKey = "expertsettings_reuseeditorifpossible";
            this.checkbox_reuseeditor.Location = new System.Drawing.Point(12, 281);
            this.checkbox_reuseeditor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_reuseeditor.Name = "checkbox_reuseeditor";
            this.checkbox_reuseeditor.PropertyName = "ReuseEditor";
            this.checkbox_reuseeditor.SectionName = "Editor";
            this.checkbox_reuseeditor.Size = new System.Drawing.Size(492, 30);
            this.checkbox_reuseeditor.TabIndex = 31;
            this.checkbox_reuseeditor.Text = "Reuse editor if possible";
            this.checkbox_reuseeditor.UseVisualStyleBackColor = true;
            // 
            // checkbox_minimizememoryfootprint
            // 
            this.checkbox_minimizememoryfootprint.LanguageKey = "expertsettings_minimizememoryfootprint";
            this.checkbox_minimizememoryfootprint.Location = new System.Drawing.Point(12, 258);
            this.checkbox_minimizememoryfootprint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_minimizememoryfootprint.Name = "checkbox_minimizememoryfootprint";
            this.checkbox_minimizememoryfootprint.PropertyName = "MinimizeWorkingSetSize";
            this.checkbox_minimizememoryfootprint.Size = new System.Drawing.Size(492, 30);
            this.checkbox_minimizememoryfootprint.TabIndex = 30;
            this.checkbox_minimizememoryfootprint.Text = "Minimize memory footprint, but with a performance penalty (not advised).";
            this.checkbox_minimizememoryfootprint.UseVisualStyleBackColor = true;
            // 
            // checkbox_checkunstableupdates
            // 
            this.checkbox_checkunstableupdates.LanguageKey = "expertsettings_checkunstableupdates";
            this.checkbox_checkunstableupdates.Location = new System.Drawing.Point(12, 234);
            this.checkbox_checkunstableupdates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_checkunstableupdates.Name = "checkbox_checkunstableupdates";
            this.checkbox_checkunstableupdates.PropertyName = "CheckForUnstable";
            this.checkbox_checkunstableupdates.Size = new System.Drawing.Size(492, 30);
            this.checkbox_checkunstableupdates.TabIndex = 29;
            this.checkbox_checkunstableupdates.Text = "Check for unstable updates";
            this.checkbox_checkunstableupdates.UseVisualStyleBackColor = true;
            // 
            // checkbox_suppresssavedialogatclose
            // 
            this.checkbox_suppresssavedialogatclose.LanguageKey = "expertsettings_suppresssavedialogatclose";
            this.checkbox_suppresssavedialogatclose.Location = new System.Drawing.Point(12, 210);
            this.checkbox_suppresssavedialogatclose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_suppresssavedialogatclose.Name = "checkbox_suppresssavedialogatclose";
            this.checkbox_suppresssavedialogatclose.PropertyName = "SuppressSaveDialogAtClose";
            this.checkbox_suppresssavedialogatclose.SectionName = "Editor";
            this.checkbox_suppresssavedialogatclose.Size = new System.Drawing.Size(492, 30);
            this.checkbox_suppresssavedialogatclose.TabIndex = 28;
            this.checkbox_suppresssavedialogatclose.Text = "Suppress the save dialog when closing the editor";
            this.checkbox_suppresssavedialogatclose.UseVisualStyleBackColor = true;
            // 
            // label_counter
            // 
            this.label_counter.AutoSize = true;
            this.label_counter.LanguageKey = "expertsettings_counter";
            this.label_counter.Location = new System.Drawing.Point(9, 356);
            this.label_counter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_counter.Name = "label_counter";
            this.label_counter.Size = new System.Drawing.Size(328, 17);
            this.label_counter.TabIndex = 27;
            this.label_counter.Text = "The number for the ${NUM} in the filename pattern";
            // 
            // textbox_counter
            // 
            this.textbox_counter.Location = new System.Drawing.Point(324, 352);
            this.textbox_counter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textbox_counter.Name = "textbox_counter";
            this.textbox_counter.PropertyName = "OutputFileIncrementingNumber";
            this.textbox_counter.Size = new System.Drawing.Size(175, 22);
            this.textbox_counter.TabIndex = 26;
            // 
            // label_footerpattern
            // 
            this.label_footerpattern.AutoSize = true;
            this.label_footerpattern.LanguageKey = "expertsettings_footerpattern";
            this.label_footerpattern.Location = new System.Drawing.Point(9, 324);
            this.label_footerpattern.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_footerpattern.Name = "label_footerpattern";
            this.label_footerpattern.Size = new System.Drawing.Size(140, 17);
            this.label_footerpattern.TabIndex = 25;
            this.label_footerpattern.Text = "Printer footer pattern";
            // 
            // textbox_footerpattern
            // 
            this.textbox_footerpattern.Location = new System.Drawing.Point(172, 320);
            this.textbox_footerpattern.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textbox_footerpattern.Name = "textbox_footerpattern";
            this.textbox_footerpattern.PropertyName = "OutputPrintFooterPattern";
            this.textbox_footerpattern.Size = new System.Drawing.Size(326, 22);
            this.textbox_footerpattern.TabIndex = 24;
            // 
            // checkbox_thumbnailpreview
            // 
            this.checkbox_thumbnailpreview.LanguageKey = "expertsettings_thumbnailpreview";
            this.checkbox_thumbnailpreview.Location = new System.Drawing.Point(12, 186);
            this.checkbox_thumbnailpreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_thumbnailpreview.Name = "checkbox_thumbnailpreview";
            this.checkbox_thumbnailpreview.PropertyName = "ThumnailPreview";
            this.checkbox_thumbnailpreview.Size = new System.Drawing.Size(492, 30);
            this.checkbox_thumbnailpreview.TabIndex = 23;
            this.checkbox_thumbnailpreview.Text = "Show window thumbnails in context menu (for Vista and windows 7)";
            this.checkbox_thumbnailpreview.UseVisualStyleBackColor = true;
            // 
            // checkbox_optimizeforrdp
            // 
            this.checkbox_optimizeforrdp.LanguageKey = "expertsettings_optimizeforrdp";
            this.checkbox_optimizeforrdp.Location = new System.Drawing.Point(12, 162);
            this.checkbox_optimizeforrdp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_optimizeforrdp.Name = "checkbox_optimizeforrdp";
            this.checkbox_optimizeforrdp.PropertyName = "OptimizeForRDP";
            this.checkbox_optimizeforrdp.Size = new System.Drawing.Size(492, 30);
            this.checkbox_optimizeforrdp.TabIndex = 22;
            this.checkbox_optimizeforrdp.Text = "Make some optimizations for usage with remote desktop";
            this.checkbox_optimizeforrdp.UseVisualStyleBackColor = true;
            // 
            // checkbox_autoreducecolors
            // 
            this.checkbox_autoreducecolors.LanguageKey = "expertsettings_autoreducecolors";
            this.checkbox_autoreducecolors.Location = new System.Drawing.Point(12, 139);
            this.checkbox_autoreducecolors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_autoreducecolors.Name = "checkbox_autoreducecolors";
            this.checkbox_autoreducecolors.PropertyName = "OutputFileAutoReduceColors";
            this.checkbox_autoreducecolors.Size = new System.Drawing.Size(510, 30);
            this.checkbox_autoreducecolors.TabIndex = 21;
            this.checkbox_autoreducecolors.Text = "Create an 8-bit image if the colors are less than 256 while having a > 8 bits ima" +
    "ge";
            this.checkbox_autoreducecolors.UseVisualStyleBackColor = true;
            // 
            // label_clipboardformats
            // 
            this.label_clipboardformats.AutoSize = true;
            this.label_clipboardformats.LanguageKey = "expertsettings_clipboardformats";
            this.label_clipboardformats.Location = new System.Drawing.Point(9, 49);
            this.label_clipboardformats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_clipboardformats.Name = "label_clipboardformats";
            this.label_clipboardformats.Size = new System.Drawing.Size(119, 17);
            this.label_clipboardformats.TabIndex = 20;
            this.label_clipboardformats.Text = "Clipboard formats";
            // 
            // checkbox_enableexpert
            // 
            this.checkbox_enableexpert.LanguageKey = "expertsettings_enableexpert";
            this.checkbox_enableexpert.Location = new System.Drawing.Point(8, 18);
            this.checkbox_enableexpert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkbox_enableexpert.Name = "checkbox_enableexpert";
            this.checkbox_enableexpert.Size = new System.Drawing.Size(492, 30);
            this.checkbox_enableexpert.TabIndex = 19;
            this.checkbox_enableexpert.Text = "I know what I am doing!";
            this.checkbox_enableexpert.UseVisualStyleBackColor = true;
            this.checkbox_enableexpert.CheckedChanged += new System.EventHandler(this.checkbox_enableexpert_CheckedChanged);
            // 
            // listview_clipboardformats
            // 
            this.listview_clipboardformats.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listview_clipboardformats.AutoArrange = false;
            this.listview_clipboardformats.CheckBoxes = true;
            this.listview_clipboardformats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listview_clipboardformats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listview_clipboardformats.LabelWrap = false;
            this.listview_clipboardformats.Location = new System.Drawing.Point(212, 48);
            this.listview_clipboardformats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listview_clipboardformats.Name = "listview_clipboardformats";
            this.listview_clipboardformats.ShowGroups = false;
            this.listview_clipboardformats.Size = new System.Drawing.Size(286, 89);
            this.listview_clipboardformats.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listview_clipboardformats.TabIndex = 0;
            this.listview_clipboardformats.UseCompatibleStateImageBehavior = false;
            this.listview_clipboardformats.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Destination";
            this.columnHeader1.Width = 225;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(564, 539);
            this.Controls.Add(this.tabcontrol);
            this.Controls.Add(this.settings_confirm);
            this.Controls.Add(this.settings_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "settings_title";
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.tabcontrol.ResumeLayout(false);
            this.tab_capture.ResumeLayout(false);
            this.groupbox_editor.ResumeLayout(false);
            this.groupbox_iecapture.ResumeLayout(false);
            this.groupbox_windowscapture.ResumeLayout(false);
            this.groupbox_windowscapture.PerformLayout();
            this.groupbox_capture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).EndInit();
            this.tab_printer.ResumeLayout(false);
            this.tab_printer.PerformLayout();
            this.groupBoxColors.ResumeLayout(false);
            this.groupBoxColors.PerformLayout();
            this.groupBoxPrintLayout.ResumeLayout(false);
            this.groupBoxPrintLayout.PerformLayout();
            this.tab_plugins.ResumeLayout(false);
            this.groupbox_plugins.ResumeLayout(false);
            this.groupbox_plugins.PerformLayout();
            this.tab_expert.ResumeLayout(false);
            this.groupbox_expert.ResumeLayout(false);
            this.groupbox_expert.PerformLayout();
            this.ResumeLayout(false);

		}
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_notifications;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_minimizememoryfootprint;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_editor;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_editor_match_capture_size;
		private Greenshot.Controls.ColorButton colorButton_window_background;
		private GreenshotPlugin.Controls.GreenshotLabel label_window_capture_mode;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_ie_capture;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_capture;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_windowscapture;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_iecapture;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_capture;
		private System.Windows.Forms.ComboBox combobox_window_capture_mode;
		private System.Windows.Forms.NumericUpDown numericUpDownWaitTime;
		private GreenshotPlugin.Controls.GreenshotLabel label_waittime;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_capture_windows_interactive;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_capture_mousepointer;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_printer;
		private System.Windows.Forms.ListView listview_plugins;
		private GreenshotPlugin.Controls.GreenshotButton button_pluginconfigure;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_plugins;
        private GreenshotPlugin.Controls.GreenshotTabPage tab_plugins;
        private System.Windows.Forms.TabControl tabcontrol;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_playsound;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private GreenshotPlugin.Controls.GreenshotButton settings_cancel;
        private GreenshotPlugin.Controls.GreenshotButton settings_confirm;
		private GreenshotPlugin.Controls.GreenshotTabPage tab_expert;
		private GreenshotPlugin.Controls.GreenshotGroupBox groupbox_expert;
		private GreenshotPlugin.Controls.GreenshotLabel label_clipboardformats;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_enableexpert;
		private System.Windows.Forms.ListView listview_clipboardformats;
        private System.Windows.Forms.ColumnHeader columnHeader1;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_autoreducecolors;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_optimizeforrdp;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_thumbnailpreview;
		private GreenshotPlugin.Controls.GreenshotLabel label_footerpattern;
		private GreenshotPlugin.Controls.GreenshotTextBox textbox_footerpattern;
		private GreenshotPlugin.Controls.GreenshotLabel label_counter;
        private GreenshotPlugin.Controls.GreenshotTextBox textbox_counter;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_suppresssavedialogatclose;
		private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_checkunstableupdates;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_reuseeditor;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_alwaysshowprintoptionsdialog;
        private GreenshotPlugin.Controls.GreenshotGroupBox groupBoxColors;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkboxPrintInverted;
        private GreenshotPlugin.Controls.GreenshotRadioButton radioBtnColorPrint;
        private GreenshotPlugin.Controls.GreenshotRadioButton radioBtnGrayScale;
        private GreenshotPlugin.Controls.GreenshotRadioButton radioBtnMonochrome;
        private GreenshotPlugin.Controls.GreenshotGroupBox groupBoxPrintLayout;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkboxDateTime;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowShrink;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowEnlarge;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowRotate;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkboxAllowCenter;
	}
}
