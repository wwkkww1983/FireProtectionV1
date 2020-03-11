using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    public class GetTrueFireAlarmListOutput
    {
        /// <summary>
        /// 火警数据Id
        /// </summary>
        public int FireAlarmId { get; set; }
        /// <summary>
        /// 核警时间
        /// </summary>
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// 火警发生时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireunitName { get; set; }
        /// <summary>
        /// 防火单位地址
        /// </summary>
        public string FireunitAddress { get; set; }
        /// <summary>
        /// 防火单位联系人
        /// </summary>
        public string FireunitContractUser { get; set; }
        /// <summary>
        /// 报警部件地址
        /// </summary>
        public string FireDetectorLocation { get; set; }
        /// <summary>
        /// 是否存在点位图坐标
        /// </summary>
        public bool ExistBitMap { get; set; }
    }
}
