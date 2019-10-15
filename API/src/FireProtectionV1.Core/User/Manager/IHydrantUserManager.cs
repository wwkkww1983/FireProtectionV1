using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public interface IHydrantUserManager : IDomainService
    {
        /// <summary>
        /// 添加消火栓管理账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> UserRegist(GetHydrantUserRegistInput input);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PutHydrantUserLoginOutput> UserLogin(LoginInput input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> ChangePassword(ChangeUserPassword input);

        /// <summary>
        /// 获取已有管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetHyrantAreaOutput>> GetUserArea(GetUserAreaInput input);

        /// <summary>
        /// 获取已有管辖区ForPC
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetHyrantAreaForPCOutput> GetUserAreaForPC(GetUserAreaInput input);

        /// <summary>
        /// 获取未拥有管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<GetHyrantAreaOutput>> GetArea(GetUserAreaInput input);

        /// <summary>
        /// 修改用户管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SuccessOutput> PutUserArea(PutUserAreaInput input);
    }
}
