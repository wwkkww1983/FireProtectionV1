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
        /// 火警联网核警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> CheckFireAlarm([FromForm]AlarmCheckInput input)
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string pathVoice = contentRootPath + "/App_Data/Files/Voices/AlarmCheck/";
            var dto = new AlarmCheckDetailDto();
            dto.FireAlarmId = input.FireAlarmId;
            dto.CheckState = input.CheckState;
            dto.CheckContent = input.CheckContent;
            dto.CheckUserId = input.CheckUserId;
            if (input.CheckVoice != null)
            {
                dto.CheckVoiceUrl = "/Src/Voices/AlarmCheck/" + await SaveFile(input.CheckVoice, pathVoice);
                dto.CheckVoiceLength = input.CheckVoiceLength;
            }
            dto.NotifyWorker = input.NotifyWorker;
            dto.NotifyMiniStation = input.NotifyMiniStation;
            await _alarmManager.CheckFirmAlarm(dto);
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
