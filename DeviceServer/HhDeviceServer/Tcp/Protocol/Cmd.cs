using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol
{
    /* 0 预留
     * 7～127 预留
     * 128～255 用户自行定义
     */

    /// <summary>
    /// 控制单元命令字节定义
    /// </summary>
    public enum Cmd :byte
    {
        Unknown=0,
        /// <summary>
        /// 控制命令 时间同步
        /// </summary>
        Control = 1,
        /// <summary>
        /// 发送数据 发送火灾报警和建筑消防设施运行状态等信息
        /// </summary>
        Send = 2,
        /// <summary>
        /// 确认 对控制命令和发送信息的确认回答
        /// </summary>
        Confirm = 3,
        /// <summary>
        /// 请求 查询火灾报警和建筑消防设施运行状态等信息
        /// </summary>
        Requst = 4,
        /// <summary>
        /// 应答 返回查询的信息
        /// </summary>
        Response = 5,
        /// <summary>
        /// 否认 对控制命令和发送信息的否认回答
        /// </summary>
        Deny=6,
        /// <summary>
        /// 安吉斯电气网关命令07
        /// </summary>
        Ajs07=7,
        /// <summary>
        /// 安吉斯电气网关命令08
        /// </summary>
        Ajs08 = 8
    }
}
