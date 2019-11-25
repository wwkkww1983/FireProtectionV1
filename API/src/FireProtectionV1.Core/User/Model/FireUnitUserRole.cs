using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Text;
using ExportAttribute = FireProtectionV1.Common.DBContext.ExportAttribute;

namespace FireProtectionV1.User.Model
{
    public class FireUnitUserRole:EntityBase
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        [Required]
        public int AccountID { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [Required]
        public FireUnitRole Role { get; set; }
    }

    [Export("防火单位人员角色")]
    public enum FireUnitRole
    {
        /// <summary>
        /// 消防管理员
        /// </summary>
        [Description("消防管理员")]
        FireUnitManager = 1,
        /// <summary>
        /// 消防值班员
        /// </summary>
        [Description("消防值班员")]
        FireUnitDuty = 2,
        /// <summary>
        /// 消防巡查员
        /// </summary>
        [Description("消防巡查员")]
        FireUnitPatrol = 3,
        /// <summary>
        /// 消防人员
        /// </summary>
        [Description("消防人员")]
        FireUnitPeople = 4
    }
}
