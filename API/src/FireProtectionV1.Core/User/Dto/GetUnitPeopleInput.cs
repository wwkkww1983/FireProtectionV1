using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class GetUnitPeopleInput
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        [Required]
        public int AccountID { get; set; }
    }
}
