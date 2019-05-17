using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.Helper
{
    public class MethodHelper
    {
        public static string CreateInvitationCode()
        {
            byte[] r = new byte[8];
            Random rand = new Random((int)(DateTime.Now.Ticks % 1000000));
            //生成6字节原始数据
            for (int i = 0; i < 6; i++)
            {
                //while循环剔除非字母和数字的随机数
                 //int   ran = rand.Next(48, 57);
                // r[i] = Convert.ToByte(ran);
                r[i]= (byte)rand.Next(48, 57);

            }
            //转换成8位String类型               
            return Encoding.ASCII.GetString(r);
        }
    }
}
