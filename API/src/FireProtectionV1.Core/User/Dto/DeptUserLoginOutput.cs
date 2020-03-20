using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.User.Dto
{
    public class DeptUserLoginOutput : SuccessOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 是否微型消防站人员ID
        /// </summary>
        public bool IsMiniFireUser { get; set; }
        /// <summary>
        /// 辖区名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 辖区等级（1:市 2:区县）
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 辖区中心位置经度
        /// </summary>
        public decimal Lng { get; set; }
        /// <summary>
        /// 辖区中心位置纬度
        /// </summary>
        public decimal Lat { get; set; }
    }
}
