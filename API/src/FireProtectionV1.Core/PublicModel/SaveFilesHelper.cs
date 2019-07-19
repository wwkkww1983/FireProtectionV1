using Abp.Domain.Repositories;
using FireProtectionV1.FireWorking.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FireProtectionV1.Common.Helper
{
    public class SaveFilesHelper
    {
        IRepository<PhotosPathSave> _photosPathSave;
        public SaveFilesHelper(IRepository<PhotosPathSave> photosPathSaveRep)
        {
            _photosPathSave = photosPathSaveRep;
        }


        public void SavePhotosPath(string tablename, int dataId, string fileName)
        {
            string photopath = "/Src/Photos/" + tablename + "/" + fileName;
            PhotosPathSave photo = new PhotosPathSave()
            {
                TableName = tablename,
                DataId = dataId,
                PhotoPath = photopath
            };
            _photosPathSave.InsertAsync(photo);
        }

        public async Task<string> SaveFiles(IFormFile file, string path)
        {
            if (!Directory.Exists(path))//判断是否存在
            {
                Directory.CreateDirectory(path);//创建新路径
            }
            string fileName = GetTimeStamp() + Path.GetExtension(file.FileName);
            using (var stream = System.IO.File.Create(path + fileName))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        //获取当前时间段额时间戳
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
    }
}
