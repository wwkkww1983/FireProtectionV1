using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.FireWorking.Dto.FireDevice;
using FireProtectionV1.FireWorking.Model;

namespace FireProtectionV1.FireWorking.Manager
{
    public class IndependentDetectorManager : IIndependentDetectorManager
    {
        IRepository<IndependentDetector> _repIndependentDetector;
        public IndependentDetectorManager(IRepository<IndependentDetector> repIndependentDetector)
        {
            _repIndependentDetector = repIndependentDetector;
        }

        public async Task AddIndependentDetector(AddIndependentDetectorInput input)
        {
            Valid.Exception(_repIndependentDetector.Count(item => item.DetectorSn.Equals(input.DetectorSn)) > 0, $"编号{input.DetectorSn}的独立式火警设备已存在");
            _repIndependentDetector.InsertAsync(new IndependentDetector()
            {
                DetectorSn = input.DetectorSn,
                Brand = input.Brand,
                FireUnitId = input.FireUnitId,
                Location = input.Location,
                EnableAlarmSMS = input.EnableAlarmSMS,
                EnableFaultSMS = input.EnableFaultSMS,
                SMSPhones = input.SMSPhones,
                State = IndependentDetectorState.Offline
            });
        }

        public async Task DeleteIndependentDetector(int detectorId)
        {
            _repIndependentDetector.DeleteAsync(detectorId);
        }

        public async Task<GetIndependentDetectorOutput> GetIndependentDetector(int detectorId)
        {
            var device = await _repIndependentDetector.GetAsync(detectorId);
            return new GetIndependentDetectorOutput()
            {
                DetectorId = device.Id,
                DetectorSn = device.DetectorSn,
                Brand = device.Brand,
                FireUnitId = device.FireUnitId,
                Location = device.Location,
                EnableAlarmSMS = device.EnableAlarmSMS,
                EnableFaultSMS = device.EnableFaultSMS,
                SMSPhones = device.SMSPhones
            };
        }

        public Task<PagedResultDto<GetIndependentDetectorListOutput>> GetIndependentDetectorList(int fireunitId, PagedResultRequestDto dto)
        {
            var devices = _repIndependentDetector.GetAll().Where(item => item.FireUnitId.Equals(fireunitId));

            var query = from a in devices
                        orderby a.CreationTime descending
                        select new GetIndependentDetectorListOutput()
                        {
                            DetectorId = a.Id,
                            Brand = a.Brand,
                            DetectorSn = a.DetectorSn,
                            Location = a.Location,
                            State = a.State,
                            PowerNum = a.State.Equals(IndependentDetectorState.Offline) ? "未知" : a.PowerNum.ToString()
                        };

            return Task.FromResult(new PagedResultDto<GetIndependentDetectorListOutput>()
            {
                Items = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList(),
                TotalCount = query.Count()
            });
        }

        public async Task RenewIndependentDetector(RenewIndependentDetectorInput input)
        {
            var device = await _repIndependentDetector.FirstOrDefaultAsync(item => item.DetectorSn.Equals(input.DetectorSn));
            Valid.Exception(device == null, $"没有找到编号为{input.DetectorSn}的独立式火警设备");

            device.PowerNum = input.PowerNum;
            device.State = input.State;
            _repIndependentDetector.UpdateAsync(device);
        }

        public async Task UpdateIndependentDetector(UpdateIndependentDetectorInput input)
        {
            var device = await _repIndependentDetector.GetAsync(input.DetectorId);
            Valid.Exception(device == null, $"没有找到Id为{input.DetectorId}的独立式火警设备");
            Valid.Exception(_repIndependentDetector.Count(item => item.Id != input.DetectorId && item.DetectorSn.Equals(input.DetectorSn)) > 0, $"编号{input.DetectorSn}的独立式火警设备已存在");

            device.DetectorSn = input.DetectorSn;
            device.Brand = input.Brand;
            device.EnableAlarmSMS = input.EnableAlarmSMS;
            device.EnableFaultSMS = input.EnableFaultSMS;
            device.SMSPhones = input.SMSPhones;
            device.Location = input.Location;

            _repIndependentDetector.UpdateAsync(device);
        }
    }
}
