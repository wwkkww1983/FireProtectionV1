using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class FaultDetectorOutput: FireAlarmDetectorDto
    {
        /// <summary>
        /// 故障描述
        /// </summary>
        public string FaultContent { get; set; }
        /// <summary>
        /// 故障时间
        /// </summary>
        public string FaultTime { get; set; }
    }
    public class AddDeviceDetectorOutput:SuccessOutput
    {
        /// <summary>
        /// 部件ID
        /// </summary>
        public int DetectorId { get; set; }
    }
    public class UpdateDetectorDto:AddDetectorDto
    {
        /// <summary>
        /// 部件ID
        /// </summary>
        public int DetectorId { get; set; }
    }
    public class AddDetectorDto
    {
        /// <summary>
        /// 设备SN
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 部件地址
        /// </summary>
        public string Identify { get; set; }
        /// <summary>
        /// 部件类型名称
        /// </summary>
        public string DetectorTypeName { get; set; }
        /// <summary>
        /// 部件所在建筑ID
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 部件所在楼层ID
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 部件安装位置
        /// </summary>
        public string Location { get; set; }
    }
    public class FireAlarmDetectorDto
    {
        /// <summary>
        /// 部件ID
        /// </summary>
        public int DetectorId { get; set; }
        /// <summary>
        /// 部件地址
        /// </summary>
        public string Identify { get; set; }
        /// <summary>
        /// 部件类型名字
        /// </summary>
        public string DetectorTypeName { get; set; }
        /// <summary>
        /// 部件所在建筑ID
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 部件所在建筑名字
        /// </summary>
        public string FireUnitArchitectureName { get; set; }
        /// <summary>
        /// 部件所在楼层ID
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 部件所在楼层名字
        /// </summary>
        public string FireUnitArchitectureFloorName { get; set; }
        /// <summary>
        /// 部件安装位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 部件状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    public class FireAlarmDetectorImportDto
    {
        /// <summary>
        /// 火警联网设施编号
        /// </summary>
        public string FireAlarmDeviceSn { get; set; }
        /// <summary>
        /// excel文件流
        /// </summary>
        public IFormFile file { get; set; }
    }
}
