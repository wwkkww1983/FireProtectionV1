using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class ShortMessageLog : EntityBase
    {
        /// <summary>
        /// 报警类型
        /// </summary>
        public AlarmType AlarmType { get; set; }
        /// <summary>
        /// 接收短信手机
        /// </summary>
        public string Phones { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 发送结果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
}
