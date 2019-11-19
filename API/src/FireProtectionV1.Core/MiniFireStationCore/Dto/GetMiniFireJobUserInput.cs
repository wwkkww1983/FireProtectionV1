using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class GetMiniFireJobUserInput : PagedResultRequestDto
    {
        /// <summary>
        /// 微型消防站ID
        /// </summary>
        [Required]
        public int MiniFireStationId { get; set; }
    }
}
