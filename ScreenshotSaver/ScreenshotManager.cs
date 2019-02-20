using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace ScreenshotSaver
{
    class ScreenshotManager
    {
        public string TakeScreenshot()
        {
            ScreenCapture sc = new ScreenCapture();

            CheckFolder();

            string path = GetImagePath();
            sc.CaptureScreenToFile(path, StringToImageFormat(Properties.Settings.Default.ScreenshotFileType));

            return path;
        }

        private string GetImageName()
        {
            return "screenshot-" + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToLongTimeString() + "." + Properties.Settings.Default.ScreenshotFileType;
        }

        private string GetImagePath()
        {
            return Path.Combine(Properties.Settings.Default.ScreenshotFolder, GetImageName());
        }

        private void CheckFolder()
        {
            string path = Properties.Settings.Default.ScreenshotFolder;
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
        }

        private ImageFormat StringToImageFormat(string format)
        {
            if (format == "jpeg")
                return ImageFormat.Jpeg;
            else if (format == "gif")
                return ImageFormat.Gif;
            else if (format == "bmp")
                return ImageFormat.Bmp;
            else
                return ImageFormat.Png;
        }
    }
}
