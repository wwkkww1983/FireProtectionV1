using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Dto
{
    public class HikapiAlarmInput
    {
        public string method { get; set; }
        public HikapiParams Params { get; set; }
    }
    public class HikapiParams
    {
        public DateTime sendTime { get; set; }
        public string ability { get; set; }
        public List<string> uids { get; set; }
        public List<string> clients { get; set; }
        public List<HikapiEvent> events { get; set; }
    }
    public class HikapiEvent
    {
        public string eventId { get; set; }
        /// <summary>
        /// 事件规则ID
        /// </summary>
        public string srcIndex { get; set; }
        public string srcType { get; set; }
        /// <summary>
        /// 事件规则名称
        /// </summary>
        public string srcName { get; set; }
        public int eventLvl { get; set; }
        public int eventType { get; set; }
        /// <summary>
        /// 事件规则名称
        /// </summary>
        public string eventName { get; set; }
        public int status { get; set; }
        public int timeout { get; set; }
        public DateTime happenTime { get; set; }
        public DateTime stopTime { get; set; }
        /// <summary>
        /// 用户自定义注释
        /// </summary>
        public string remark { get; set; }
        public List<HikapiEventDetail> eventDetails { get; set; }
    }
    public class HikapiEventDetail
    {
        public int eventType { get; set; }
        public string ability { get; set; }
        public string srcIndex { get; set; }
        public string srcType { get; set; }
        public string srcName { get; set; }
        public string regionIndexCode { get; set; }
        public string regionName { get; set; }
        public string locationIndexCode { get; set; }
        public string locationName { get; set; }
    }
}
