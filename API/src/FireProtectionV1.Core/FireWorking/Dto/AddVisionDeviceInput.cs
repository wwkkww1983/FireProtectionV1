using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddVisionDeviceInput
    {
        /// <summary>
        /// 消防分析仪编号
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 最大监控路数
        /// </summary>
        public int MonitorNum { get; set; }
        /// <summary>
        /// 短信接收号码
        /// </summary>
        public string SMSPhones { get; set; }
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
    }
    public class UpdateVisionDeviceInput
    {
        public int Id { get; set; }
        /// <summary>
        /// 消防分析仪编号
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 最大监控路数
        /// </summary>
        public int MonitorNum { get; set; }
        /// <summary>
        /// 短信接收号码
        /// </summary>
        public string SMSPhones { get; set; }
    }
    public class VisionDeviceItemDto
    {
        /// <summary>
        /// 消防分析仪设备Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 消防分析仪编号
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 最大监控路数
        /// </summary>
        public int MonitorNum { get; set; }
    }
    public class VisionDetectorItemDto
    {
        /// <summary>
        /// 摄像头对应的通道Id
        /// </summary>
        public int VisionDetectorId { get; set; }
        /// <summary>
        /// 摄像头对应的通道编号
        /// </summary>
        public int Sn { get; set; }
        /// <summary>
        /// 摄像头监控地址
        /// </summary>
        public string Location { get; set; }
    }
}
