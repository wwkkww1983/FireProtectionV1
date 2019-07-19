using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public class HydrantUserManager : DomainService, IHydrantUserManager
    {
        IRepository<HydrantUser> _hydrantUserRepository;
        IRepository<FireUnitUserRole> _fireUnitAccountRoleRepository;
        IRepository<FireUnit> _fireUnitRepository;
        IRepository<Area> _area;
        IRepository<HydrantUserArea> _hydrantUserArea;
        ISqlRepository _SqlRepository;

        public HydrantUserManager(
            IRepository<HydrantUser> hydrantUserRepository,
            IRepository<FireUnitUserRole> fireUnitAccountRoleRepository,
            IRepository<FireUnit> fireUnitRepository,
            IRepository<Area> area,
            IRepository<HydrantUserArea> hydrantUserArea,
            ISqlRepository sqlRepository
            )
        {
            _hydrantUserRepository = hydrantUserRepository;
            _fireUnitAccountRoleRepository = fireUnitAccountRoleRepository;
            _fireUnitRepository = fireUnitRepository;
            _hydrantUserArea = hydrantUserArea;
            _area = area;
            _SqlRepository = sqlRepository;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UserRegist(GetHydrantUserRegistInput input)
        {
            var user= _hydrantUserRepository.GetAll().Where(p => p.Account.Equals(input.Phone)).FirstOrDefault();
            if (user != null)
                return new SuccessOutput() { Success = false, FailCause = "手机号已被注册" };
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Phone, 16);
            await _hydrantUserRepository.InsertAsync(new HydrantUser()
            {
                Account = input.Phone,
                Name = input.UserName,
                GuideFlage = true,
                Password = md5
            });
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PutHydrantUserLoginOutput> UserLogin(LoginInput input)
        {
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            PutHydrantUserLoginOutput output = new PutHydrantUserLoginOutput() { Success = true };
            var v = await _hydrantUserRepository.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
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
                output.GuideFlage = v.GuideFlage;               
            }
            return output;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(DeptChangePassword input)
        {
            string md5 = MD5Encrypt.Encrypt(input.OldPassword + input.Account, 16);
            SuccessOutput output = new SuccessOutput() { Success = true };
            var v = await _hydrantUserRepository.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "当前密码不正确";
            }
            else
            {
                string newMd5 = MD5Encrypt.Encrypt(input.NewPassword + input.Account, 16);
                v.Password = newMd5;
                var x = await _hydrantUserRepository.UpdateAsync(v);
                output.Success = true;
            }
            return output;
        }

        /// <summary>
        /// 获取已有管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetHyrantAreaOutput>> GetUserArea(GetUserAreaInput input)
        {
            var userArealist = _hydrantUserArea.GetAll().Where(u=>u.AccountID==input.UserID);
            var user = _hydrantUserRepository.Single(u=>u.Id==input.UserID);
            var arealist = _area.GetAll().Where(u => u.ParentId == 32949);
            var output = (from a in userArealist
                          join b in arealist on a.AreaID equals b.Id
                          select new GetHyrantAreaOutput
                          {
                              AreaID = b.Id,
                              AreaName = b.Name
                          }).ToList();
            if(user.GuideFlage==false)
            {
                user.GuideFlage = true;
                _hydrantUserRepository.Update(user);
            }
            return Task.FromResult(output);
        }

        /// <summary>
        /// 获取未拥有管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetHyrantAreaOutput>> GetArea(GetUserAreaInput input)
        {
            var userArealist = _hydrantUserArea.GetAll().Where(u => u.AccountID == input.UserID);
            var arealist = _area.GetAll().Where(u=>u.ParentId== 32949);
            var removelist = from a in userArealist
                             join b in arealist on a.AreaID equals b.Id
                             select b;
            var arealistexc = arealist.Except(removelist).ToList();
            var output = from a in arealistexc
                         select new GetHyrantAreaOutput
                         {
                             AreaID = a.Id,
                             AreaName = a.Name
                         };
            var c = output.ToList();
            return Task.FromResult(c);
        }
        /// <summary>
        /// 修改用户管辖区
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> PutUserArea(PutUserAreaInput input)
        {
            SuccessOutput output=new SuccessOutput(){ Success=false};
            if (input.Operation == Operation.add)
            {
                foreach(var a in input.arealist)
                {
                    HydrantUserArea area = new HydrantUserArea()
                    {
                        AccountID = input.UserID,
                        AreaID = a.AreaID
                    };
                    await _hydrantUserArea.InsertAsync(area);
                }
                output.Success = true;
            }
            else if(input.Operation == Operation.delete)
            {
                foreach (var a in input.arealist)
                {
                    HydrantUserArea area = new HydrantUserArea()
                    {
                        AccountID = input.UserID,
                        AreaID = a.AreaID
                    };
                    await _hydrantUserArea.DeleteAsync(area);
                }
                output.Success = true;
            }
            return output;
        }
    }
}
