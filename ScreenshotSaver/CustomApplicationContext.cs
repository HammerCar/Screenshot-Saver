using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotSaver
{
    public class CustomApplicationContext : ApplicationContext
    {
        private static readonly string IconFileName = "icon.ico";
        private static readonly string DefaultTooltip = "Click to take a screenshot";
        private readonly ScreenshotManager screenshotManager;

        /// <summary>
		/// This class should be created and passed into Application.Run( ... )
		/// </summary>
		public CustomApplicationContext()
        {
            InitializeContext();
            screenshotManager = new ScreenshotManager();
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
            notifyIcon.ContextMenuStrip.Items.Clear();

            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Take a Screenshot", null, screenshotItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Settings", null, settingsItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Help/About", null, showHelpItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Exit", null, exitItem_Click));
        }

        private Form settingsForm;
        private Form helpForm;

        private string lastScreenshotPath;

        private void TakeScreenShot()
        {
            string ssPath = screenshotManager.TakeScreenshot();
            lastScreenshotPath = ssPath;

            notifyIcon.ShowBalloonTip(3000, "Screenshot Saved", "Saved to " + ssPath, ToolTipIcon.Info);
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select," + lastScreenshotPath);
        }

        private void OpenSettings()
        {
            MessageBox.Show("settings");
        }

        private void OpenHelp()
        {
            MessageBox.Show("help");
        }



        # region Event Handlers

        private void notifyIcon_DoubleClick(object sender, EventArgs e) { OpenSettings(); }
        
        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TakeScreenShot();
            }
        }

        private void screenshotItem_Click(object sender, EventArgs e)
        {
            TakeScreenShot();
        }

        private void settingsItem_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void showHelpItem_Click(object sender, EventArgs e)
        {
            OpenHelp();
        }

        # endregion Event Handlers

        # region generic code framework

        private System.ComponentModel.IContainer components;	// a list of components to dispose when the context is disposed
        private NotifyIcon notifyIcon;				            // the icon that sits in the system tray

        private void InitializeContext()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(IconFileName),
                Text = DefaultTooltip,
                Visible = true
            };
            notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.MouseUp += notifyIcon_MouseUp;

            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
        }

        /// <summary>
		/// When the application context is disposed, dispose things like the notify icon.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) { components.Dispose(); }
        }

        /// <summary>
        /// When the exit menu item is clicked, make a call to terminate the ApplicationContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitItem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        /// <summary>
        /// If we are presently showing a form, clean it up.
        /// </summary>
        protected override void ExitThreadCore()
        {
            // before we exit, let forms clean themselves up.
            if (settingsForm != null) { settingsForm.Close(); }
            if (helpForm != null) { helpForm.Close(); }

            notifyIcon.Visible = false; // should remove lingering tray icon
            base.ExitThreadCore();
        }

        # endregion generic code framework
    }
}
