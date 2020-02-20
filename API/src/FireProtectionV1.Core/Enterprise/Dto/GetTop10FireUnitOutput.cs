using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class GetTop10FireUnitOutput
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 防火单位名称
        /// </summary>
        public string Name { get; set; }
    }
}
