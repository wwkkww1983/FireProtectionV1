using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.BigScreen.Dto
{
    /// <summary>
    /// 文本数据（单行 | 多行）
    /// </summary>
    public class DataText
    {
        public string value { get; set; }
    }
    /// <summary>
    /// 小数数据
    /// </summary>
    public class DataDouble
    {
        public double value { get; set; }
    }
    /// <summary>
    /// XY坐标数据
    /// </summary>
    public class DataXY
    {
        public string X { get; set; }
        public int Y { get; set; }
    }
}
