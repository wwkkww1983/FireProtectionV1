using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddNewDetectorFaultInput
    {
        /// <summary>
        /// 火警联网设施编号
        /// </summary>
        public string FireAlarmDeviceSn { get; set; }
        /// <summary>
        /// 联网部件编号
        /// </summary>
        public string FireAlarmDetectorSn { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        public string FaultRemark { get; set; }
    }
}
