using Abp.Application.Services.Dto;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Manager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class AlarmAppService : AppServiceBase
    {
        IAlarmManager _alarmManager;
        IHostingEnvironment _hostingEnvironment;
        public AlarmAppService(IAlarmManager alarmManager, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _alarmManager = alarmManager;
        }

        /// <summary>
        /// 获取指定防火单位警情数据
        /// </summary>
        /// <param name="FireUnitId">防火单位Id</param>
        /// <param name="Filter">筛选项'未核警,误报,测试,真实火警,已过期,全部</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmCheckOutput>> GetAlarmChecks([Required]int FireUnitId,string Filter,PagedResultRequestDto dto)
        {
            List<string> fs = new List<string>();
            if(Filter!=null)
            {
                var ff = Filter.Split(',');
                foreach(var v in ff)
                {
                    fs.Add(v);
                }

            }
            return await _alarmManager.GetAlarmChecks(FireUnitId, fs, dto);
        }
        /// <summary>
        /// 查询给定checkId的警情详细信息
        /// </summary>
        /// <param name="CheckId"></param>
        /// <returns></returns>
        public async Task<AlarmCheckDetailOutput> GetAlarmCheckDetail([Required]int CheckId)
        {
            return await _alarmManager.GetAlarmCheckDetail(CheckId);
        }
        /// <summary>
        /// 调试修复数据
        /// </summary>
        private void RepairData()
        {
            _alarmManager.RepairData();
        }

        /// <summary>
        /// 获取数据大屏的火警联网实时达
        /// </summary>
        /// <param name="fireUnitId">防火单位Id</param>
        /// <param name="dataNum">需要的数据条数，不传的话默认为5条</param>
        /// <returns></returns>
        public async Task<List<FireAlarmForDataScreenOutput>> GetFireAlarmForDataScreen(int fireUnitId, int dataNum = 5)
        {
            return await _alarmManager.GetFireAlarmForDataScreen(fireUnitId, dataNum);
        }
        /// <summary>
        /// 根据fireAlarmId获取单条火警数据详情
        /// </summary>
        /// <param name="fireAlarmId"></param>
        /// <returns></returns>
        public async Task<FireAlarmDetailOutput> GetFireAlarmById(int fireAlarmId)
        {
            return await _alarmManager.GetFireAlarmById(fireAlarmId);
        }
        /// <summary>
        /// 获取火警联网数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<FireAlarmListOutput>> GetFireAlarmList(FireAlarmListInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetFireAlarmList(input, dto);
        }
        /// <summary>
        /// 核警某一条警情[FromForm]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> CheckAlarm([FromForm]AlarmCheckInput input)
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string pathPhoto = contentRootPath + "/App_Data/Files/Photos/AlarmCheck/";
            string pathVoice = contentRootPath + "/App_Data/Files/Voices/AlarmCheck/";
            var dto = new AlarmCheckDetailDto();
            dto.CheckId = input.CheckId;
            dto.CheckState = input.CheckState;
            dto.Content = input.Content;
            dto.UserId = input.UserId;
            if (input.Picture1 != null)
                dto.PictureUrl_1 = "/Src/Photos/AlarmCheck/" + await SaveFile(input.Picture1, pathPhoto);
            if (input.Picture2 != null)
                dto.PictureUrl_2 = "/Src/Photos/AlarmCheck/" + await SaveFile(input.Picture2, pathPhoto);
            if (input.Picture3 != null)
                dto.PictureUrl_3 = "/Src/Photos/AlarmCheck/" + await SaveFile(input.Picture3, pathPhoto);
            if (input.Voice != null)
            {
                dto.VioceUrl = "/Src/Voices/AlarmCheck/" + await SaveFile(input.Voice, pathVoice);
                dto.VoiceLength = input.VoiceLength;
            }
            dto.NotifyWorker = input.NotifyWorker;
            dto.NotifyMiniaturefire = input.NotifyMiniaturefire;
            dto.Notify119 = input.Notify119;
            await _alarmManager.CheckAlarm(dto);
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="path"></param>
        /// <returns>new filename</returns>
        private async Task<string> SaveFile(IFormFile formFile,string path)
        {
            if (formFile != null)
            {
                string filename= DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString("N").Substring(0,16)+ Path.GetExtension(formFile.FileName);
                using (var stream = System.IO.File.Create(path+ filename))
                {
                    await formFile.CopyToAsync(stream);
                }
                return filename; 
            }
            return "";
        }
    }
}
