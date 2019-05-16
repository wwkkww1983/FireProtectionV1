using FireProtectionV1.Common.Helper;
using System;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = new int[] { 1, 2, 3, 4 };
            int[] arr2 = new int[] { 2, 3, 3, 4 };
            var v = from a in arr1
                    join b in arr2
                    on a equals b
                    select a;
            for(int i=0;i<5;i++)
                Console.WriteLine( MethodHelper.CreateInvitationCode());
            Console.Read();
        }
    }
}
