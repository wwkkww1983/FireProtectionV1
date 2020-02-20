using Abp.Domain.Services;
using FireProtectionV1.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public interface IEngineerUserManager : IDomainService
    {
        /// <summary>
        /// 工程人员手机端登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EngineerUserLoginOutput> LoginForMobile(LoginInput input);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> ChangePassword(ChangeUserPassword input);
    }
}
