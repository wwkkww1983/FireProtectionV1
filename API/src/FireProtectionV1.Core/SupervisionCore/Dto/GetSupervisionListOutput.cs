using FireProtectionV1.SupervisionCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Dto
{
    public class GetSupervisionListOutput
    {
        public int Id { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string FireUnitName { get; set; }
        /// <summary>
        /// 检查人员
        /// </summary>
        public string CheckUser { get; set; }
        /// <summary>
        /// 检查日期
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 检查结论
        /// </summary>
        public CheckResult CheckResult { get; set; }
    }
}
