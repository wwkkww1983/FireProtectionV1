using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;

namespace FireProtectionV1.FireWorking.Manager
{
    public class VisionDeviceManager : IVisionDeviceManager
    {
        IRepository<VisionDevice> _repVisionDevice;
        IRepository<VisionDetector> _repVisionDetector;
        public VisionDeviceManager(
            IRepository<VisionDevice> repVisionDevice,
            IRepository<VisionDetector> repVisionDetector
            )
        {
            _repVisionDevice = repVisionDevice;
            _repVisionDetector = repVisionDetector;
        }
        /// <summary>
        /// 添加消防分析仪设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddVisionDevice(AddVisionDeviceInput input)
        {
            Valid.Exception(_repVisionDevice.Count(item => item.Sn.Equals(input.Sn)) > 0, $"编号为{input.Sn}的消防分析仪设备已存在");
            Valid.Exception(input.MonitorNum <= 0, "最大监控路数必须是一个正整数");

            var deviceId = await _repVisionDevice.InsertAndGetIdAsync(new VisionDevice()
            {
                Sn = input.Sn,
                MonitorNum = input.MonitorNum,
                SMSPhones = input.SMSPhones,
                FireUnitId = input.FireUnitId
            });
            // 根据最大监控路数，在VisionDetector表中插入数据
            for (int n = 1; n <= input.MonitorNum; n++)
            {
                _repVisionDetector.Insert(new VisionDetector()
                {
                    Sn = n,
                    VisionDeviceId = deviceId
                });
            }
        }
        /// <summary>
        /// 删除消防分析仪设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteVisionDevice(int id)
        {
            await _repVisionDetector.DeleteAsync(item => item.VisionDeviceId.Equals(id));
            await _repVisionDevice.DeleteAsync(id);
        }
        /// <summary>
        /// 获得单个消防分析仪设备详细信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<UpdateVisionDeviceInput> GetVisionDevice(int deviceId)
        {
            var device = await _repVisionDevice.GetAsync(deviceId);
            return new UpdateVisionDeviceInput()
            {
                Id = device.Id,
                MonitorNum = device.MonitorNum,
                SMSPhones = device.SMSPhones,
                Sn = device.Sn
            };
        }
        /// <summary>
        /// 获取消防分析仪设备列表
        /// </summary>
        /// <param name="fireUnitId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<PagedResultDto<VisionDeviceItemDto>> GetVisionDeviceList(int fireUnitId, PagedResultRequestDto dto)
        {
            var query = _repVisionDevice.GetAll().Where(item => item.FireUnitId.Equals(fireUnitId)).OrderByDescending(item => item.CreationTime).Select(item => new VisionDeviceItemDto()
            {
                Id = item.Id,
                Sn = item.Sn,
                MonitorNum = item.MonitorNum
            });
            var lst = query.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToList();
            return Task.FromResult(new PagedResultDto<VisionDeviceItemDto>()
            {
                Items = lst,
                TotalCount = query.Count()
            });
        }
        /// <summary>
        /// 列表中点击“监控路数”时，获取监控路数列表数据
        /// </summary>
        /// <param name="visionDeviceId"></param>
        /// <returns></returns>
        public Task<List<VisionDetectorItemDto>> GetVisionDetectorList(int visionDeviceId)
        {
            var query = _repVisionDetector.GetAll().Where(item => item.VisionDeviceId.Equals(visionDeviceId)).Select(item => new VisionDetectorItemDto()
            {
                VisionDetectorId = item.Id,
                Sn = item.Sn,
                Location = item.Location
            });

            return Task.FromResult(query.OrderBy(item => item.Sn).ToList());
        }
        /// <summary>
        /// 监控路数页面点击提交保存时调用的接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateVisionDetectorList(List<VisionDetectorItemDto> input)
        {
            foreach (var item in input)
            {
                var detector = await _repVisionDetector.GetAsync(item.VisionDetectorId);
                detector.Location = item.Location;
                _repVisionDetector.Update(detector);
            }
        }
        /// <summary>
        /// 修改消防分析仪设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateVisionDevice(UpdateVisionDeviceInput input)
        {
            Valid.Exception(_repVisionDevice.Count(item => item.Sn.Equals(input.Sn) && !item.Id.Equals(input.Id)) > 0, $"编号为{input.Sn}的消防分析仪设备已存在");
            Valid.Exception(input.MonitorNum <= 0, "最大监控路数必须是一个正整数");

            var device = await _repVisionDevice.GetAsync(input.Id);
            device.Sn = input.Sn;
            device.MonitorNum = input.MonitorNum;
            device.SMSPhones = input.SMSPhones;
            _repVisionDevice.UpdateAsync(device);

            int oldNum = _repVisionDetector.GetAll().Where(item => item.VisionDeviceId.Equals(input.Id)).Max(item => item.Sn);
            // 如果监控路数发生了变化
            if (oldNum != input.MonitorNum)
            {
                if (input.MonitorNum > oldNum)
                {
                    for (int n = oldNum + 1; n <= input.MonitorNum; n++)
                    {
                        await _repVisionDetector.InsertAsync(new VisionDetector()
                        {
                            Sn = n,
                            VisionDeviceId = input.Id
                        });
                    }
                }
                else
                {
                    _repVisionDetector.DeleteAsync(item => item.VisionDeviceId.Equals(input.Id) && item.Sn > input.MonitorNum);
                }
            }
        }
    }
}
