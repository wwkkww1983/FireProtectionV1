using FireProtectionV1.Configuration;
using GovFire.Dto;
using Newtonsoft.Json.Linq;
using System;

namespace GovFire
{
    public class DataApi
    {
        /// <summary>
        /// 返回大联动数据Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        static public string UpdateAlarm(AlarmDto dto)
        {
            string s = $"firecompany={dto.firecompany}&devicesn={dto.devicesn}&devicetype={dto.devicetype}&devicelocation={dto.devicelocation}&alarmtime={dto.alarmtime}&additionalinfo={dto.additionalinfo}&lat={dto.lat}&lon={dto.lon}";
            
            var res=GovRequst.HttpPost("/ioc/fire/saveFireUnit", s);
            if (!string.IsNullOrEmpty(res))
            {
                var jobj = JObject.Parse(res);
                return jobj["fireUnitId"].ToString();
            }
            return "";
        }
        static public void UpdateEvent(EventDto dto)
        {
            string s = $"fireUnitId={dto.fireUnitId}&id={dto.id}&firecompany={dto.firecompany}&eventtype={dto.eventtype}&eventcontent={dto.eventcontent}&createtime={dto.createtime}&donetime={dto.donetime}&state={dto.state}&lat={dto.lat}&lon={dto.lon}";
            GovRequst.HttpPost("/ioc/fire/saveOrUpdateAffire", s);
        }
    }
}
