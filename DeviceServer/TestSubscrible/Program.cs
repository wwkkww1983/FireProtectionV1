using System;
using System.Threading.Tasks;
using TsjDeviceServer;

namespace TestSubscrible
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAll(ClientTest.RunAsync());
            while (!Console.ReadLine().Equals("exit")) ;
        }
    }
}
