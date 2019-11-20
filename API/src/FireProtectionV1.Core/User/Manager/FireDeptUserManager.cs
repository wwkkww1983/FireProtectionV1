using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.MiniFireStationCore.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;

namespace FireProtectionV1.User.Manager
{
    public class FireDeptUserManager:IFireDeptUserManager
    {
        IRepository<FireUnit> _repFireUnit;
        IRepository<MiniFireStation> _repMiniFireStation;
        IRepository<MiniFireStationJobUser> _repMiniFireStationJobUser;
        IRepository<FireDept> _fireDeptRep;
        IRepository<FireDeptUser> _fireDeptUserRep;
        public FireDeptUserManager(
            IRepository<FireUnit> repFireUnit,
            IRepository<MiniFireStation> repMiniFireStation,
            IRepository<MiniFireStationJobUser> repMiniFireStationJobUser,
            IRepository<FireDeptUser> fireDeptUserRep, IRepository<FireDept> fireDeptRep)
        {
            _repFireUnit = repFireUnit;
            _repMiniFireStation = repMiniFireStation;
            _repMiniFireStationJobUser = repMiniFireStationJobUser;
            _fireDeptUserRep = fireDeptUserRep;
            _fireDeptRep = fireDeptRep;
        }
        public async Task<DeptUserLoginOutput> UserLogin(LoginInput input)
        {
            string md5=MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            DeptUserLoginOutput output = new DeptUserLoginOutput() { Success = true,IsMiniFireUser=false };
            var minifireuser = await _repMiniFireStationJobUser.FirstOrDefaultAsync(p => p.ContactPhone.Equals(input.Account) && p.Job.Equals("站长"));
            if (minifireuser != null)
            {
                output.UserId = minifireuser.Id;
                output.Name = minifireuser.ContactName;
                var ministation = await _repMiniFireStation.FirstOrDefaultAsync(p => p.Id == minifireuser.MiniFireStationId);
                var fireunit = await _repFireUnit.FirstOrDefaultAsync(p => p.Id == ministation.FireUnitId);
                if(fireunit!=null)
                    output.DeptName = fireunit.Name;
                output.IsMiniFireUser = true;
                return output;
            }
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
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            string md5 = MD5Encrypt.Encrypt(input.OldPassword + input.Account, 16);
            SuccessOutput output = new SuccessOutput() { Success = true };
            var v = await _fireDeptUserRep.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "当前密码不正确";
            }
            else
            {
                string newMd5= MD5Encrypt.Encrypt(input.NewPassword + input.Account, 16);
                v.Password = newMd5;
                var x = await _fireDeptUserRep.UpdateAsync(v);
                output.Success = true;
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
