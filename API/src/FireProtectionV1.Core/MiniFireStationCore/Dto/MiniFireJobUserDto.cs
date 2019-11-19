using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.MiniFireStationCore.Dto
{
    public class MiniFireJobUserDto
    {
        /// <summary>
        /// 微型消防站人员ID
        /// </summary>
        public int JobUserId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Job { get; set; }
    }
    public class MiniFireJobUserDetailDto
    {
        /// <summary>
        /// 微型消防站人员ID
        /// </summary>
        public int JobUserId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string PhotoBase64 { get; set; }
    }
    public class AddMiniFireJobUserDto
    {
        /// <summary>
        /// 微型消防站ID
        /// </summary>
        public int MiniFireStationId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string PhotoBase64 { get; set; }
    }
}
