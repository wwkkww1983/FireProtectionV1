using Common;
using FireProtectionV1.FireWorking.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TsjDeviceServer.Data;
using TsjWebApi;

namespace TsjDeviceServer
{
    /// <summary>
    /// 后台topic定义
    /// </summary>
    class Protocol
    {
        public event EventHandler ReportAlarm;
        /// <summary>
        /// 后台订阅topic列表
        /// </summary>
        static public List<string> SubTopics = new List<string>()
        {
                "Response/#",
                "Report/#"
        };
        /// <summary>
        /// 后台请求某设备
        /// </summary>
        /// <param name="gatewayIdentify"></param>
        /// <returns></returns>
        static public string Pub_GetConfig(string gatewayIdentify)
        {
            return $"Get/{gatewayIdentify}/Config";
        }
        /// <summary>
        /// 后台设置某设备
        /// </summary>
        /// <param name="gatewayIdentify"></param>
        /// <returns></returns>
        static public string Pub_SetConfig(string gatewayIdentify)
        {
            return $"Set/{gatewayIdentify}/Config";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="level">主题层级</param>
        /// <returns></returns>
        static public string Level(string topic,int level)
        {
            try
            {
                return topic.Split('/')[level - 1];
            }
            catch (Exception)
            {
                return "";
            }
        }
        static public void ParseMsg(TsjTopic topic,string payload)
        {
            //if (topic.Level(1).Equals("Report"))
            //{
            //    var reportData = JsonConvert.DeserializeObject<ReportData>(payload);
            //    if (topic.Level(3).Equals("Alarm"))
            //    {
            //        FireApi.HttpPost(Config.Url("/api/services/app/Data/AddAlarmFire"), new AddAlarmFireInput()
            //        {
            //            GatewayIdentify=topic.Level(2),
            //            Identify = reportData.Identify,
            //            Origin = Origin.Tianshuju,
            //            DetectorGBType = reportData.GBType
            //        });
            //    }
            //}
        }
    }
}
