using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class GetSingleSupervisionMainOutput : Supervision
    {
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
        /// <summary>
        /// 照片路径
        /// </summary>
        public List<string> PhotoPath { get; set; }
    }
}
