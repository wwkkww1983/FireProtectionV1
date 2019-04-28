using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Account.Dto;
using FireProtectionV1.Account.Model;
using System.Threading.Tasks;

namespace FireProtectionV1.Account.Manager
{
    public class FireUnitAccountManager : DomainService, IFireUnitAccountManager
    {
        IRepository<FireUnitAccount> _fireUnitAccountRepository;
        IRepository<FireUnitAccountRole> _fireUnitAccountRoleRepository;

        public FireUnitAccountManager(
            IRepository<FireUnitAccount> fireUnitAccountRepository,
            IRepository<FireUnitAccountRole> fireUnitAccountRoleRepository
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
        public async Task<int> Add(FireUnitAccountInput input)
        {
            //AccountInfo account = new AccountInfo()
            //{
            //    Account = input.Account,
            //    Password = input.Password,
            //    Name = input.Name
            //};
            var account = input.MapTo<FireUnitAccount>();
            int accountID = await _fireUnitAccountRepository.InsertAndGetIdAsync(account);

            var accountRole = new FireUnitAccountRole()
            {
                AccountID = accountID,
                Role = input.Role
            };
            await _fireUnitAccountRoleRepository.InsertAsync(accountRole);
            return accountID;
        }
    }
}
