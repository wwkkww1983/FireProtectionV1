using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    /// <summary>
    /// 独立式报警设备
    /// </summary>
    public class IndependentDetector : EntityBase
    {
        /// <summary>
        /// sn号
        /// </summary>
        public string DetectorSn { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 具体安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 当前电量
        /// </summary>
        public decimal PowerNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public IndependentDetectorState State { get; set; }
        /// <summary>
        /// 启用报警发送短信
        /// </summary>
        public bool EnableAlarmSMS { get; set; }
        /// <summary>
        /// 启用故障发送短信
        /// </summary>
        public bool EnableFaultSMS { get; set; }
        /// <summary>
        /// 短信接收号码，多个以英文逗号分隔
        /// </summary>
        public string SMSPhones { get; set; }
        /// <summary>
        /// 最后一次故障ID
        /// </summary>
        public int LastFaultId { get; set; }
    }
}
