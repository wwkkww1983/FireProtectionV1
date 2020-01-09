using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace TsjDeviceServer.DeviceCtrl
{
    /* 命令对象 转换为Json数据调用Send方法
    public class CmdSwitch
    {
        public string cmd = "Switch";
        public string deviceSn;
    }
    public class CmdUpdateAnalog
    {
        public string cmd = "UpdateAnalog";
        public string deviceSn;
    }
    */
    public class CmdClt
    {
        static public void Send(string jsonCmd)
        {
            using (NamedPipeClientStream pipeClient =
            new NamedPipeClientStream(".", "TsjCmdSrv", PipeDirection.Out))
            {
                pipeClient.Connect();

                try
                {
                    // Read user input and send that to the client process.
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine(jsonCmd);
                    }
                }
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    throw e;
                    //Console.WriteLine("ERROR: {0}", e.Message);
                }
            }
        }
    }
}
