using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    /// <summary>
    /// 地图呼吸气泡
    /// </summary>
    public class BreathingBubble
    {
        public int id { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int value { get; set; } = 1;
        public int type { get; set; } = 1;
        public string info { get; set; }
    }
}
