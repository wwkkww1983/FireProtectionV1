using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitInfoOutput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 防火单位区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 防火单位地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 防火单位类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContractPhone { get; set; }
        /// <summary>
        /// 维保单位Id
        /// </summary>
        public int SafeUnitId { get; set; }
        /// <summary>
        /// 维保单位
        /// </summary>
        public string SafeUnit { get; set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsAttention { get; set; }
    }
}
