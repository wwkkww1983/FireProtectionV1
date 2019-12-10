using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto.FireDevice
{
    public class FireOrtherDeviceItemDto {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 所在建筑
        /// </summary>
        public string FireUnitArchitectureName { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public string FireUnitArchitectureFloorName { get; set; }
        /// <summary>
        /// 设备地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 是否联网
        /// </summary>
        public string IsNet { get; set; }
        /// <summary>
        /// 到期时间(yyyy-MM-dd)
        /// </summary>
        public string ExpireTime { get; set; }
    }
    public class GetFireOrtherDeviceOutput: UpdateFireOrtherDeviceDto
    {
        /// <summary>
        /// 所在建筑
        /// </summary>
        public string FireUnitArchitectureName { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public string FireUnitArchitectureFloorName { get; set; }
    }
    public class UpdateFireOrtherDeviceDto: FireOrtherDeviceDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
    }
    public class FireOrtherDeviceDto
    {
        /// <summary>
        /// 防火单位ID
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 所属系统
        /// </summary>
        public int FireSystemId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceSn { get; set; }
        /// <summary>
        /// 所在建筑
        /// </summary>
        public int FireUnitArchitectureId { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int FireUnitArchitectureFloorId { get; set; }
        /// <summary>
        /// 设备地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 启用时间(yyyy-MM-dd)
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 到期时间(yyyy-MM-dd)
        /// </summary>
        public string ExpireTime { get; set; }
    }
    public class FireOtherDeviceImportDto
    {
        /// <summary>
        /// 防火单位ID
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// excel文件流
        /// </summary>
        public IFormFile file { get; set; }
    }
}
