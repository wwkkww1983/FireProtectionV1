using Abp.Application.Services.Dto;
using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetEquipmentNoListOutput
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 页面数据
        /// </summary>
        public List<GetEquipmentNoList> EquipmentNoList { get; set; }
    }

    public class GetEquipmentNoList
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 设施编码
        /// </summary>
        public string EquiNo { get; set; }
        /// <summary>
        /// 具体地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 消防系统
        /// </summary>
        public string FireSystemName { get; set; }
    }
}
