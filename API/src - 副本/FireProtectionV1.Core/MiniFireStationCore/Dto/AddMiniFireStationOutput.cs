﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class AddMiniFireStationOutput:SuccessOutput
    {
        /// <summary>
        /// 微型消防站ID
        /// </summary>
        public int MiniFireStationId { get; set; }
    }
}
