using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.Enum
{
    public enum CheckStateType:byte
    {
        /// <summary>
        /// 未处理
        /// </summary>
        UnCheck=0,
        /// <summary>
        /// 误报
        /// </summary>
        False,
        /// <summary>
        /// 测试
        /// </summary>
        Test,
        /// <summary>
        /// 真实报警
        /// </summary>
        True,
        /// <summary>
        /// 已过期
        /// </summary>
        Expire
    }
    public class CheckStateTypeNames
    {
        static public string GetName(CheckStateType type)
        {
            string name = "";
            switch (type)
            {
                case CheckStateType.UnCheck:
                    name = "核警";
                    break;
                case CheckStateType.False:
                    name = "误报";
                    break;
                case CheckStateType.Test:
                    name = "测试";
                    break;
                case CheckStateType.True:
                    name = "真实火警";
                    break;
                case CheckStateType.Expire:
                    name = "已过期";
                    break;
            }
            return name;
        }
    }
}
