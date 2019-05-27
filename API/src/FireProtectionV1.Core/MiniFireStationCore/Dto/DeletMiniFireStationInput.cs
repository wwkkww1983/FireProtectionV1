using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class DeletMiniFireStationInput
    {
        /// <summary>
        /// 迷你消防站ID
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
