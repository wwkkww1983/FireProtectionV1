using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.FireWorking.Dto
{
    public class AddNewPatrolInput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 防火单位用户Id
        /// </summary>
        public int FireUnitUserId { get; set; }
    }
}
