﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetFireUnitListOutput
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
        /// 邀请码（自动生成）
        /// </summary>
        public string InvitationCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get; set; }
    }
}
