using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.HydrantCore.Dto
{
    public class DeletHydrantInput
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
