using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class UpdateEquipmentNoInfoInput
    {
        /// <summary>
        /// 操作
        /// </summary>
        public opreationType Opreation { get; set; }
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
        /// 消防系统ID
        /// </summary>
        public int FireSystemId { get; set; }
    }
    public enum opreationType
    {
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        delete = 0,
        /// <summary>
        /// 更新
        /// </summary>
        [Description("更新")]
        update = 1,
    }
}
