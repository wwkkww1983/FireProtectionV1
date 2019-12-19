using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class PutUserAreaInput
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserID { get; set; }
        /// <summary>
        /// 操作（0新增/1删除）
        /// </summary>
        [Required]
        public Operation Operation { get; set; }
        /// <summary>
        /// 操作（0新增/1删除）
        /// </summary>
        [Required]
        public List<GetHyrantAreaOutput> arealist { get; set; }
    }

    public enum Operation
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        add = 0,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        delete = 1,
    }
}
