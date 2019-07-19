using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class GetHydrantAlarmInfoInput
    {
        /// <summary>
        /// 警情ID
        /// </summary>
        [Required]
        public int AlarmID { get; set; }
    }
}
