using FireProtectionV1.Common.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Text;

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

    public enum FireUnitRole
    {
        /// <summary>
        /// 防火单位管理员
        /// </summary>
        [Description("防火单位管理员")]
        FireUnitManager = 1,
        /// <summary>
        /// 防火单位值班员
        /// </summary>
        [Description("防火单位值班员")]
        FireUnitDuty = 2,
        /// <summary>
        /// 防火单位巡查员
        /// </summary>
        [Description("防火单位巡查员")]
        FireUnitPatrol = 3,
    }
}
