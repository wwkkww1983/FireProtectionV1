using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Account.Model;
using FireProtectionV1.Common.Helper;

namespace FireProtectionV1.Account.Manager
{
    public class FireDeptUserManager:IFireDeptUserManager
    {
        IRepository<FireDeptUser> _fireDeptUserRep;
        public FireDeptUserManager(IRepository<FireDeptUser> fireDeptUserRep)
        {
            _fireDeptUserRep = fireDeptUserRep;
        }
        public async Task<UserLoginOutput> UserLogin(UserLoginInput input)
        {
            string md5=MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            UserLoginOutput ouput = new UserLoginOutput() { Success = true, Result = "登录成功" };
            var v =await _fireDeptUserRep.FirstOrDefaultAsync(p => p.Account.Equals(input.Account)&&p.Password.Equals(md5));
            if (v == null)
            {
                ouput.Success = false;
                ouput.Result = "账号或密码不正确";
            }
            //await Task.Delay(1);
            return ouput;
        }
        public async Task<UserRegistOutput> UserRegist(UserRegistInput input)
        {
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            UserRegistOutput ouput = new UserRegistOutput() { Success = true, Result = "注册成功" };
            if (_fireDeptUserRep.FirstOrDefault(p => p.Account.Equals(input.Account))!=null)
            {
                ouput.Success = false;
                ouput.Result = "账号已存在";
            }
            else
            {
                try
                {
                var v = await _fireDeptUserRep.InsertAsync(new FireDeptUser() {
                    Account =input.Account,
                    Password= md5,
                    FireDeptId=input.FireDeptId,
                    Name=input.Name
                });

                }catch(Exception e)
                {
                    ouput.Success = false;
                    ouput.Result = "参数不正确";
                }
            }
            //await Task.Delay(1);
            return ouput;
        }
    }
}
