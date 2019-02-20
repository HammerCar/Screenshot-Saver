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
            sc.CaptureScreenToFile(path, ImageFormat.Png);

            return path;
        }

        private string GetImagePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Screenshots", 
                "screenshot-" + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToLongTimeString() + ".png");
        }

        private void CheckFolder()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Screenshots");
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
        }
    }
}
