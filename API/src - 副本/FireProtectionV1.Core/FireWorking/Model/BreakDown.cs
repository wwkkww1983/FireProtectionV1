﻿using FireProtectionV1.Common.DBContext;
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
        [Required]
        public int FireUnitId { get; set; }
        /// <summary>
        /// 记录人员ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 记录人员类型：FireUnitUser,SafeUnitUser
        /// </summary>
        [MaxLength(20)]
        public string UserFrom { get; set; }
        /// <summary>
        /// 处理人员Id
        /// </summary>
        public int DoUserId { get; set; }
        /// <summary>
        /// 处理人员类型：FireUnitUser,SafeUnitUser
        /// </summary>
        [MaxLength(20)]
        public string DoUserFrom { get; set; }
        /// <summary>
        /// 故障来源（1.值班 2.巡查 3.物联终端）
        /// </summary>
        [Required]
        public byte Source { get; set; }
        /// <summary>
        /// 处理状态（1待处理,2处理中,3已解决,4自行处理,5维保叫修处理中,6维保叫修已处理）
        /// </summary>
        [Required]
        public byte HandleStatus { get; set; }
        /// <summary>
        /// 故障来源数据ID
        /// </summary>
        [Required]
        public int DataId { get; set; }
        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime SolutionTime { get; set; }
        /// <summary>
        /// 问题处理途径（1.自行处理 2.维保叫修）
        /// </summary>
        public byte SolutionWay { get; set; }
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
