using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    /// <summary>
    /// 柱状图
    /// </summary>
    public class Histogram
    {
        public string x { get; set; }
        public int y { get; set; }
        public int s { get; set; } = 1;
    }
}
