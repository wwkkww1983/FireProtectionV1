using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Model
{
    /// <summary>
    /// 取证照片
    /// </summary>
    public class SupervisionPhotos : EntityBase
    {
        /// <summary>
        /// 监督执法记录Id
        /// </summary>
        public int SupervisionId { get; set; }
        /// <summary>
        /// 照片路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 照片名称
        /// </summary>
        public string Name { get; set; }
    }
}
