using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotSaver
{
    public partial class Settings : Form
    {
        private CustomApplicationContext app;

        public Settings(CustomApplicationContext app)
        {
            this.app = app;

            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            textBoxFolder.Text = Properties.Settings.Default.ScreenshotFolder;
            CreateFileTypes();

            checkBoxNotification.Checked = Properties.Settings.Default.Notification;
            checkBoxStartup.Checked = Properties.Settings.Default.Startup;


            labelVersion.Text = "Current Version: " + Application.ProductVersion;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.ScreenshotFolder = textBoxFolder.Text;
            Properties.Settings.Default.ScreenshotFileType = (string)comboBoxFileType.SelectedItem;

            Properties.Settings.Default.Notification = checkBoxNotification.Checked;
            Properties.Settings.Default.Startup = checkBoxStartup.Checked;

            Properties.Settings.Default.Save();
        }

        private void CreateFileTypes()
        {
            comboBoxFileType.Items.Clear();
            
            comboBoxFileType.Items.Add(ImageFormat.Png.ToString().ToLower());
            comboBoxFileType.Items.Add(ImageFormat.Jpeg.ToString().ToLower());
            comboBoxFileType.Items.Add(ImageFormat.Gif.ToString().ToLower());
            comboBoxFileType.Items.Add(ImageFormat.Bmp.ToString().ToLower());

            comboBoxFileType.SelectedText = Properties.Settings.Default.ScreenshotFileType;
        }

        private void OpenFolderDialog()
        {
            folderBrowserDialog.Description = "Select the screenshot folder";
            folderBrowserDialog.SelectedPath = Properties.Settings.Default.ScreenshotFolder;
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                textBoxFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }


        

        private void buttonFolderBrowse_Click(object sender, EventArgs e)
        {
            OpenFolderDialog();
        }

        private void checkBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            app.ChangeStartup(checkBoxStartup.Checked);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
