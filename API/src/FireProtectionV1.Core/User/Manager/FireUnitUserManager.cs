using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public class FireUnitUserManager : DomainService, IFireUnitUserManager
    {
        IRepository<FireUnitUser> _fireUnitAccountRepository;
        IRepository<FireUnitUserRole> _fireUnitAccountRoleRepository;
        IRepository<FireUnit> _fireUnitRepository;
        ISqlRepository _SqlRepository;

        public FireUnitUserManager(
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<FireUnitUserRole> fireUnitAccountRoleRepository,
            IRepository<FireUnit> fireUnitRepository,
            ISqlRepository sqlRepository
            )
        {
            _fireUnitAccountRepository = fireUnitAccountRepository;
            _fireUnitAccountRoleRepository = fireUnitAccountRoleRepository;
            _fireUnitRepository = fireUnitRepository;
            _SqlRepository = sqlRepository;
        }
        public async Task<SuccessOutput> UserRegist(UserRegistInput input)
        {
            var fireunit =await _fireUnitRepository.SingleAsync(p => p.Name.Equals(input.FireUnitName) && p.InvitationCode.Equals(input.InvitatCode));
            var user=_fireUnitAccountRepository.GetAll().Where(p => p.Account.Equals(input.Phone)).FirstOrDefault();
            if (user != null)
                return new SuccessOutput() { Success = false, FailCause = "手机号已被注册" };
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Phone, 16);
            await _fireUnitAccountRepository.InsertAsync(new FireUnitUser()
            {
                Account = input.Phone,
                Name = input.UserName,
                Status = Common.Enum.NormalStatus.Enabled,
                FireUnitInfoID = fireunit.Id,
                GuideFlage = false,
                Password = md5
            });
            return new SuccessOutput() { Success = true };
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
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FireUnitUserLoginOutput> UserLogin(LoginInput input)
        {
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            FireUnitUserLoginOutput output = new FireUnitUserLoginOutput() { Success = true };
            var v = await _fireUnitAccountRepository.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "账号或密码不正确";
            }
            else
            {
                output.UserId = v.Id;
                output.Name = v.Name;
                output.GuideFlage = false;
                var rolllist = _fireUnitAccountRoleRepository.GetAll();
                output.Rolelist = (from a in rolllist
                                   where a.AccountID == v.Id
                                   select a.Role).ToList();

                if(output.Rolelist.Contains(FireUnitRole.FireUnitManager))
                {
                    var guideCount = _fireUnitAccountRepository.GetAll().Where(u => u.FireUnitInfoID == v.FireUnitInfoID && u.GuideFlage == true).Count();
                    if(guideCount>0)
                    {
                        output.GuideFlage = true;
                    }
                }

                var fireunit = await _fireUnitRepository.FirstOrDefaultAsync(p => p.Id == v.FireUnitInfoID);
                if (fireunit != null)
                {
                    output.FireUnitName = fireunit.Name;
                    output.FireUnitID = fireunit.Id;
                }
            }
            return output;
        }
        /// <summary>
        /// 获取防火单位工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetUnitPeopleOutput>> GetFireUnitPeople(GetUnitPeopleInput input)
        {
            var loginman = await _fireUnitAccountRepository.SingleAsync(u=>u.Id==input.AccountID);
            var unitpeoplelist = _fireUnitAccountRepository.GetAll();
            var rolllist = _fireUnitAccountRoleRepository.GetAll();

            var unitpeople = from a in unitpeoplelist
                             where a.FireUnitInfoID == loginman.FireUnitInfoID
                             select new GetUnitPeopleOutput
                             {
                                 ID = a.Id,
                                 Name = a.Name,
                                 Account = a.Account,
                                 Rolelist = rolllist.Where(u => u.AccountID == a.Id).Select(u => u.Role).ToList()
                             };
            return unitpeople.ToList();
                
        }
        /// <summary>
        /// 获取工作人员详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUnitPeopleOutput> GetUserInfo(GetUnitPeopleInput input)
        {
            var loginman = await _fireUnitAccountRepository.SingleAsync(u => u.Id == input.AccountID);
            var rolllist = _fireUnitAccountRoleRepository.GetAll();
            GetUnitPeopleOutput userInfo = new GetUnitPeopleOutput()
            {
                ID = loginman.Id,
                Name = loginman.Name,
                Account = loginman.Account,
                Rolelist = rolllist.Where(u => u.AccountID == input.AccountID).Select(u => u.Role).ToList()
            };
            return userInfo;

        }

        /// <summary>
        /// 编辑工作人员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateUserInfo(GetUnitPeopleOutput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            var userInfo= await _fireUnitAccountRepository.SingleAsync(u => u.Id == input.ID);
            userInfo.Account = input.Account;
            userInfo.Name = input.Name;
            _fireUnitAccountRepository.Update(userInfo);

            string sql = $@"DELETE FROM fireunituserrole WHERE AccountID={input.ID}";
            var dataTable = _SqlRepository.Query(sql);
            foreach(var a in input.Rolelist)
            {
                FireUnitUserRole userRole = new FireUnitUserRole()
                {
                    AccountID = input.ID,
                    Role = a
                };
                _fireUnitAccountRoleRepository.Insert(userRole);
            }

            return output;
        }
        /// <summary>
        /// 新增工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddUser(AddUserInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            FireUnitUser user = new FireUnitUser()
            {
                Name = input.Name,
                Account=input.Account,
                FireUnitInfoID = input.FireUnitInfoID,
                Password = MD5Encrypt.Encrypt("666666" + input.Account, 16),
                GuideFlage=true
            };
            var userid = _fireUnitAccountRepository.InsertAndGetId(user);
            foreach(var roleid in input.Rolelist)
            {
                FireUnitUserRole role = new FireUnitUserRole()
                {
                    AccountID = userid,
                    Role = roleid
                };
                await _fireUnitAccountRoleRepository.InsertAsync(role);
            }
            return output;
        }
    }
}
