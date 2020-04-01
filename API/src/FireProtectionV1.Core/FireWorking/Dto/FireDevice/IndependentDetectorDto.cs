using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class AddIndependentDetectorInput
    {
        /// <summary>
        /// sn号
        /// </summary>
        [Required]
        public string DetectorSn { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 具体安装位置
        /// </summary>
        [Required]
        public string Location { get; set; }
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
    }

    public class UpdateIndependentDetectorInput : AddIndependentDetectorInput
    {
        public int DetectorId { get; set; }
    }

    public class GetIndependentDetectorOutput : UpdateIndependentDetectorInput
    { }

    public class RenewIndependentDetectorInput
    {
        /// <summary>
        /// sn号
        /// </summary>
        public string DetectorSn { get; set; }
        /// <summary>
        /// 当前电量
        /// </summary>
        public decimal PowerNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public IndependentDetectorState State { get; set; }
    }

    public class GetIndependentDetectorListOutput
    {
        public int DetectorId { get; set; }
        /// <summary>
        /// sn号
        /// </summary>
        public string DetectorSn { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 具体安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 当前电量
        /// </summary>
        public string PowerNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public IndependentDetectorState State { get; set; }
    }
}
