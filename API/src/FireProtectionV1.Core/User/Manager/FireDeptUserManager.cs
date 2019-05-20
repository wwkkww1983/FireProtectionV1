﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;

namespace FireProtectionV1.User.Manager
{
    public class FireDeptUserManager:IFireDeptUserManager
    {
        IRepository<FireDept> _fireDeptRep;
        IRepository<FireDeptUser> _fireDeptUserRep;
        public FireDeptUserManager(IRepository<FireDeptUser> fireDeptUserRep, IRepository<FireDept> fireDeptRep)
        {
            _fireDeptUserRep = fireDeptUserRep;
            _fireDeptRep = fireDeptRep;
        }
        public async Task<DeptUserLoginOutput> UserLogin(DeptUserLoginInput input)
        {
            string md5=MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            DeptUserLoginOutput output = new DeptUserLoginOutput() { Success = true };
            var v =await _fireDeptUserRep.FirstOrDefaultAsync(p => p.Account.Equals(input.Account)&&p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "账号或密码不正确";
            }
            else
            {
                output.UserId = v.Id;
                output.Name = v.Name;
                var dept = await _fireDeptRep.SingleAsync(p => p.Id == v.FireDeptId);
                if (dept != null)
                    output.DeptName = dept.Name;
            }
            return output;
        }

        //public async Task<UserRegistOutput> UserRegist(UserRegistInput input)
        //{
        //    string md5 = MD5Encrypt.Encrypt(input.Password + input.Account, 16);
        //    UserRegistOutput ouput = new UserRegistOutput() { Success = true, Result = "注册成功" };
        //    if (_fireDeptUserRep.FirstOrDefault(p => p.Account.Equals(input.Account))!=null)
        //    {
        //        ouput.Success = false;
        //        ouput.Result = "账号已存在";
        //    }
        //    else
        //    {
        //        try
        //        {
        //        var v = await _fireDeptUserRep.InsertAsync(new FireDeptUser() {
        //            Account =input.Account,
        //            Password= md5,
        //            FireDeptId=input.FireDeptId,
        //            Name=input.Name
        //        });

        //        }catch(Exception e)
        //        {
        //            ouput.Success = false;
        //            ouput.Result = "参数不正确";
        //        }
        //    }
        //    //await Task.Delay(1);
        //    return ouput;
        //}
    }
}
