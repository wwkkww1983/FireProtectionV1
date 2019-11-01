using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public class SafeUnitManager : DomainService, ISafeUnitManager
    {
        IRepository<FireUnit> _repFireUnit;
        IRepository<BreakDown> _repBreakDown;
        IRepository<SafeUnitUserFireUnit> _repSafeUnitUserFireUnit;
        IRepository<SafeUnit> _safeUnitRepository;
        IRepository<SafeUnitUser> _repSafeUnitUser;

        public SafeUnitManager(
            IRepository<FireUnit> repFireUnit,
            IRepository<SafeUnit> safeUnitRepository,
            IRepository<SafeUnitUser> repSafeUnitUser,
            IRepository<BreakDown> repBreakDown,
            IRepository<SafeUnitUserFireUnit> repSafeUnitUserFireUnit)
        {
            _repFireUnit = repFireUnit;
            _repBreakDown = repBreakDown;
            _safeUnitRepository = safeUnitRepository;
            _repSafeUnitUser = repSafeUnitUser;
            _repSafeUnitUserFireUnit = repSafeUnitUserFireUnit;
        }
        public async Task<SuccessOutput> UserRegist(SafeUnitUserRegistInput input)
        {
            var safeunits = _safeUnitRepository.GetAll().Where(p => p.InvitationCode.Equals(input.InvitatCode));
            if(safeunits.Count() == 0)
                return new SuccessOutput() { Success = false, FailCause = "邀请码不正确" };
            var safeUnit = safeunits.Where(p=>p.Name.Equals(input.SafeUnitName)).FirstOrDefault();
            if(safeUnit==null)
                return new SuccessOutput() { Success = false, FailCause = "维保单位名称不正确" };
            var user = await _repSafeUnitUser.FirstOrDefaultAsync(p => p.Account.Equals(input.Phone));
            if (user != null)
                return new SuccessOutput() { Success = false, FailCause = "手机号已被注册" };
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Phone, 16);
            await _repSafeUnitUser.InsertAsync(new SafeUnitUser()
            {
                Account = input.Phone,
                Name = input.UserName,
                SafeUnitId= safeUnit.Id,
                Password = md5
            });
            return new SuccessOutput() { Success = true };
        }
        public async Task<SafeUserLoginOutput> UserLogin(LoginInput input)
        {
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            SafeUserLoginOutput output = new SafeUserLoginOutput() { Success = true };
            var v = await _repSafeUnitUser.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "账号或密码不正确";
            }
            else
            {
                output.UserId = v.Id;
                output.Name = v.Name;

                var safeunit = await _safeUnitRepository.FirstOrDefaultAsync(p => p.Id == v.SafeUnitId);
                if (safeunit != null)
                {
                    output.SafeUnitName = safeunit.Name;
                    output.SafeUnitID = safeunit.Id;
                }
            }
            return output;
        }
        /// <summary>
        /// 邀请码验证 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> InvitatVerify(InvitatVerifyDto input)
        {
            var fireunit = _safeUnitRepository.GetAll().Where(p => p.Name.Equals(input.UnitName)).FirstOrDefault();
            SuccessOutput output = new SuccessOutput() { Success = true };
            output = await Task.Run<SuccessOutput>(() =>
            {
                if (fireunit == null)
                    return new SuccessOutput() { Success = false, FailCause = "不存在此防火单位" };
                if (!fireunit.InvitationCode.Equals(input.InvitatCode))
                    return new SuccessOutput() { Success = false, FailCause = "邀请码不正确" };
                return new SuccessOutput() { Success = true };
            });
            return output;
        }
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            string md5 = MD5Encrypt.Encrypt(input.OldPassword + input.Account, 16);
            SuccessOutput output = new SuccessOutput() { Success = true };
            var v = await _repSafeUnitUser.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "当前密码不正确";
            }
            else
            {
                string newMd5 = MD5Encrypt.Encrypt(input.NewPassword + input.Account, 16);
                v.Password = newMd5;
                var x = await _repSafeUnitUser.UpdateAsync(v);
                output.Success = true;
            }
            return output;
        }
        /// <summary>
        /// 选择查询维保单位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetSafeUnitOutput>> GetSelectSafeUnits(GetSafeUnitInput input)
        {
            var query = _safeUnitRepository.GetAll().Where(p => string.IsNullOrEmpty(input.Name) ? true : p.Name.Contains(input.Name))
                .Select(p => new GetSafeUnitOutput()
                {
                    SafeUnitId = p.Id,
                    SafeUnitName = p.Name
                }).Take(10);
            return Task.FromResult<List<GetSafeUnitOutput>>(query.ToList());
        }
        public async Task<PagedResultDto<UnitNameAndIdDto>> GetAllFireUnitOfSafe(int SafeUnitId, PagedResultRequestDto page)
        {
            var output = new PagedResultDto<UnitNameAndIdDto>();
             await Task.Run(()=>
             {
                 var query = _repFireUnit.GetAll().Where(p => p.SafeUnitId == SafeUnitId).Select(p => new UnitNameAndIdDto()
                 {
                     Name = p.Name,
                     UnitId = p.Id
                 });
                 output.Items = query.Skip(page.SkipCount).Take(page.MaxResultCount).ToList();
                 output.TotalCount = query.Count();
             });
            return output;
        }
        public async Task<PagedResultDto<FireUnitSafe>> GetSafeUnitUserEvent(int UserId, PagedResultRequestDto page)
        {
            var fireUnitSafes = from a in _repSafeUnitUserFireUnit.GetAll().Where(p => p.SafeUnitUserId == UserId)
                                join b in _repFireUnit.GetAll()
                                on a.FireUnitId equals b.Id
                                join c in _repBreakDown.GetAll().Where(p => p.HandleStatus != 3).GroupBy(p => p.FireUnitId)
                                on b.Id equals c.Key into b2
                                from d in b2.DefaultIfEmpty()
                                select new FireUnitSafe()
                                {
                                    FireUnitId = b.Id,
                                    FireUnitName = b.Name,
                                    HaveSafeEvent = d != null
                                };
            var output = new PagedResultDto<FireUnitSafe>();
            await Task.Run(() =>
            {
                output.Items = fireUnitSafes.Skip(page.SkipCount).Take(page.MaxResultCount).ToList();
                output.TotalCount = fireUnitSafes.Count();
            });
            return output;
        }
        public async Task<SuccessOutput> AddSafeUserFireUnit(SafeUserFireUnitDto dto)
        {
            var data = await _repSafeUnitUserFireUnit.FirstOrDefaultAsync(p => p.SafeUnitUserId == dto.SafeUserId && p.FireUnitId == dto.FireUnitId);
            if (data != null)
                return new SuccessOutput() { Success = false, FailCause = "已关联了防火单位" };
            await _repSafeUnitUserFireUnit.InsertAsync(new SafeUnitUserFireUnit()
            {
                SafeUnitUserId = dto.SafeUserId,
                FireUnitId = dto.FireUnitId
            });
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> DelSafeUserFireUnit(SafeUserFireUnitDto dto)
        {
            //var e =await _repSafeUnitUserFireUnit.FirstOrDefaultAsync(p => p.SafeUnitUserId == SafeUserId && p.FireUnitId == FireUnitId);
            //if(e!=null)
                await _repSafeUnitUserFireUnit.DeleteAsync(p=>p.SafeUnitUserId==dto.SafeUserId&&p.FireUnitId==dto.FireUnitId );
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(AddSafeUnitInput input)
        {
            Valid.Exception(_safeUnitRepository.Count(m => input.Name.Equals(m.Name)) > 0, "保存失败：单位名称已存在");
            
            return await _safeUnitRepository.InsertAndGetIdAsync(new SafeUnit()
            {
                CreationTime = DateTime.Now,
                Name = input.Name,
                ContractName = input.ContractName,
                ContractPhone = input.ContractPhone,
                Level = input.Level,
                InvitationCode = MethodHelper.CreateInvitationCode().Trim(),
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput>  Delete(DeletFireUnitInput input)
        {
             await _safeUnitRepository.DeleteAsync(input.Id);
            return new SuccessOutput() { Success = true };
        }

        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SafeUnit> GetById(int id)
        {
            return await _safeUnitRepository.GetAsync(id);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<SafeUnit>> GetList(GetSafeUnitListInput input)
        {
            var safeUnits = _safeUnitRepository.GetAll();

            var expr = ExprExtension.True<SafeUnit>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            safeUnits = safeUnits.Where(expr);

            List<SafeUnit> list = safeUnits
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = safeUnits.Count();

            return Task.FromResult(new PagedResultDto<SafeUnit>(tCount, list));
        }
        /// <summary>
        /// 消防维保Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<SafeUnit>> GetSafeUnitsExcel(GetSafeUnitListInput input)
        {
            var safeUnits = _safeUnitRepository.GetAll();

            var expr = ExprExtension.True<SafeUnit>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            safeUnits = safeUnits.Where(expr);

            List<SafeUnit> list = safeUnits
                .OrderByDescending(item => item.CreationTime)
                .ToList();
            var tCount = safeUnits.Count();

            return Task.FromResult<List<SafeUnit>>(list);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateSafeUnitInput input)
        {
            Valid.Exception(_safeUnitRepository.Count(m => input.Name.Equals(m.Name) && !input.Id.Equals(m.Id)) > 0, "保存失败：单位名称已存在");
            var old = _safeUnitRepository.GetAll().Where(u => u.Id == input.Id).FirstOrDefault();
            old.Name = input.Name;
            old.Level = input.Level;
            old.ContractName = input.ContractName;
            old.ContractPhone = input.ContractPhone;
            await _safeUnitRepository.UpdateAsync(old);
        }
    }
}
