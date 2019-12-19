using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.TsjDevice.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.TsjDevice.Manager
{
    public class TsjDeviceManager:ITsjDeviceManager
    {
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<RecordAnalog> _repRecordAnalog;
        IRepository<BreakDown> _repBreakDown;
        IRepository<AlarmCheck> _repAlarmCheck;
        IRepository<AlarmToElectric> _repAlarmToElectric;
        IRepository<AlarmToFire> _repAlarmToFire;
        IRepository<Detector> _repDetector;
        IRepository<Fault> _repFault;
        public TsjDeviceManager(
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<RecordAnalog> repRecordAnalog,
            IRepository<BreakDown> repBreakDown,
            IRepository<AlarmCheck> repAlarmCheck,
            IRepository<AlarmToElectric> repAlarmToElectric,
            IRepository<AlarmToFire> repAlarmToFire,
            IRepository<Fault> repFault,
            IRepository<Detector> repDetector)
        {
            _repFireElectricDevice = repFireElectricDevice;
            _repRecordAnalog = repRecordAnalog;
            _repAlarmCheck = repAlarmCheck;
            _repAlarmToElectric = repAlarmToElectric;
            _repAlarmToFire = repAlarmToFire;
            _repFault = repFault;
            _repDetector = repDetector;
        }
        public async Task<SuccessOutput> NewFault(NewFaultInput input)
        {
            var detector = await _repDetector.FirstOrDefaultAsync(p => p.Identify.Equals(input.Identify) && p.Origin.Equals(Origin.Tianshuju));
            if (detector == null)
                return new SuccessOutput() { Success = false, FailCause = "探测器不存在" };
            var id = _repFault.InsertAndGetId(new Fault()
            {
                GatewayId=detector.GatewayId,
                FireUnitId = detector.FireUnitId,
                DetectorId = detector.Id,
                FaultRemark = "终端故障",
                ProcessState = (byte)Common.Enum.HandleStatus.UuResolve
            });
            detector.FaultNum++;
            detector.LastFaultId = id;
            await _repDetector.UpdateAsync(detector);

            var breakdown = new BreakDown()
            {
                DataId = id,
                FireUnitId = detector.FireUnitId,
                HandleStatus = (byte)Common.Enum.HandleStatus.UuResolve,
                Remark = "终端故障",
                Source = (byte)Common.Enum.SourceType.Terminal
            };
            var breakdownId = await _repBreakDown.InsertAndGetIdAsync(breakdown);
            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> NewAlarm(NewAlarmInput input)
        {
            var detector = await _repDetector.FirstOrDefaultAsync(p => p.Identify.Equals(input.Identify) && p.Origin.Equals(Origin.Tianshuju));
            if (detector == null)
                return new SuccessOutput() { Success = false, FailCause = "探测器不存在" };
            int id=await _repAlarmToFire.InsertAndGetIdAsync(new AlarmToFire()
            {
                DetectorId = detector.Id,
                FireUnitId = detector.FireUnitId,
                GatewayId = detector.GatewayId
            });
            await _repAlarmCheck.InsertAsync(new AlarmCheck()
            {
                AlarmDataId = id,
                FireSysType = detector.FireSysType,
                FireUnitId = detector.FireUnitId,
                CheckState = (byte)CheckStateType.UnCheck
            });

            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> NewMonitor(NewMonitorInput input)
        {
            try
            {
            var analog = double.Parse(input.Value);
            var detector =await _repDetector.FirstOrDefaultAsync(p => p.Identify.Equals(input.Identify) && p.Origin.Equals(Origin.Tianshuju));
            detector.State = $"{input.Value}";
            await _repDetector.UpdateAsync(detector);
            await _repRecordAnalog.InsertAsync(new RecordAnalog()
            {
                Analog = analog,
                DetectorId = detector.Id
            });
            var device = await _repFireElectricDevice.FirstOrDefaultAsync(p => p.GatewayId == detector.GatewayId);
            if (!string.IsNullOrEmpty(device.PhaseJson))
            {
                var jobject = JObject.Parse(device.PhaseJson);
                if (jobject.ContainsKey($"{input.Identify}max"))
                {
                    var max = double.Parse(jobject[$"{input.Identify}max"].ToString());
                    if (analog >= max)
                        device.State = "超限";
                    else if (analog >= max * 0.8)
                        device.State = "隐患";
                    else
                        device.State = "良好";
                    await _repFireElectricDevice.UpdateAsync(device);
                }
            }

            }catch(Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }

            return new SuccessOutput() { Success = true };
        }
        public async Task<SuccessOutput> NewOverflow(NewOverflowInput input)
        {
            try
            {
                var analog = double.Parse(input.Value);
                var detector = await _repDetector.FirstOrDefaultAsync(p => p.Identify.Equals(input.Identify) && p.Origin.Equals(Origin.Tianshuju));
                string unit = input.Identify.Equals("I0") ? "mA" : "℃";
                detector.State = $"{input.Value}{unit}";
                await _repDetector.UpdateAsync(detector);
                await _repRecordAnalog.InsertAsync(new RecordAnalog()
                {
                    Analog = analog,
                    DetectorId = detector.Id
                });
                var device = await _repFireElectricDevice.FirstOrDefaultAsync(p => p.GatewayId == detector.GatewayId);
                string alarmLimit="";
                if (!string.IsNullOrEmpty(device.PhaseJson))
                {
                    var jobject = JObject.Parse(device.PhaseJson);
                    if (jobject.ContainsKey($"{input.Identify}max"))
                    {
                        var max = double.Parse(jobject[$"{input.Identify}max"].ToString());
                        if (analog >= max)
                            device.State = "超限";
                        else if (analog >= max * 0.8)
                            device.State = "隐患";
                        else
                            device.State = "良好";
                        await _repFireElectricDevice.UpdateAsync(device);
                        alarmLimit = max.ToString()+unit;
                    }
                }
                int id = _repAlarmToElectric.InsertAndGetId(new AlarmToElectric()
                {
                    FireUnitId = detector.FireUnitId,
                    DetectorId = detector.Id,
                    Analog = analog,
                    AlarmLimit = alarmLimit,
                    Unit = unit
                });
                await _repAlarmCheck.InsertAsync(new AlarmCheck()
                {
                    AlarmDataId = id,
                    FireSysType = detector.FireSysType,
                    FireUnitId = detector.FireUnitId,
                    CheckState = (byte)CheckStateType.UnCheck
                });
            }
            catch (Exception e)
            {
                return new SuccessOutput() { Success = false, FailCause = e.Message };
            }

            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 获取固件更新列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<UpdateFirmwareOutput>> GetUpdateFirmwareList()
        {
            return new List<UpdateFirmwareOutput>();
        }
    }
}
