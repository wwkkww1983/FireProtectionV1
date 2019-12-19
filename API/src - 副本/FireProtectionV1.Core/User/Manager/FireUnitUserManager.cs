using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.MiniFireStationCore.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public class FireUnitUserManager : DomainService, IFireUnitUserManager
    {
        IRepository<MiniFireStation> _repMiniFireStation;
        IRepository<FireUnitUser> _fireUnitAccountRepository;
        IRepository<FireUnitUserRole> _fireUnitAccountRoleRepository;
        IRepository<FireUnit> _fireUnitRepository;
        ISqlRepository _SqlRepository;
        IRepository<FireUntiSystem> _fireUnitSystemRep;

        public FireUnitUserManager(
            IRepository<MiniFireStation> repMiniFireStation,
            IRepository<FireUntiSystem> fireUnitSystemRep,
            IRepository<FireUnitUser> fireUnitAccountRepository,
            IRepository<FireUnitUserRole> fireUnitAccountRoleRepository,
            IRepository<FireUnit> fireUnitRepository,
            ISqlRepository sqlRepository
            )
        {
            _repMiniFireStation = repMiniFireStation;
            _fireUnitSystemRep = fireUnitSystemRep;
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
            int id=await _fireUnitAccountRepository.InsertAndGetIdAsync(new FireUnitUser()
            {
                Account = input.Phone,
                Name = input.UserName,
                Status = Common.Enum.NormalStatus.Enabled,
                FireUnitInfoID = fireunit.Id,
                Password = md5
            });
            FireUnitUserRole role = new FireUnitUserRole()
            {
                AccountID = id,
                Role = FireUnitRole.FireUnitManager
            };
            await _fireUnitAccountRoleRepository.InsertAsync(role);
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 添加防火单位账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(FireUnitUserInput input)
        {
            Valid.Exception(_fireUnitAccountRepository.Count(m => m.Account.Equals(input.Account)) > 0, "手机号已被注册");
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
                output.Account = v.Account;
                output.GuideFlage = false;
                var rolllist = _fireUnitAccountRoleRepository.GetAll();
                output.Rolelist = (from a in rolllist
                                   where a.AccountID == v.Id
                                   select a.Role).ToList();

                var fireunit = await _fireUnitRepository.FirstOrDefaultAsync(p => p.Id == v.FireUnitInfoID);
                if (fireunit != null)
                {
                    output.FireUnitName = fireunit.Name;
                    output.FireUnitID = fireunit.Id;
                    var mini = await _repMiniFireStation.FirstOrDefaultAsync(p => p.FireUnitId == fireunit.Id);
                    if (mini != null)
                    {
                        output.MiniFireStationId = mini.Id;
                        output.MiniFireStationName = mini.Name;
                    }
                }
                if (output.Rolelist.Contains(FireUnitRole.FireUnitManager))
                {
                    output.GuideFlage = fireunit.Patrol == 0;
                    //因为引导是分步调用API，这里判断是否引导完成，也加条件判断
                    if (!output.GuideFlage)
                    {
                        output.GuideFlage = 0 == _fireUnitSystemRep.GetAll().Where(u => u.FireUnitId == v.FireUnitInfoID).Count();
                    }
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
                             orderby a.CreationTime descending
                             let rolelst= rolllist.Where(u => u.AccountID == a.Id).Select(u => u.Role).ToList()
                             select new GetUnitPeopleOutput
                             {
                                 ID = a.Id,
                                 Name = a.Name,
                                 Account = a.Account,
                                 Rolelist = FireUnitRoleFunc.GetListName( rolelst),//rolllist.Where(u => u.AccountID == a.Id).Select(u => u.Role).ToList(),
                                 Photo=a.Photo,
                                 Qualification=a.Qualification,
                                 QualificationNumber=a.QualificationNumber,
                                 QualificationValidity=a.QualificationValidity.ToString("yyyy-MM-dd")
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
            var rolllist = _fireUnitAccountRoleRepository.GetAll().Where(u => u.AccountID == input.AccountID).Select(u => u.Role).ToList();
            GetUnitPeopleOutput userInfo = new GetUnitPeopleOutput()
            {
                ID = loginman.Id,
                Name = loginman.Name,
                Account = loginman.Account,
                Rolelist = FireUnitRoleFunc.GetListName( rolllist),
                Photo = loginman.Photo,
                Qualification = loginman.Qualification,
                QualificationNumber = loginman.QualificationNumber,
                QualificationValidity = loginman.QualificationValidity.ToString("yyyy-MM-dd")
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
            userInfo.Photo = input.Photo;
            userInfo.Qualification = input.Qualification;
            userInfo.QualificationNumber = input.QualificationNumber;
            userInfo.QualificationValidity = DateTime.Parse( input.QualificationValidity);
            _fireUnitAccountRepository.Update(userInfo);

            string sql = $@"DELETE FROM fireunituserrole WHERE AccountID={input.ID}";
            var dataTable = _SqlRepository.Query(sql);
            if (input.Rolelist != null)
            {
                foreach (var a in input.Rolelist)
                {
                    FireUnitUserRole userRole = new FireUnitUserRole()
                    {
                        AccountID = input.ID,
                        Role = FireUnitRoleFunc.GetRoleEnum(a)
                    };
                    _fireUnitAccountRoleRepository.Insert(userRole);
                }
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
            Valid.Exception(_fireUnitAccountRepository.Count(m => m.Account.Equals(input.Account)) > 0, "手机号已被注册");
            FireUnitUser user = new FireUnitUser()
            {
                Name = input.Name,
                Account=input.Account,
                FireUnitInfoID = input.FireUnitInfoID,
                Photo = input.Photo,
                Qualification = input.Qualification,
                QualificationNumber = input.QualificationNumber,
                QualificationValidity = DateTime.Parse( input.QualificationValidity),
                Password = MD5Encrypt.Encrypt("666666" + input.Account, 16),
            };
            var userid = _fireUnitAccountRepository.InsertAndGetId(user);
            if (input.Rolelist != null)
            {
                foreach (var roleid in input.Rolelist)
                {
                    FireUnitUserRole role = new FireUnitUserRole()
                    {
                        AccountID = userid,
                        Role = FireUnitRoleFunc.GetRoleEnum(roleid)
                    };
                    await _fireUnitAccountRoleRepository.InsertAsync(role);
                }
            }
            return output;
        }

        /// <summary>
        /// 删除工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteUser(DeleteUserInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            try {
                
                await _fireUnitAccountRepository.DeleteAsync(u => u.Id == input.UserId);
            }
            catch(Exception e)
            {
                output.FailCause = e.Message;
                output.Success = false;
            }
            
            return output;
        }

        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            string md5 = MD5Encrypt.Encrypt(input.OldPassword + input.Account, 16);
            SuccessOutput output = new SuccessOutput() { Success = true };
            var v = await _fireUnitAccountRepository.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "当前密码不正确";
            }
            else
            {
                string newMd5 = MD5Encrypt.Encrypt(input.NewPassword + input.Account, 16);
                v.Password = newMd5;
                var x = await _fireUnitAccountRepository.UpdateAsync(v);
                output.Success = true;
            }
            return output;
        }
    }
}
