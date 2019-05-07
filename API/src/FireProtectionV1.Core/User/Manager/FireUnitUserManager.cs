using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public class FireUnitUserManager : DomainService, IFireUnitUserManager
    {
        IRepository<FireUnitUser> _fireUnitAccountRepository;
        IRepository<FireUnitUserRole> _fireUnitAccountRoleRepository;

        public FireUnitUserManager(
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<FireUnitUserRole> fireUnitAccountRoleRepository
            )
        {
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _fireUnitAccountRoleRepository = fireUnitAccountRoleRepository;
        }

        /// <summary>
        /// 添加防火单位账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(FireUnitUserInput input)
        {
            //AccountInfo account = new AccountInfo()
            //{
            //    Account = input.Account,
            //    Password = input.Password,
            //    Name = input.Name
            //};
            var account = input.MapTo<FireUnitUser>();
            int accountID = await _fireUnitAccountRepository.InsertAndGetIdAsync(account);

            var accountRole = new FireUnitUserRole()
            {
                AccountID = accountID,
                Role = input.Role
            };
            await _fireUnitAccountRoleRepository.InsertAsync(accountRole);
            return accountID;
        }
    }
}
