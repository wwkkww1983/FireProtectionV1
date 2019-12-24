using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class OneNetAppService: AppServiceBase
    {
        /// <summary>
        /// OneNet测试
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="msg"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public async Task<string> Test(string nonce, string msg, string signature)
        {
            return msg;
        }
    }
}
