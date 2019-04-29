using Abp.Domain.Services;
using FireProtectionV1.Account.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Account.Manager
{
    public interface IFireDeptUserManager : IDomainService
    {
        Task<UserLoginOutput> UserLogin(UserLoginInput input);
        Task<UserRegistOutput> UserRegist(UserRegistInput input);
    }
}
