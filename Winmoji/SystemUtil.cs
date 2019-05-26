using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
//调用WINDOWS API函数时要用到
using Microsoft.Win32;

namespace Winmoji
{
    public static class SystemUtil
    {
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
        );
        public static bool setScreenWallPaper(Image image)
        {
            CheckWallPaperFolder();
            string filename = GetWallPaperFolderPath()+"\\"+DateTime.Now.ToString("yyyyMMddHHmmss")+".jpg";

            image.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
            return SystemParametersInfo(20, 1, filename, 1) == 1 ? true : false;
        }

        private static void CheckWallPaperFolder()
        {
            String folder = GetWallPaperFolderPath();
            ;
            if (false == System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
        }

        private static String GetWallPaperFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/Winmoji";
            ;
        }

        public static bool SaveImage(Image image)
        {
            string pictureName = DateTime.Now.ToString("yyyyMMddHHmmss");
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "请选择保存路径";
            fileDialog.DefaultExt = "jpg";
            fileDialog.RestoreDirectory = true;
            fileDialog.Filter = "jpg图片|*.JPG|gif图片|*.GIF|png图片|*.PNG|jpeg图片|*.JPEG"; //设置要选择的文件的类型
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                using (MemoryStream mem = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(image);
                    //保存到磁盘文件
                    bmp.Save(file, image.RawFormat);
                    bmp.Dispose();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
