using Abp.Domain.Services;
using FireProtectionV1.TsjDevice.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.TsjDevice.Manager
{
    public interface ITsjDeviceManager : IDomainService
    {
        Task<SuccessOutput> NewFault(NewFaultInput input);
        Task<SuccessOutput> NewAlarm(NewAlarmInput input);
        Task<SuccessOutput> NewOverflow(NewOverflowInput input);
        Task<SuccessOutput> NewMonitor(NewMonitorInput input);
        Task<List<UpdateFirmwareOutput>> GetUpdateFirmwareList();
    }
}
