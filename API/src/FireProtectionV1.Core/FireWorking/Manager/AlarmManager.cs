using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class AlarmManager:IAlarmManager
    {
        IDeviceManager _deviceManager;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        public AlarmManager(
            IDeviceManager deviceManager,
            IRepository<AlarmToFire> alarmToFireRep,
            IRepository<AlarmToElectric> alarmToElectricRep
        )
        {
            _deviceManager = deviceManager;
            _alarmToElectricRep = alarmToElectricRep;
            _alarmToFireRep = alarmToFireRep;
        }
        /// <summary>
        /// 新增安全用电报警
        /// </summary>
        /// <param name="input"></param>
        /// <param name="alarmLimit"></param>
        /// <returns></returns>
        public async Task AddAlarmElec(AddDataElecInput input,string alarmLimit)
        {
            Detector detector = _deviceManager.GetDetector(input.Identify);
            if(detector==null)
            {
                //新增探测器
                detector=await _deviceManager.AddDetector(new AddDetectorInput()
                {
                    DetectorGBType = input.DetectorGBType,
                    GatewayIdentify = input.GatewayIdentify,
                    Identify = input.Identify
                });
            }
            await _alarmToElectricRep.InsertAsync(new AlarmToElectric() {
                DetectorId=detector.Id,
                Analog=input.Analog,
                AlarmLimit=alarmLimit,
                Unit=input.Unit
            });
        }
        /// <summary>
        /// 新增火灾监控设备报警
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAlarmFire(AddAlarmFireInput input)
        {
            Detector detector = _deviceManager.GetDetector(input.Identify);
            if (detector == null)
            {
                //新增探测器
                detector = await _deviceManager.AddDetector(new AddDetectorInput()
                {
                    DetectorGBType = input.DetectorGBType,
                    GatewayIdentify = input.GatewayIdentify,
                    Identify = input.Identify
                });
            }
            await _alarmToFireRep.InsertAsync(new AlarmToFire()
            {
                FireUnitId=detector.FireUnitId,
                DetectorId=detector.Id
            });
        }
    }
}
