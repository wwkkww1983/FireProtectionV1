using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    public class GetFireunitLatForMapOutput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireunitId { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Lat { get; set; }
        /// <summary>
        /// 本月是否存在真实火警
        /// </summary>
        public bool ExistTrueFireAlarm { get; set; }
    }
}
