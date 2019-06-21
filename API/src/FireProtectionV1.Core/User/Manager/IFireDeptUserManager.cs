using Abp.Domain.Services;
using FireProtectionV1.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public interface IFireDeptUserManager : IDomainService
    {
        Task<DeptUserLoginOutput> UserLogin(LoginInput input);
        //Task<UserRegistOutput> UserRegist(UserRegistInput input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> ChangePassword(DeptChangePassword input);
    }
}
