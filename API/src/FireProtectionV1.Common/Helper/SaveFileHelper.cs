using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Common.Helper
{
    public class SaveFileHelper
    {
        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="path"></param>
        /// <returns>new filename</returns>
        public static async Task<string> SaveFile(IFormFile formFile, string path)
        {
            if (formFile != null)
            {
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString("N").Substring(0, 16) + Path.GetExtension(formFile.FileName);
                using (var stream = System.IO.File.Create(path + filename))
                {
                    await formFile.CopyToAsync(stream);
                }
                return filename;
            }
            return "";
        }
    }
}
