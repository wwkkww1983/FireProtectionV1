using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

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
        static public async Task SendAsync(string jsonCmd)
        {
            using (NamedPipeClientStream pipeClient =
            new NamedPipeClientStream(".", "TsjCmdSrv", PipeDirection.Out))
            {
                

                try
                {
                    pipeClient.Connect(2 * 1000);
                    // Read user input and send that to the client process.
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        sw.AutoFlush = true;
                        await sw.WriteLineAsync(jsonCmd);
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
