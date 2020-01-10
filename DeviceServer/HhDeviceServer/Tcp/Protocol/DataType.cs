using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceServer.Tcp.Protocol
{
    /// <summary>
    /// 数据单元类型字节定义
    /// </summary>
    enum DataType : byte
    {
        /// <summary>
        /// 上传建筑消防设施系统状态 上行
        /// </summary>
        UploadSysState = 1,
        /// <summary>
        /// 上传建筑消防设施部件运行状态 上行
        /// </summary>
        UploadUnitState = 2,
        /// <summary>
        /// 上传建筑消防设施部件模拟量值 上行
        /// </summary>
        UploadUnitAnalog = 3,
        /// <summary>
        /// 上传建筑消防设施操作信息 上行
        /// </summary>
        UploadOperatInfo = 4,
        /// <summary>
        /// 上传建筑消防设施软件版本 上行
        /// </summary>
        UploadVersion = 5,
        /// <summary>
        /// 上传建筑消防设施系统配置情况 上行
        /// </summary>
        UploadSysConfig = 6,
        /// <summary>
        /// 上传建筑消防设施部件配置情况 上行
        /// </summary>
        UploadUnitConfig = 7,
        /// <summary>
        /// 上传建筑消防设施系统时间 上行
        /// </summary>
        UploadSysTime = 8,
        /// <summary>
        /// 上传用户信息传输装置运行状态 上行
        /// </summary>
        UploadUITDState = 21,
        /// <summary>
        /// 上传用户信息传输装置操作信息 上行
        /// </summary>
        UploadUITDOperatInfo = 24,
        /// <summary>
        /// 上传用户信息传输装置软件版本 上行
        /// </summary>
        UploadUITDVersion = 25,
        /// <summary>
        /// 上传用户信息传输装置配置情况 上行
        /// </summary>
        UploadUITDConfig = 26,
        /// <summary>
        /// 上传用户信息传输装置系统时间 上行
        /// </summary>
        UploadUITDTime = 28,
        /// <summary>
        /// 读建筑消防设施系统状态 下行
        /// </summary>
        ReadSysState = 61,
        /// <summary>
        /// 读建筑消防设施部件运行状态 下行
        /// </summary>
        ReadUnitRunState = 62,
        /// <summary>
        /// 读建筑消防设施部件模拟量值 下行
        /// </summary>
        ReadUnitAnalog = 63,
        /// <summary>
        /// 读建筑消防设施操作信息 下行
        /// </summary>
        ReadOperatInfo = 64,
        /// <summary>
        /// 读建筑消防设施软件版本 下行
        /// </summary>
        ReadVersion = 65,
        /// <summary>
        /// 读建筑消防设施系统配置情况 下行
        /// </summary>
        ReadSysConfig = 66,
        /// <summary>
        /// 读建筑消防设施部件配置情况 下行
        /// </summary>
        ReadUnitConfig = 67,
        /// <summary>
        /// 读建筑消防设施系统时间 下行
        /// </summary>
        ReadSysTime = 68,
        /// <summary>
        /// 读用户信息传输装置运行状态 下行
        /// </summary>
        ReadRunState = 81,
        /// <summary>
        /// 读用户信息传输装置操作信息记录 下行
        /// </summary>
        ReadUITDOperatInfo = 84,
        /// <summary>
        /// 读用户信息传输装置软件版本 下行
        /// </summary>
        ReadUITDVersion = 85,
        /// <summary>
        /// 读用户信息传输装置配置情况 下行
        /// </summary>
        ReadUITDConfig = 86,
        /// <summary>
        /// 读用户信息传输装置系统时间 下行
        /// </summary>
        ReadUITDTime = 88,
        /// <summary>
        /// 初始化用户信息传输装置 下行
        /// </summary>
        InitUITD = 89,
        /// <summary>
        /// 同步用户信息传输装置时钟 下行
        /// </summary>
        SyncUITDTime = 90,
        /// <summary>
        /// 查岗命令 下行
        /// </summary>
        Inspect = 91,
        /// <summary>
        /// 安吉斯自定义类型(可能是同步时间)
        /// </summary>
        AjsA1=0xA1
    }
}
