using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.Enterprise.Dto
{
    public class DeletFireUnitInput
    {
        /// <summary>
        /// 防火单位ID
        /// </summary>
        [Required]
        public int Id { get; set; }

    }
}
