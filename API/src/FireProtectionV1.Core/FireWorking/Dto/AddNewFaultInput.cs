using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddNewFaultInput: DeviceBaseInput
    {
        /// <summary>
        /// 部件国标类型
        /// </summary>
        public byte DetectorGBType { get; set; }
        /// <summary>
        /// 网关设备标识
        /// </summary>
        public string GatewayIdentify { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        [MaxLength(StringType.Long)]
        public string FaultRemark { get; set; }
    }
}
