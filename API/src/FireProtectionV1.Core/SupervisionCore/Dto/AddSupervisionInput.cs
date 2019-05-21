using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class AddSupervisionInput
    {
        /// <summary>
        /// 综合信息
        /// </summary>
        public Supervision Supervision { get; set; }
        /// <summary>
        /// 明细项目信息
        /// </summary>
        public List<AddSupervisionDetailInput> SupervisionDetailInputs { get; set; }
    }
}
