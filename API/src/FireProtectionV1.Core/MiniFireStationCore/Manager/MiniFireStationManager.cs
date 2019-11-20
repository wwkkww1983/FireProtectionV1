using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.MiniFireStationCore.Dto;
using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.MiniFireStationCore.Manager
{
    public class MiniFireStationManager : DomainService, IMiniFireStationManager
    {
        IRepository<MiniFireStationJobUser> _repMiniFireStationJobUser;
        IRepository<FireUnit> _repFireUnit;
        IRepository<MiniFireStation> _miniFireStationRepository;
        ISqlRepository _SqlRepository;
        ISqlExecuter _sqlExecuter;
        IRepository<MiniFireEquipment> _repMiniFireEquipment;
        IRepository<MiniFireEquipmentDefine> _repMiniFireEquipmentDefine;
        IRepository<MiniFireAction> _repMiniFireAction;
        IRepository<MiniFireActionType> _repMiniFireActionType;

        public MiniFireStationManager(
            IRepository<MiniFireAction> repMiniFireAction,
            IRepository<MiniFireActionType> repMiniFireActionType,
            IRepository<MiniFireEquipment> repMiniFireEquipment,
            IRepository<MiniFireEquipmentDefine> repMiniFireEquipmentDefine,
            IRepository<MiniFireStationJobUser> repMiniFireStationJobUser,
            IRepository<FireUnit> repFireUnit,
            IRepository<MiniFireStation> miniFireStationRepository, ISqlRepository sqlRepository, ISqlExecuter sqlExecuter)
        {
            _repMiniFireAction = repMiniFireAction;
            _repMiniFireActionType = repMiniFireActionType;
            _repMiniFireEquipment = repMiniFireEquipment;
            _repMiniFireEquipmentDefine = repMiniFireEquipmentDefine;
            _repMiniFireStationJobUser = repMiniFireStationJobUser;
            _repFireUnit = repFireUnit;
            _miniFireStationRepository = miniFireStationRepository;
            _SqlRepository = sqlRepository;
            _sqlExecuter = sqlExecuter;
        }
        public async Task<SuccessOutput> AddMiniFireAction(MiniFireActionAddDto input)
        {
            var type = await _repMiniFireActionType.FirstOrDefaultAsync(p => p.Type.Equals(input.Type));
            if (type == null)
                return new SuccessOutput() { Success = false, FailCause = "活动类别不存在" };
            try
            {
                var actionid=await _repMiniFireAction.InsertAndGetIdAsync(new MiniFireAction()
                {
                    MiniFireStationId = input.MiniFireStationId,
                    Address = input.Address,
                    MiniFireActionTypeId = type.Id,
                    Content=input.Content,
                    CreationTime=DateTime.Parse(input.Date),
                    Problem=input.Problem,
                    AttendUser=input.AttendUser
                });
            }catch(Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> DeleteMiniFireAction(int MiniFireActionId)
        {
            if(await _repMiniFireAction.FirstOrDefaultAsync(MiniFireActionId)==null)
                return new SuccessOutput() { Success = false,FailCause="指定ID的活动不存在" };
            await _repMiniFireAction.DeleteAsync(MiniFireActionId);
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> UpdateMiniFireAction(MiniFireActionDetailDto input)
        {
            var action = await _repMiniFireAction.FirstOrDefaultAsync(p => p.Id == input.MiniFireActionId);
            if (action == null)
                return new SuccessOutput() { Success = false, FailCause = "指定ID的活动不存在" };
            action.Address = input.Address;
            var type = await _repMiniFireActionType.FirstOrDefaultAsync(p => p.Type.Equals(input.Type));
            if(type==null)
                return new SuccessOutput() { Success = false, FailCause = "指定活动类别不存在" };
            action.MiniFireActionTypeId = type.Id;
            action.CreationTime = DateTime.Parse(input.Date);
            action.Content = input.Content;
            action.Problem = input.Problem;
            action.AttendUser = input.AttendUser;
            await _repMiniFireAction.UpdateAsync(action);
            return new SuccessOutput() { Success = true };
        }
        public async Task<MiniFireActionDetailDto> GetMiniFireAction(int MiniFireActionId)
        {
            var action = await _repMiniFireAction.GetAsync(MiniFireActionId);
            var type = await _repMiniFireActionType.GetAsync(action.MiniFireActionTypeId);
            var detail= new MiniFireActionDetailDto()
            {
                Address = action.Address,
                Content = action.Content,
                Problem = action.Problem,
                Date = action.CreationTime.ToString("yyyy-MM-dd"),
                MiniFireActionId = action.Id,
                Type = type.Type,
                AttendUser=action.AttendUser
            };
            return detail;
        }
        public async Task<PagedResultDto<MiniFireActionDto>> GetActionList(GetMiniFireListBaseInput input)
        {
            var query = from a in _repMiniFireAction.GetAll().Where(p => p.MiniFireStationId == input.MiniFireStationId)
                        join b in _repMiniFireActionType.GetAll()
                        on a.MiniFireActionTypeId equals b.Id
                        orderby a.CreationTime descending
                        select new MiniFireActionDto()
                        {
                            Date=a.CreationTime.ToString("yyyy-MM-dd"),
                            Address = a.Address,
                            Type = b.Type,
                            MiniFireActionId=a.Id
                        };
            return await Task.Run<PagedResultDto<MiniFireActionDto>>(() =>
            {
                return new PagedResultDto<MiniFireActionDto>()
                {
                    Items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList(),
                    TotalCount = query.Count()
                };
            });
        }
        public async Task<List<string>> GetMiniFireEquipmentTypes()
        {
            return await Task.Run<List<string>>(() =>
            {
                return _repMiniFireEquipmentDefine.GetAll().GroupBy(p => p.Type).Select(p => p.Key).OrderBy(p=>p).ToList();
            });
        }
        public async Task<List<MiniFireEquipmentNameOutput>> GetMiniFireEquipmentsByType(string Type)
        {
            return await Task.Run<List<MiniFireEquipmentNameOutput>>(() =>
            {
                return _repMiniFireEquipmentDefine.GetAll().Where(p => p.Type.Equals(Type)).Select(p => new MiniFireEquipmentNameOutput()
                {
                    EquipmentId = p.Id,
                    Name = p.Name
                }).OrderBy(p => p.Name).ToList();
            });
        }
        public async Task<MiniFireEquipmentDto> GetMiniFireEquipment(int MiniFireEquipmentId)
        {
            var equip = await _repMiniFireEquipment.GetAsync(MiniFireEquipmentId);
            var type = await _repMiniFireEquipmentDefine.GetAsync(equip.DefineId);
            return new MiniFireEquipmentDto()
            {
                MiniFireEquipmentId = equip.Id,
                Count = equip.Count,
                Name = type.Name,
                Type = type.Type
            };
        }
        public async Task<PagedResultDto<MiniFireEquipmentDto>> GetMiniFireEquipmentList(GetMiniFireListBaseInput input)
        {
            var query = from a in _repMiniFireEquipment.GetAll().Where(p => p.MiniFireStationId == input.MiniFireStationId)
                        join b in _repMiniFireEquipmentDefine.GetAll()
                        on a.DefineId equals b.Id
                        orderby b.Type
                        select new MiniFireEquipmentDto()
                        {
                            MiniFireEquipmentId=a.Id,
                            Type = b.Type,
                            Name = b.Name,
                            Count=a.Count
                        };
            return await Task.Run<PagedResultDto<MiniFireEquipmentDto>>(() =>
            {
                return new PagedResultDto<MiniFireEquipmentDto>()
                {
                    Items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList(),
                    TotalCount = query.Count()
                };
            });
        }
        public async Task<SuccessOutput> AddMiniFireEquipment(AddMiniFireEquipmentDto input)
        {
            var def = await _repMiniFireEquipmentDefine.FirstOrDefaultAsync(p => p.Type.Equals(input.Type) && p.Name.Equals(input.Name));
            if (def == null)
                return new SuccessOutput() { Success = false, FailCause = "消防设施类别或名称不正确" };
            await _repMiniFireEquipment.InsertAsync(new MiniFireEquipment()
            {
                MiniFireStationId = input.MiniFireStationId,
                DefineId = def.Id,
                Count=input.Count
            });
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> UpdateMiniFireEquipment(MiniFireEquipmentDto input)
        {
            var equip = await _repMiniFireEquipment.FirstOrDefaultAsync(p => p.Id == input.MiniFireEquipmentId);
            if (equip == null)
                return new SuccessOutput() { Success = false, FailCause = "指定消防设施ID不存在" };
            var def = await _repMiniFireEquipmentDefine.FirstOrDefaultAsync(p => p.Type.Equals(input.Type) && p.Name.Equals(input.Name));
            if(def==null)
                return new SuccessOutput() { Success = false, FailCause = "消防设施类别或名称不正确" };
            equip.DefineId = def.Id;
            equip.Count = input.Count;
            await _repMiniFireEquipment.UpdateAsync(equip);
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> DeleteMiniFireEquipment(int MiniFireEquipmentId)
        {
            await _repMiniFireEquipment.DeleteAsync(MiniFireEquipmentId);
            return new SuccessOutput() { Success = true };
        }
        public async Task<MiniFireJobUserDetailDto> GetJobUserDetail(int JobUserId)
        {
            var user=await _repMiniFireStationJobUser.GetAsync(JobUserId);
            return new MiniFireJobUserDetailDto()
            {
                ContactName = user.ContactName,
                ContactPhone = user.ContactPhone,
                Job = user.Job,
                JobUserId = user.Id,
                PhotoBase64 = user.HeadPhotoBase64
            };
        }
        public async Task<SuccessOutput> DeleteJobUser(int JobUserId)
        {
            try
            {
                await _repMiniFireStationJobUser.DeleteAsync(JobUserId);
            }catch(Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> UpdateJobUser(MiniFireJobUserDetailDto input)
        {
            var jobuser = await _repMiniFireStationJobUser.FirstOrDefaultAsync(p => p.Id == input.JobUserId);
            if (jobuser != null)
            {
                if (jobuser.Job.Equals("站长"))
                {
                    if(!jobuser.ContactName.Equals(input.ContactName)||
                        !jobuser.ContactPhone.Equals(input.ContactPhone)||
                        !jobuser.Job.Equals(input.Job))
                        return new SuccessOutput() { Success = false, FailCause = "此功能只能修改站长照片" };
                }
                jobuser.ContactName = input.ContactName;
                jobuser.ContactPhone = input.ContactPhone;
                jobuser.Job = input.Job;
                jobuser.HeadPhotoBase64 = input.PhotoBase64;
                await _repMiniFireStationJobUser.UpdateAsync(jobuser);
                return new SuccessOutput() { Success = true };
            }
            return new SuccessOutput() { Success = false, FailCause = "指定用户ID不存在" };
        }
        public async Task<SuccessOutput> AddJobUser(AddMiniFireJobUserDto input)
        {
            if(input.Job.Equals("站长"))
                return new SuccessOutput() { Success = false, FailCause = "此功能不能添加站长" };
            var jobuser =await _repMiniFireStationJobUser.FirstOrDefaultAsync(p =>p.ContactName.Equals(input.ContactName)&&
            p.Job.Equals(input.Job)&& p.ContactPhone.Equals(input.ContactPhone) && p.MiniFireStationId == input.MiniFireStationId);
            if (jobuser != null)
            {
                return new SuccessOutput() { Success = false, FailCause = "相同职位联系人已经存在" };
            }
            await _repMiniFireStationJobUser.InsertAsync(new MiniFireStationJobUser()
            {
                MiniFireStationId = input.MiniFireStationId,
                ContactName = input.ContactName,
                ContactPhone = input.ContactPhone,
                Job = input.Job,
                HeadPhotoBase64=input.PhotoBase64
            });
            return new SuccessOutput() { Success = true };
        }
        public async Task<PagedResultDto<MiniFireJobUserDto>> GetMiniFireJobUser(GetMiniFireJobUserInput input)
        {
            var users = new List<MiniFireJobUserDto>();
            var station=await _miniFireStationRepository.FirstOrDefaultAsync(p => p.Id == input.MiniFireStationId);
            if (station == null)
                return new PagedResultDto<MiniFireJobUserDto>()
                {
                    Items = users,
                    TotalCount = 0
                };

            var jobs = _repMiniFireStationJobUser.GetAll().Where(p => p.MiniFireStationId == input.MiniFireStationId);
            foreach(var job in jobs)
            {
                users.Add(new MiniFireJobUserDto()
                {
                    JobUserId = job.Id,
                    ContactName = job.ContactName,
                    ContactPhone = job.ContactPhone,
                    Job = job.Job
                });
            }
            return new PagedResultDto<MiniFireJobUserDto>() {
                Items =users.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(p=>p.JobUserId).ToList(),
                TotalCount=users.Count()
            };
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Add(AddMiniFireStationInput input)
        {
            if (await _miniFireStationRepository.FirstOrDefaultAsync(p => p.Name.Equals(input.Name)) != null)
                return new SuccessOutput() { Success = false, FailCause = "保存失败：站点名称已存在" };
            if (await _miniFireStationRepository.FirstOrDefaultAsync(p => p.ContactPhone.Equals(input.ContactPhone)) != null)
                return new SuccessOutput() { Success = false, FailCause = "保存失败：手机号已存在" };
            //Valid.Exception(_miniFireStationRepository.Count(m => input.Name.Equals(m.Name)) > 0, "保存失败：站点名称已存在");
            //Valid.Exception(_miniFireStationRepository.Count(m => input.ContactPhone.Equals(m.ContactPhone)) > 0, "保存失败：手机号已存在");

            try
            {
            var unit = await _repFireUnit.FirstOrDefaultAsync(p => p.Name == input.FireUnitName);
            var entity = new MiniFireStation()
            {
                Address = input.Address,
                ContactName = input.ContactName,
                ContactPhone = input.ContactPhone,
                FireUnitId = unit != null ? unit.Id : 0,
                Lat = input.Lat,
                Level = input.Level,
                Lng = input.Lng,
                Name = input.Name,
                PersonNum = input.PersonNum,
            };

            //var entity = input.MapTo<MiniFireStation>();
            var stationid= await _miniFireStationRepository.InsertAndGetIdAsync(entity);
            entity.StationUserId = await _repMiniFireStationJobUser.InsertAndGetIdAsync(new MiniFireStationJobUser()
            {
                MiniFireStationId = stationid,
                ContactName = input.ContactName,
                ContactPhone = input.ContactPhone,
                Job = "站长"
            });
            await _miniFireStationRepository.UpdateAsync(entity);
            }catch(Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Delete(DeletMiniFireStationInput input)
        {
            await _miniFireStationRepository.DeleteAsync(input.Id);
            await _repMiniFireStationJobUser.DeleteAsync(p => p.MiniFireStationId == input.Id);
            return new SuccessOutput() { Success = true };
        }

        /// <summary>
        /// 获取单个微型消防站信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MiniFireStationOutput> GetById(int id)
        {
            var a= await _miniFireStationRepository.GetAsync(id);
            var b = await _repFireUnit.FirstOrDefaultAsync(p => p.Id == a.FireUnitId);
            return new MiniFireStationOutput()
            {
                Id = a.Id,
                Address = a.Address,
                ContactName = a.ContactName,
                ContactPhone = a.ContactPhone,
                FireUnitId = a.FireUnitId,
                FireUnitName = b == null ? "" : b.Name,
                Lat = a.Lat,
                Level = a.Level,
                Lng = a.Lng,
                Name = a.Name,
                PersonNum = a.PersonNum
            };
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<MiniFireStationOutput>> GetList(GetMiniFireStationListInput input)
        {
            var miniFireStations = _miniFireStationRepository.GetAll();

            var expr = ExprExtension.True<MiniFireStation>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            miniFireStations = miniFireStations.Where(expr);

            var query = from a in miniFireStations
                       join b in _repFireUnit.GetAll()
                       on a.FireUnitId equals b.Id into b0
                       from b2 in b0.DefaultIfEmpty()
                       orderby a.CreationTime descending
                       select new MiniFireStationOutput()
                       {
                           Id=a.Id,
                           Address = a.Address,
                           ContactName = a.ContactName,
                           ContactPhone = a.ContactPhone,
                           FireUnitId = a.FireUnitId,
                           FireUnitName = b2 == null ? "" : b2.Name,
                           Level = a.Level,
                           Lat = a.Lat,
                           Lng = a.Lng,
                           Name = a.Name,
                           PersonNum = a.PersonNum
                       };
            var list = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var tCount = miniFireStations.Count();

            return Task.FromResult(new PagedResultDto<MiniFireStationOutput>(tCount, list));
        }

        /// <summary>
        /// 微型消防站Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<MiniFireStation>> GetStationExcel(GetMiniFireStationListInput input)
        {
            var miniFireStations = _miniFireStationRepository.GetAll();

            var expr = ExprExtension.True<MiniFireStation>()
             .IfAnd(!string.IsNullOrEmpty(input.Name), item => item.Name.Contains(input.Name));

            miniFireStations = miniFireStations.Where(expr);

            List<MiniFireStation> list = miniFireStations
                .OrderByDescending(item => item.CreationTime)
                .ToList();

            return Task.FromResult<List<MiniFireStation>>(list);
        }

        /// <summary>
        /// 根据坐标点获取附近1KM直线距离内的微型消防站
        /// </summary>
        /// <param name="lng">经度，例如104.159203</param>
        /// <param name="lat">纬度，例如30.633145</param>
        /// <returns></returns>
        public Task<List<GetNearbyStationOutput>> GetNearbyStation(decimal lng, decimal lat)
        {
            // 6378.138是地球赤道的半径，单位千米
            string sql = $@"SELECT * FROM (SELECT *, ROUND(6378.138 * 2 * ASIN(SQRT(POW(SIN(({lat} * PI() / 180 - Lat * PI() / 180) / 2), 2) + COS({lat} * PI() / 180) * COS(Lat * PI() / 180) * 
POW(SIN(({lng} * PI() / 180 - Lng * PI() / 180) / 2), 2))) *1000) AS Distance FROM MiniFireStation WHERE Lat !=0 and Lng != 0) a WHERE Distance <= 1000 ORDER BY Distance ASC";

            var dataTable = _SqlRepository.Query(sql);
            return Task.FromResult(_SqlRepository.DataTableToList<GetNearbyStationOutput>(dataTable));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> Update(UpdateMiniFireStationInput input)
        {
            if (_miniFireStationRepository.Count(m => input.Name.Equals(m.Name)) > 1)
                return new SuccessOutput() { Success = false, FailCause = "保存失败：站点名称重复" };
            if(_miniFireStationRepository.Count(m => input.ContactPhone.Equals(m.ContactPhone)) > 1)
                return new SuccessOutput() { Success = false, FailCause = "保存失败：站长手机号重复" };
            //Valid.Exception(_miniFireStationRepository.Count(m => input.Name.Equals(m.Name) && !input.Id.Equals(m.Id)) > 0, "保存失败：站点名称已存在");
            try
            {
                var unit = await _repFireUnit.FirstOrDefaultAsync(p => p.Name == input.FireUnitName);
                var old = _miniFireStationRepository.GetAll().Where(u => u.Id == input.Id).FirstOrDefault();
                old.Name = input.Name;
                old.FireUnitId = unit != null ? unit.Id : 0;
                old.Level = input.Level;
                old.ContactName = input.ContactName;
                old.ContactPhone = input.ContactPhone;
                old.PersonNum = input.PersonNum;
                old.Address = input.Address;
                old.Lng = input.Lng;
                old.Lat = input.Lat;
                await _miniFireStationRepository.UpdateAsync(old);
                var jobuser = await _repMiniFireStationJobUser.FirstOrDefaultAsync(p => p.MiniFireStationId == input.Id && p.Job.Equals("站长"));
                if(jobuser==null)
                {
                    await _repMiniFireStationJobUser.InsertAsync(new MiniFireStationJobUser()
                    {
                        MiniFireStationId = input.Id,
                        ContactName = input.ContactName,
                        ContactPhone = input.ContactPhone,
                        Job = "站长"
                    });
                }
                else
                {
                    jobuser.ContactName = input.ContactName;
                    jobuser.ContactPhone = input.ContactPhone;
                    await _repMiniFireStationJobUser.UpdateAsync(jobuser);
                }
            } catch(Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }
            return new SuccessOutput() { Success = true };
        }
    }
}
