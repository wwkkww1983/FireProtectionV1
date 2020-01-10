using System;
using System.IO;
using System.IO.Pipes;

namespace testClt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NamedPipeClientStream pipeClient =
                        new NamedPipeClientStream(".", "testpipe", PipeDirection.Out))
            {
                pipeClient.Connect();

                try
                {
                    // Read user input and send that to the client process.
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine("hello world ");
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
