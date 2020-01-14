using Common;
using DeviceServer.Tcp;
using Newtonsoft.Json.Linq;
using System;

namespace DeviceServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //check 00
            //string s = "40 40 01 00 05 01 1f 1f 09 18 03 13 0b 00 00 01 c8 01 c0 a8 01 3f 00 00 08 00 02 1c 01 1f 1f 09 18 03 13";// 95 23 23";
            //string s = "01 00 05 01 1f 1f 09 18 03 13 0b 00 00 01 c8 01 c0 a8 01 3f 00 00 08 00 02 1c 01 1f 1f 09 18 03 13";// 95 23 23";
            //string s = "02 00 05 01 3b 1f 09 18 03 13 0b 00 00 01 c8 01 c0 a8 01 3f 00 00 08 00 02 1c 01 3b 1f 09 18 03 13";
            //var ss = s.Split(' ');
            //int sum = 0;
            //for(int i = 0; i < ss.Length; i++)
            //{
            //    byte bt = Convert.ToByte(ss[i],16);
            //    sum += bt;
            //}
            //string check = ((byte)sum).ToString("X2");
            //Transfer transfer = new Transfer();
            //int i = 0;
            //while (true)
            //{
            //    var add = Config.Configuration[$"Transfer:{i}:Address"];
            //    var ip = Config.Configuration[$"Transfer:{i}:IP"];
            //    var port0 = Config.Configuration[$"Transfer:{i}:Port"];
            //    var portlocal = Config.Configuration[$"Transfer:{i}:LocalPort"];
            //    if (add == null || ip == null || port0 == null)
            //        break;
            //    transfer.AddTransDis(add, ip, int.Parse(port0),int.Parse(portlocal));
            //    i++;
            //}

            SessionManager manager = new SessionManager(new Disposal());
            TcpSrv srv = new TcpSrv(manager);
            //string ip = Config.Configuration["TCP:ServerIP"];
            int port = int.Parse(Config.Configuration["TCP:ServerPort"]);
            string s=srv.Start(port);
            Console.WriteLine($"服务器启动成功!{s}");
            //Console.WriteLine($"服务器启动成功!IP：{ip} 端口：{port}");
            while (!Console.ReadLine().Equals("exit"))
            {

            }
        }
    }
}
