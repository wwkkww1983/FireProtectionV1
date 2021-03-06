﻿using System;
using System.IO;
using System.IO.Pipes;

namespace testSrv
{
    class Program
    {
        static void Main(string[] args)
        {
            NamedPipeServerStream pipeServer =
                 new NamedPipeServerStream("testpipe", PipeDirection.In);//创建连接
            pipeServer.WaitForConnection();//等待连接，程序会阻塞在此处，直到有一个连接到达
            while (true)
            {
                if (!pipeServer.IsConnected)
                {
                    pipeServer.Close();
                    pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.In);
                    pipeServer.WaitForConnection();//等待连接，程序会阻塞在此处，直到有一个连接到达
                }
                try
                {
                    // Read user input and send that to the client process.
                    using (StreamReader sr = new StreamReader(pipeServer))
                    {
                        string rl = sr.ReadLine();
                        if (!string.IsNullOrEmpty(rl))
                            Console.WriteLine(rl);
                    }
                }
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }

            }
            Console.ReadKey();
        }
    }
}
