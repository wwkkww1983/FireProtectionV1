using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.HydrantCore.Dto;
using FireProtectionV1.HydrantCore.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.SettingCore.Model;
using FireProtectionV1.User.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.HydrantCore.Manager
{
    public class HydrantSystemManager : DomainService, IHydrantSystemManager
    {
        IRepository<Hydrant> _hydrantRepository;
        IRepository<HydrantUser> _hydrantUserRepository;
        IRepository<HydrantUserArea> _hydrantUserArea;
        IRepository<HydrantPressure> _hydrantPressureRepository;
        IRepository<HydrantAlarm> _hydrantAlarmRepository;
        IRepository<Area> _areaRepository;
        IRepository<FireSetting> _settingRepository;
        IRepository<PhotosPathSave> _photosPathSave;
        ISqlRepository _SqlRepository;
        private IHostingEnvironment _hostingEnv;

        public HydrantSystemManager(
            IRepository<Hydrant> hydrantRepository,
            IRepository<HydrantPressure> hydrantPressureRepository,
            IRepository<HydrantAlarm> hydrantAlarmRepository,
            IRepository<HydrantUserArea> hydrantUserArea,
            IRepository<Area> areaRepository,
            IRepository<FireSetting> settingRepository,
            IRepository<PhotosPathSave> photosPathSaveRep,
            IHostingEnvironment env,
            IRepository<HydrantUser> hydrantUserRepository,
            ISqlRepository sqlRepository)
        {
            _hydrantRepository = hydrantRepository;
            _hydrantPressureRepository = hydrantPressureRepository;
            _hydrantAlarmRepository = hydrantAlarmRepository;
            _areaRepository = areaRepository;
            _hydrantUserArea = hydrantUserArea;
            _settingRepository = settingRepository;
            _photosPathSave = photosPathSaveRep;
            _SqlRepository = sqlRepository;
            _hydrantUserRepository = hydrantUserRepository;
            _hostingEnv = env;
        }

        /// <summary>
        /// 获取消火栓列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public Task<List<GetUserHydrantListOutput>> GetUserHydrant(GetUserHydrantInput input)
        {
            var userArealist = _hydrantUserArea.GetAll().Where(u => u.AccountID == input.UserID);
            var output = from a in _areaRepository.GetAll()
                         join b in userArealist on a.Id equals b.AreaID
                         join c in _hydrantRepository.GetAll() on b.AreaID equals c.AreaId
                         select new GetUserHydrantListOutput
                         {
                             ID = c.Id,
                             Sn = c.Sn,
                             Address = c.Address,
                             Status = c.Status
                         };
            return Task.FromResult(output.ToList());
        }
        /// <summary>
        /// 获取警情处理列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public Task<GetHydrantAlarmPagingOutput> GetHydrantAlarmlistForMobile(GetHydrantAlarmListInput input)
        {
            var list = (from a in _hydrantAlarmRepository.GetAll()
                        join b in _hydrantRepository.GetAll() on a.HydrantId equals b.Id
                        join c in _hydrantUserArea.GetAll().Where(u => u.AccountID == input.UserID) on b.AreaId equals c.AreaID
                        where a.HandleStatus == (byte)input.HandleStatus
                        select new GetHydrantAlarmOutput
                        {
                            AlarmId = a.Id,
                            CreateTime = a.CreationTime.ToString("yyyy-MM-dd hh:mm"),
                            Address = b.Address,
                            Title = a.Title,
                            ReadFlag = a.ReadFlag
                        }).OrderBy(u => u.ReadFlag).ThenByDescending(u => u.CreateTime);
            GetHydrantAlarmPagingOutput output = new GetHydrantAlarmPagingOutput()
            {
                TotalCount = list.Count(),
                AlarmList = list.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
            };
            return Task.FromResult(output);

        }
        /// <summary>
        /// 获取警情处理详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<GetHydrantAlarmInfoOutput> GetHydrantAlarmInfo(GetHydrantAlarmInfoInput input)
        {
            var alarm = await _hydrantAlarmRepository.SingleAsync(u => u.Id == input.AlarmID);
            var hyrant = await _hydrantRepository.FirstOrDefaultAsync(u => u.Id == alarm.HydrantId);
            var user = await _hydrantUserRepository.FirstOrDefaultAsync(u => u.Name == alarm.HandleUser);
            GetHydrantAlarmInfoOutput output = new GetHydrantAlarmInfoOutput();
            output.AlarmID = alarm.Id;
            output.Title = alarm.Title;
            output.SolutionTime = alarm.SoultionTime.ToString("yyyy-MM-dd hh:mm");
            output.HandleUser = alarm.HandleUser;
            output.ProblemRemarkType = (ProblemType)alarm.ProblemRemarkType;
            output.ProblemRemark = alarm.ProblemRemark;
            output.Sn = hyrant.Sn;
            output.Adress = hyrant.Address;
            output.Lng = hyrant.Lng;
            output.Lat = hyrant.Lat;
            output.Phone = user==null?null:user.Account;
            output.PhtosPath = _photosPathSave.GetAll().Where(u => u.TableName.Equals("HydrantAlarm") && u.DataId == alarm.Id).Select(u => u.PhotoPath).ToList();

            return output;
        }
    



        /// <summary>
        /// 警情处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task<SuccessOutput> UpdtateHydrantAlarm(UpdateHydrantAlarmInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            try
            {
                SaveFilesHelper saveFilesHelper = new SaveFilesHelper(_photosPathSave);
                var alarm = await _hydrantAlarmRepository.SingleAsync(u => u.Id == input.AlarmId);
                alarm.HandleStatus = (byte)input.HandleStatus;
                alarm.ProblemRemarkType = (byte)input.ProblemRemarkType;
                alarm.SoultionTime = DateTime.Now;
                alarm.HandleUser = input.UserName;

                string tableName = "HydrantAlarm";
                string path = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Photos/HydrantAlarm/";
                if (input.Picture1 != null)
                    saveFilesHelper.SavePhotosPath(tableName, input.AlarmId, await saveFilesHelper.SaveFiles(input.Picture1, path));
                if (input.Picture2 != null)
                    saveFilesHelper.SavePhotosPath(tableName, input.AlarmId, await saveFilesHelper.SaveFiles(input.Picture2, path));
                if (input.Picture3 != null)
                    saveFilesHelper.SavePhotosPath(tableName, input.AlarmId, await saveFilesHelper.SaveFiles(input.Picture3, path));

                string voicepath = _hostingEnv.ContentRootPath + $@"/App_Data/Files/Voices/HydrantAlarm/";
                if ((int)input.ProblemRemarkType == 1)
                {
                    alarm.ProblemRemark = alarm.ProblemRemark;
                }
                else if ((int)input.ProblemRemarkType == 2 && input.VoiceFile != null)
                {
                    alarm.ProblemRemark = "/Src/Voices/DataToDuty/" + await saveFilesHelper.SaveFiles(input.VoiceFile, voicepath);
                }

                await _hydrantAlarmRepository.UpdateAsync(alarm);
                return output;
            }
            catch (Exception e)
            {
                output.Success = false;
                output.FailCause = e.Message;
                return output;
            }

        }

    }

       
}
