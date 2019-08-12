using DeviceServer.Tcp.Protocol;
using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeviceServer
{
    public class TestAjsDeviceServer
    {
        static public void Test()
        {
            //List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
            var v = new AddAlarmFireInput
            {
                DetectorGBType = (byte)UnitType.UITD,
                Identify = "11.0.0.1.200.1",
                GatewayIdentify = "11.0.0.1.200.1",
                Origin=Origin.Anjisi
            };
            //formData.Add(new KeyValuePair<string, string>("DetectorGBType", UnitType.UITD.ToString()));
            //formData.Add(new KeyValuePair<string, string>("Identify", "11.0.0.1.200.1"));
            //formData.Add(new KeyValuePair<string, string>("GatewayIdentify", "11.0.0.1.200.1"));
            FireApi.HttpPost("/api/services/app/Data/AddAlarmFire", v);

        }
        static internal void Log(string s)
        {

            using (StreamWriter sw = new StreamWriter("c:/rec_" + DateTime.Now.ToString("yyMMdd"),true))
            {
                sw.Write(s);
            }
        }
    }
}
