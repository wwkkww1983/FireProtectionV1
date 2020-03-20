using Common;
using DeviceServer.Tcp.Protocol;
using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TsjWebApi;

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
            FireApi.HttpPost(Config.Url("/api/services/app/Data/AddAlarmFire"), v);

        }
        static byte[] bts = new byte[2];
        static internal void Log(byte b)
        {
            using (StreamWriter sw = new StreamWriter("c:/rec_" + DateTime.Now.ToString("yyMMdd"),true))
            {
                if (bts[0] == 0x40 && bts[1] == 0x40)
                    sw.WriteLine();
                string s = b.ToString("X2") + " ";
                sw.Write(s);
            }
            bts[0] = bts[1];
            bts[1] = b;
        }
    }
}
