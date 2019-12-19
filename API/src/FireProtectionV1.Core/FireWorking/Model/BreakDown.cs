using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.FireWorking.Model
{
    public class BreakDown : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 记录人员ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 处理人员Id（根据SolutionWay，可能是防火单位人员，也可能是维保单位人员）
        /// </summary>
        public int DoUserId { get; set; }
        /// <summary>
        /// 故障来源
        /// </summary>
        public FaultSource Source { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public HandleStatus HandleStatus { get; set; }
        /// <summary>
        /// 故障来源数据ID（值班表、巡查表、火警联网部件故障表的Id）
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime SolutionTime { get; set; }
        /// <summary>
        /// 问题处理途径
        /// </summary>
        public HandleChannel SolutionWay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }
        /// <summary>
        /// 派单时间
        /// </summary>
        public DateTime DispatchTime { get; set; }
        /// <summary>
        /// 维保处理完成时间
        /// </summary>
        public DateTime SafeCompleteTime { get; set; }
    }
    public class BreakDownWords
    {
        static public string GetHandleStatus(byte state)
        {
            if (state == 1)
                return "未处理";
            if (state == 2)
                return "处理中";
            if (state == 3)
                return "已解决";
            return "未知";
        }
        static public string GetSource(byte source)
        {
            if (source == 1)
                return "值班故障";
            if (source == 2)
                return "巡查故障";
            if (source == 3)
                return "物联终端故障";
            return "未知";
        }
    }
}
