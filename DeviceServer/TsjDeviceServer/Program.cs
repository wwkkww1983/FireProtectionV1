using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading.Tasks;

namespace TsjDeviceServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAll( ClientTest.RunAsync());
            while(!Console.ReadLine().Equals("exit"));
        }
    }
}
