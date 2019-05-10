using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.SupervisionCore.Model
{
    /// <summary>
    /// 监督执法记录主表
    /// </summary>
    public class Supervision : EntityBase
    {
        /// <summary>
        /// 防火单位Id
        /// </summary>
        public int FireUnitId { get; set; }
        /// <summary>
        /// 检查人员
        /// </summary>
        public string CheckUser { get; set; }
        /// <summary>
        /// 检查结论
        /// </summary>
        public CheckResult CheckResult { get; set; }
        /// <summary>
        /// 记录提交人员Id
        /// </summary>
        public int FireDeptUserId { get; set; }
        /// <summary>
        /// 其它情况说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 法律文书-当场
        /// </summary>
        public int DocumentSite { get; set; }
        /// <summary>
        /// 法律文书-限期
        /// </summary>
        public int DocumentDeadline { get; set; }
        /// <summary>
        /// 法律文书-重大
        /// </summary>
        public int DocumentMajor { get; set; }
        /// <summary>
        /// 法律文书-复查
        /// </summary>
        public int DocumentReview { get; set; }
        /// <summary>
        /// 法律文书-检查意见书
        /// </summary>
        public int DocumentInspection { get; set; }
        /// <summary>
        /// 法律文书-处罚决定书
        /// </summary>
        public int DocumentPunish { get; set; }
    }

    public enum CheckResult
    {
        合格 = 1,
        现场改正 = 0,
        限期整改 = -1,
        停业整顿 = -2,
        未指定 = -10
    }
}
