using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.Helper
{
    public class MethodHelper
    {
        public static string CreateInvitationCode()
        {
            string code = "";
            for (int i = 0; i < 5; i++)
            {
                code = code + new Random().Next(0, 9).ToString();
            }
            return code;
        }
    }
}
