using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using GovFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public class FaultManager: IFaultManager
    {
        IRepository<FireAlarmDevice> _repFireAlarmDevice;
        IRepository<FireAlarmDetector> _repFireAlarmDetector;
        IRepository<BreakDown> _repBreakDown;
        IRepository<Fault> _repFault;
        public FaultManager(
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<BreakDown> repBreakDown,
            IRepository<Fault> repFault
            )
        {
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireAlarmDetector = repFireAlarmDetector;
            _repBreakDown = repBreakDown;
            _repFault = repFault;
        }
        /// <summary>
        /// 添加火警联网部件故障数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddDetectorFault(AddNewDetectorFaultInput input)
        {
            var fireAlarmDevice = await _repFireAlarmDevice.FirstOrDefaultAsync(item => item.DeviceSn.Equals(input.FireAlarmDeviceSn));
            Valid.Exception(fireAlarmDevice == null, $"未找到编号为{input.FireAlarmDeviceSn}的火警联网设施");

            var fireAlarmDetector = await _repFireAlarmDetector.FirstOrDefaultAsync(item => item.Identify.Equals(input.FireAlarmDetectorSn) && item.FireAlarmDeviceId.Equals(fireAlarmDevice.Id));
            // 如果部件数据不存在，则添加一条部件数据
            if (fireAlarmDetector == null)
            {
                int detectorId = await _repFireAlarmDetector.InsertAndGetIdAsync(new FireAlarmDetector()
                {
                    FireAlarmDeviceId = fireAlarmDevice.Id,
                    FireUnitId = fireAlarmDevice.FireUnitId,
                    Identify = input.FireAlarmDetectorSn
                });
                fireAlarmDetector = await _repFireAlarmDetector.GetAsync(detectorId);
            }
            else if (fireAlarmDetector.State.Equals(FireAlarmDetectorState.Normal)) // 如果部件状态为正常，则添加故障数据，否则忽略故障数据
            {
                int faultId = await _repFault.InsertAndGetIdAsync(new Fault()
                {
                    FireUnitId = fireAlarmDevice.FireUnitId,
                    FireAlarmDeviceId = fireAlarmDevice.Id,
                    FireAlarmDetectorId = fireAlarmDetector.Id,
                    FaultRemark = input.FaultRemark,
                });

                fireAlarmDetector.FaultNum++;
                fireAlarmDetector.LastFaultId = faultId;
                fireAlarmDetector.State = FireAlarmDetectorState.Fault;
                await _repFireAlarmDetector.UpdateAsync(fireAlarmDetector);

                await _repBreakDown.InsertAsync(new BreakDown()
                {
                    DataId = faultId,
                    FireUnitId = fireAlarmDevice.FireUnitId,
                    HandleStatus = HandleStatus.UnResolve,
                    ProblemRemark = input.FaultRemark,
                    Source = FaultSource.Terminal
                });
            }
        }
        /// <summary>
        /// 查找某个月份的火警联网部件故障数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IQueryable<Fault> GetFaultDataMonth(int year, int month)
        {
            return _repFault.GetAll().Where(p => p.CreationTime.Year == year && p.CreationTime.Month == month);
        }
    }
}
