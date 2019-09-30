using System;
using System.Collections;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace FireProtectionV1.Common.Helper
{
    public class ImageHelper
    {
        /// <summary>
        /// 根据文件类型和文件名返回新路径
        /// </summary>
        /// <param name="type">文件类型</param>
        /// <param name="fileName">文件名/回传新的相对路径</param>
        /// <returns>全新文件绝对路径</returns>
        //public static string CreatePath(FileType type, ref string fileName)
        //{
        //    string path1 = $"{RootPath}{Enum.GetName(typeof(FileType), type)}/";
        //    var path = $"{AppContext.BaseDirectory}{path1}";
        //    //检查上传的物理路径是否存在，不存在则创建
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    string name = $"{DateTime.Now:yyyyMMddHHmmssff}.{GetFileExt(fileName)}";
        //    fileName = $"{path1}{name}";
        //    return $"{path}{name}";
        //}

        /// <summary>
        /// 取小写文件名后缀
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns>返回小写后缀，不带“.”</returns>
        public static string GetFileExt(string name)
        {
            return name.Split(".").Last().ToLower();
        }

        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="fileExt">文件扩展名，不含“.”</param>
        public static bool IsImage(string fileExt)
        {
            ArrayList al = new ArrayList { "bmp", "jpeg", "jpg", "gif", "png", "ico" };
            return al.Contains(fileExt);
        }

        /// <summary>
        /// 检查是否允许文件
        /// </summary>
        /// <param name="fileExt">文件后缀</param>
        /// <param name="allowExt">允许文件数组</param>
        public static bool CheckFileExt(string fileExt, string[] allowExt)
        {
            return allowExt.Any(t => t == fileExt);
        }


        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="original">图片对象</param>
        /// <param name="newFileName">新图路径</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void ThumbImg(Image original, string newFileName, int maxWidth, int maxHeight)
        {
            Size newSize = ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
            using (Image displayImage = new Bitmap(original, newSize))
            {
                try
                {
                    displayImage.Save(newFileName, original.RawFormat);
                }
                finally
                {
                    original.Dispose();
                }
            }
        }
        /// <summary>
        /// 制作缩略图base64
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static string ThumbImg(string fileName)
        {
            try
            {
                Bitmap img = new Bitmap(fileName);
                var width = 200 * (double)img.Size.Width / img.Size.Height;

                Size newSize = ResizeImage(img.Width, img.Height, Convert.ToInt32(width), 200);
                using (Image displayImage = new Bitmap(img, newSize))
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        displayImage.Save(ms, img.RawFormat);
                        byte[] arr = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(arr, 0, (int)ms.Length);
                        ms.Close();
                        return Convert.ToBase64String(arr);
                    }
                    finally
                    {
                        img.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="newFileName">新图路径</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void ThumbImg(string fileName, string newFileName, int maxWidth, int maxHeight)
        {
            byte[] imageBytes = File.ReadAllBytes(fileName);
            Image img = Image.FromStream(new MemoryStream(imageBytes));
            ThumbImg(img, newFileName, maxWidth, maxHeight);
        }


        /// <summary>
        /// 计算新尺寸
        /// </summary>
        /// <param name="width">原始宽度</param>
        /// <param name="height">原始高度</param>
        /// <param name="maxWidth">最大新宽度</param>
        /// <param name="maxHeight">最大新高度</param>
        /// <returns></returns>
        private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            if (maxWidth <= 0)
                maxWidth = width;
            if (maxHeight <= 0)
                maxHeight = height;
            decimal MAX_WIDTH = maxWidth;
            decimal MAX_HEIGHT = maxHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;
            decimal originalWidth = width;
            decimal originalHeight = height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }
            return new Size(newWidth, newHeight);
        }


        /// <summary>
        /// 得到图片格式
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string name)
        {
            string ext = GetFileExt(name);
            switch (ext)
            {
                case "ico":
                    return ImageFormat.Icon;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }
    }
}
