using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Model
{
    public class HydrantUserArea : EntityBase
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        [Required]
        public int AccountID { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        [Required]
        public int AreaID { get; set; }
    }
}
