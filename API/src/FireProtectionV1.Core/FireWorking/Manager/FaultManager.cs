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
        IRepository<DetectorType> _repDetectorType;
        public FaultManager(
            IRepository<FireAlarmDevice> repFireAlarmDevice,
            IRepository<FireAlarmDetector> repFireAlarmDetector,
            IRepository<BreakDown> repBreakDown,
            IRepository<Fault> repFault,
            IRepository<DetectorType> repDetectorType
            )
        {
            _repFireAlarmDevice = repFireAlarmDevice;
            _repFireAlarmDetector = repFireAlarmDetector;
            _repBreakDown = repBreakDown;
            _repFault = repFault;
            _repDetectorType = repDetectorType;
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
                    Identify = input.FireAlarmDetectorSn,
                    State = FireAlarmDetectorState.Normal
                });
                fireAlarmDetector = await _repFireAlarmDetector.GetAsync(detectorId);
            }
            if (fireAlarmDetector.State.Equals(FireAlarmDetectorState.Normal)) // 如果部件状态为正常，则添加故障数据，否则忽略故障数据
            {
                int faultId = await _repFault.InsertAndGetIdAsync(new Fault()
                {
                    FireUnitId = fireAlarmDevice.FireUnitId,
                    FireAlarmDeviceId = fireAlarmDevice.Id,
                    FireAlarmDetectorId = fireAlarmDetector.Id
                });

                fireAlarmDetector.FaultNum++;
                fireAlarmDetector.LastFaultId = faultId;
                fireAlarmDetector.State = FireAlarmDetectorState.Fault;
                await _repFireAlarmDetector.UpdateAsync(fireAlarmDetector);

                // 部件故障详情：【联网设施】TSJ-CS101912001【部件编号】0001机001回路【部件类型】感烟式火灾探测器【部件位置】行政楼3楼3002室
                string deviceSn = !string.IsNullOrEmpty(fireAlarmDevice.DeviceSn) ? fireAlarmDevice.DeviceSn : " - ";
                string detectorSn = !string.IsNullOrEmpty(fireAlarmDetector.Identify) ? fireAlarmDetector.Identify : " - ";
                string detectorTypeName = " - ";
                if (fireAlarmDetector.DetectorTypeId > 0)
                {
                    var detectorType = await _repDetectorType.GetAsync(fireAlarmDetector.DetectorTypeId);
                    if (detectorType != null) detectorTypeName = detectorType.Name;
                }
                string detectorAddress = !string.IsNullOrEmpty(fireAlarmDetector.FullLocation) ? fireAlarmDetector.FullLocation : " -";
                string problemRemark = $"部件故障详情：【联网设施】{deviceSn}【部件编号】{detectorSn}【部件类型】{detectorTypeName}【部件位置】{detectorAddress}";

                await _repBreakDown.InsertAsync(new BreakDown()
                {
                    DataId = faultId,
                    FireUnitId = fireAlarmDevice.FireUnitId,
                    HandleStatus = HandleStatus.UnResolve,
                    ProblemRemark = problemRemark,
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
