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

        public AlarmAppService(IAlarmManager alarmManager)
        {
            _alarmManager = alarmManager;
        }
        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAlarmFire(AddAlarmFireInput input)
        {
            //Console.WriteLine($"{DateTime.Now} 收到报警 AddAlarmFire 部件类型:{input.DetectorGBType.ToString()} 部件地址：{input.Identify} 网关地址：{input.GatewayIdentify}");
            await _alarmManager.AddAlarmFire(input);
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
        /// 获取防火单位电气火灾警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ElectricAlarmListOutput>> GetElectricAlarmList(GetElectricAlarmListInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetElectricAlarmList(input, dto);
        }
        /// <summary>
        /// 添加消防分析仪海康平台报警数据
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public void AddAlarmVisionHikapi([FromBody]object body)
        {
            string s = body.ToString();
            Console.WriteLine(s);

            /* 数据如下，定义参考https://open.hikvision.com/docs/582054b384304433aad532055dfbb570
            {
                "method": "OnEventNotify",
  "params": {
                    "ability": "",
    "events": [
      {
        "eventDetails": [
          {
            "ability": "event_fpms",
            "data": {
              "extendKey": "simulate",
              "lanId": "zh_CN"
            },
            "eventOriginalId": "1RF2EC7DHIM201YJR5NYMUD461X1RWTE",
            "eventType": 254104,
            "locationIndexCode": "",
            "locationName": "",
            "regionIndexCode": "root000000",
            "regionName": "根节点",
            "srcIndex": "4938b8b5566d4ee283a9ccbd5c607ac2",
            "srcName": "视频2@NP-FA108(D46705649)",
            "srcType": "camera"
          }
        ],
        "eventId": "F97F5A0BA985465CB2138C7291527562",
        "eventLvl": 3,
        "eventName": "火点",
        "eventOldId": "1RF2EC7DHIM201YJR5NYMUD461X1RWTE",
        "eventType": 0,
        "happenTime": "2020-03-24T10:33:21.137+08:00",
        "linkageAcion": [
          {
            "content": "{\"bPopupEventPicture\":true}\n",
            "linkageType": "popUpPictureOnClient"
          },
          {
            "content": "{\"bAudioAlarm\":true}\n",
            "linkageType": "alertBySound"
          },
          {
            "content": "{\"soundText\":\"根节点，视频2@NP-FA108(D46705649)发生火点报警事件\"}\n",
            "linkageType": "alertByEventInfo"
          }
        ],
        "remark": "",
        "ruleDescription": "",
        "srcIndex": "dd668537a8c04531b23a216a3719aa3b",
        "srcName": "火点",
        "srcType": "eventRule",
        "status": 0,
        "stopTime": "2020-03-24T10:33:21.137+08:00",
        "timeout": 30
      }
    ],
    "sendTime": "2020-03-24T10:33:21.137+08:00"
  }
}
{
  "method": "OnEventNotify",
  "params": {
    "ability": "",
    "events": [
      {
        "eventDetails": [
          {
            "ability": "event_fpms",
            "data": {
              "extendKey": "simulate",
              "lanId": "zh_CN"
            },
            "eventOriginalId": "H6RFWL3OTPNCJN9E8354K8GS5EE1L2MB",
            "eventType": 254103,
            "locationIndexCode": "",
            "locationName": "",
            "regionIndexCode": "root000000",
            "regionName": "根节点",
            "srcIndex": "4938b8b5566d4ee283a9ccbd5c607ac2",
            "srcName": "视频2@NP-FA108(D46705649)",
            "srcType": "camera"
          }
        ],
        "eventId": "12C5A0B64FE54557BB47CBA5CBCB2A7D",
        "eventLvl": 3,
        "eventName": "消防通道占用视屏2",
        "eventOldId": "H6RFWL3OTPNCJN9E8354K8GS5EE1L2MB",
        "eventType": 0,
        "happenTime": "2020-03-23T17:02:31.55+08:00",
        "linkageAcion": [
          {
            "content": "{\"bPopupEventPicture\":true}\n",
            "linkageType": "popUpPictureOnClient"
          },
          {
            "content": "{\"bAudioAlarm\":true}\n",
            "linkageType": "alertBySound"
          },
          {
            "content": "{\"soundText\":\"根节点，视频2@NP-FA108(D46705649)发生通道占用报警事件\"}\n",
            "linkageType": "alertByEventInfo"
          }
        ],
        "remark": "",
        "ruleDescription": "",
        "srcIndex": "4e4eca9681ab4f76ab0c191c00c45043",
        "srcName": "消防通道占用视屏2",
        "srcType": "eventRule",
        "status": 0,
        "stopTime": "2020-03-23T17:02:31.55+08:00",
        "timeout": 30
      }
    ],
    "sendTime": "2020-03-23T17:02:31.55+08:00"
  }
}*/
        }
        /// <summary>
        /// 添加消防分析仪报警数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAlarmVision([FromForm]AddAlarmVisionInput input)
        {
            await _alarmManager.AddAlarmVision(input);
        }
        /// <summary>
        /// 获取防火单位消防分析仪报警列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmVisionListOutput>> GetVisionAlarmList(AlarmVisionListInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetVisionAlarmList(input, dto);
        }
        /// <summary>
        /// 监管部门获取消防分析仪报警列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlarmVisionList_DeptOutput>> GetVisionAlarmList_Dept(AlarmVisionList_DeptInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetVisionAlarmList_Dept(input, dto);
        }
        /// <summary>
        /// 获取某条消防分析仪报警数据的照片
        /// </summary>
        /// <param name="visionAlarmId"></param>
        /// <returns></returns>
        public async Task<string> GetVisionAlarmPhotoPath(int visionAlarmId)
        {
            return await _alarmManager.GetVisionAlarmPhotoPath(visionAlarmId);
        }
        /// <summary>
        /// 获取工程端电气火灾警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ElectricAlarmListOutput>> GetElectricAlarmListForEngineer(GetElectricAlarmListForEngineerInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetElectricAlarmListForEngineer(input, dto);
        }
        /// <summary>
        /// 获取消防管网警情数据列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<WaterAlarmListOutput>> GetWaterAlarmList(GetWaterAlarmListInput input, PagedResultRequestDto dto)
        {
            return await _alarmManager.GetWaterAlarmList(input, dto);
        }
        /// <summary>
        /// 火警联网核警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CheckFireAlarm([FromForm]AlarmCheckInput input)
        {
            await _alarmManager.CheckFirmAlarm(input);
        }
        /// <summary>
        /// 获取区域内各防火单位火警联网的真实火警报119数据
        /// </summary>
        /// <param name="fireDeptId"></param>
        /// <returns></returns>
        public async Task<List<GetTrueFireAlarmListOutput>> GetAlarmTo119List(int fireDeptId)
        {
            return await _alarmManager.GetAlarmTo119List(fireDeptId);
        }
        /// <summary>
        /// 获取防火单位未读警情类型及数量
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <returns></returns>
        public async Task<List<GetNoReadAlarmNumOutput>> GetNoReadAlarmNumList(int fireUnitId)
        {
            return await _alarmManager.GetNoReadAlarmNumList(fireUnitId);
        }
        /// <summary>
        /// 获取工程端未读警情类型及数量
        /// </summary>
        /// <param name="engineerId"></param>
        /// <returns></returns>
        public async Task<List<GetNoReadAlarmNumOutput>> GetNoReadAlarmNumListForEngineer(int engineerId)
        {
            return await _alarmManager.GetNoReadAlarmNumListForEngineer(engineerId);
        }
    }
}
