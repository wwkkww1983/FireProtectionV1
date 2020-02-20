using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public class EngineerUserManager : DomainService, IEngineerUserManager
    {
        IRepository<EngineerUser> _repEngineerUser;
        public EngineerUserManager(
            IRepository<EngineerUser> repEngineerUser)
        {
            _repEngineerUser = repEngineerUser;
        }
        /// <summary>
        /// 工程人员手机端登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EngineerUserLoginOutput> LoginForMobile(LoginInput input)
        {
            var user = await _repEngineerUser.FirstOrDefaultAsync(item => item.Account.Equals(input.Account) && item.Password.Equals(input.Password));
            Valid.Exception(user == null, "账号或密码错误");

            EngineerUserLoginOutput output = new EngineerUserLoginOutput()
            {
                UserId = user.Id,
                Account = user.Account,
                Name = user.Name,
                AreaId = user.AreaId
            };

            return output;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            var user = await _repEngineerUser.FirstOrDefaultAsync(item => item.Account.Equals(input.Account) && item.Password.Equals(input.OldPassword));
            if (user == null)
            {
                output.Success = false;
                output.FailCause = "原密码不正确";
            }
            else
            {
                user.Password = input.NewPassword;
                await _repEngineerUser.UpdateAsync(user);
            }
            return output;
        }
    }
}
