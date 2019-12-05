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
    public class FireUnitRoleFunc
    {
        static public List<string> GetListName(List<FireUnitRole> lstRole)
        {
            List<string> lst = new List<string>();
            foreach(var v in lstRole)
            {
                lst.Add(GetRoleName(v));
            }
            return lst;
        }
        static public FireUnitRole GetRoleEnum(string role)
        {
            switch (role)
            {
                case "消防管理员":
                    return FireUnitRole.FireUnitManager;
                case "消防值班员":
                    return FireUnitRole.FireUnitDuty;
                case "消防巡查员":
                    return FireUnitRole.FireUnitPatrol;
                case "消防人员":
                    return FireUnitRole.FireUnitPeople;
            }
            return FireUnitRole.FireUnitPeople;
        }
        static public string GetRoleName(FireUnitRole role)
        {
            switch (role)
            {
                case FireUnitRole.FireUnitManager:
                    return "消防管理员";
                case FireUnitRole.FireUnitDuty:
                    return "消防值班员";
                case FireUnitRole.FireUnitPatrol:
                    return "消防巡查员";
                case FireUnitRole.FireUnitPeople:
                    return "消防人员";
            }
            return "";
        }
    }
}
