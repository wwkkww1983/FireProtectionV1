using System;
using System.Collections.Generic;
using System.Text;

namespace GovFire.Dto
{
    public class AlarmDto
    {
        public string firecompany { get; set; }
        public string devicesn { get; set; }
        public string devicetype { get; set; }
        public string devicelocation { get; set; }
        public string alarmtime { get; set; }
        public string additionalinfo { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string lon { get; set; }
    }
}
