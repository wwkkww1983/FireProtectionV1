using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class Log
    {
        static public void Error(string str)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Error\r\n{str}");
        }
        static public void Info(string str)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Info\r\n{str}");
        }
    }
}
