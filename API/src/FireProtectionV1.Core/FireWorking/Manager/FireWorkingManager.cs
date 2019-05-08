using Abp.Domain.Repositories;
using FireProtectionV1.Configuration;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class FireWorkingManager
    {
        IRepository<Duty> _dutyRep;
        IRepository<DataToPatrol> _patrolRep;
        IRepository<Fault> _faultRep;
        IRepository<ControllerElectric> _controllerElectricRep;
        IRepository<DetectorElectric> _detectorElectricRep;
        IRepository<ControllerFire> _controllerFireRep;
        IRepository<DetectorFire> _detectorFireRep;
        IRepository<AlarmToFire> _alarmToFireRep;
        IRepository<AlarmToElectric> _alarmToElectricRep;
        public FireWorkingManager(IRepository<Duty> dutyRep,
            IRepository<DataToPatrol> patrolRep,
            IRepository<Fault> faultRep,
            IRepository<ControllerFire> controllerFireRep,
            IRepository<ControllerElectric> controllerElectricR,
            IRepository<DetectorElectric> detectorElectricR,
            IRepository<DetectorFire> detectorFireR,
            IRepository<AlarmToFire> alarmToFireR,
            IRepository<AlarmToElectric> alarmToElectricR)
        {
            _dutyRep = dutyRep;
            _patrolRep = patrolRep;
            _faultRep = faultRep;
            _controllerFireRep = controllerFireRep;
            _controllerElectricRep = controllerElectricR;
            _detectorElectricRep = detectorElectricR;
            _detectorFireRep = detectorFireR;
            _alarmToElectricRep = alarmToElectricR;
            _alarmToFireRep = alarmToFireR;
        }
        /// <summary>
        /// 防火单位消防数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitAlarmOutput> GetFireUnitAlarm(GetFireUnitAlarmInput input)
        {
            GetFireUnitAlarmOutput output = new GetFireUnitAlarmOutput();
            int highFreq = int.Parse(ConfigHelper.Configuration["FireDomain:HighFreqAlarm"]);
            await Task.Run(() =>
            {
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.Elec30DayCount = alarmElec.Count();
                output.ElecHighCount = alarmElec.GroupBy(p => p.DeviceId).Where(p => p.Count() > highFreq).Count();
                output.ElecPointsCount = (from det in _detectorElectricRep.GetAll()
                                          join con in _controllerElectricRep.GetAll().Where(p => p.FireUnitId == input.Id)
                                          on det.ControllerId equals con.Id
                                          select det.Id).Count();
                var netStates = _controllerElectricRep.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.Status);
                int netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.ElecState = netStates.First();
                    //if (netStatesCount > 1)
                    //    output.ElecState = $"{output.ElecState}({netStates.Select(p => p.Equals(output.ElecState)).Count()}/{netStatesCount})";
                }
                //火警预警数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）；
                var alarmFire = _alarmToFireRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.Fire30DayCount = alarmFire.Count();
                output.FireHighCount = alarmFire.GroupBy(p => p.DeviceId).Where(p => p.Count() > highFreq).Count();
                output.FirePointsCount = (from det in _detectorFireRep.GetAll()
                                          join con in _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id)
                                          on det.ControllerId equals con.Id
                                          select det.Id).Count();
                //火警控制器可能不知道下面有多少点位，就以控制器数量充当
                output.FirePointsCount += _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id).Count();
                netStates = _controllerFireRep.GetAll().Where(p => p.FireUnitId == input.Id).Select(p => p.Status);
                netStatesCount = netStates.Count();
                if (netStatesCount > 0)
                {
                    output.FireState = netStates.First();
                    //if (netStatesCount > 1)
                    //    output.FireState = $"{output.FireState}({netStates.Select(p => p.Equals(output.FireState)).Count()}/{netStatesCount})";
                }
                //故障数据
                var faults = _faultRep.GetAll().Where(p => p.FireUnitId == input.Id);
                output.FaultCount = faults.Count();
                output.FaultPendingCount = faults.Where(p => p.ProcessState == 0).Count();
                output.FaultProcessedCount = output.FaultCount - output.FaultPendingCount;
                //巡查记录：最近提交时间、最近30天提交记录数量
                output.Patrol30DayCount = _patrolRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count();
                //值班记录：最近提交时间、最近30天提交记录数量
                output.Duty30DayCount = _dutyRep.GetAll().Where(p => p.FireUnitId == input.Id && p.CreationTime >= DateTime.Now.Date.AddDays(-30)).Count();
            });
            return output;
        }
        /// <summary>
        /// 安全用电最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnit30DayAlarmEleOutput> GetFireUnit30DayAlarmEle(GetFireUnit30DayAlarmEleInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 火警预警最近30天报警记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnit30DayAlarmFireOutput> GetFireUnit30DayAlarmFire(GetFireUnit30DayAlarmFireInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 安全用电高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitHighFreqAlarmEleOutput> GetFireUnitHighFreqAlarmEle(GetFireUnitHighFreqAlarmEleInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 火警预警高频报警部件查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitHighFreqAlarmFireOutput> GetFireUnitHighFreqAlarmFire(GetFireUnitHighFreqAlarmFireInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设备设施故障待处理故障查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetFireUnitPendingFaultOutput> GetFireUnitPendingFault(GetFireUnitPendingFaultInput input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 安全用电综合数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetAreasAlarmElectricOutput> GetAreasAlarmElectric(GetAreasAlarmElectricInput input)
        {
            GetAreasAlarmElectricOutput output = new GetAreasAlarmElectricOutput();
            await Task.Run(() =>
            {
                //安全用电数据：管控点位数量、网关状态、最近30天报警次数（可查）、高频报警部件数量（可查）
                var alarmElec = _alarmToElectricRep.GetAll().Where(p => p.CreationTime >= DateTime.Now.Date.AddDays(-30));
                output.JoinFireUnitCount = alarmElec.Count();
            });
            return output;
        }
    }
}
