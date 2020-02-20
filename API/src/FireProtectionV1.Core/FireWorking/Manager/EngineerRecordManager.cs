using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.User.Model;

namespace FireProtectionV1.FireWorking.Manager
{
    public class EngineerRecordManager : IEngineerRecordManager
    {
        IRepository<EngineerRecord> _repEngineerRecord;
        IRepository<EngineerUser> _repEngineerUser;
        IRepository<FireUnit> _repFireUnit;
        IRepository<FireUnitUser> _repFireUnitUser;
        IRepository<FireUnitUserRole> _repFireUnitUserRole;
        IRepository<FireElectricDevice> _repFireElectricDevice;
        IRepository<TsjDeviceModel> _repTsjDeviceModel;
        public EngineerRecordManager(
            IRepository<EngineerRecord> repEngineerRecord,
            IRepository<EngineerUser> repEngineerUser,
            IRepository<FireUnit> repFireUnit,
            IRepository<FireUnitUser> repFireUnitUser,
            IRepository<FireUnitUserRole> repFireUnitUserRole,
            IRepository<FireElectricDevice> repFireElectricDevice,
            IRepository<TsjDeviceModel> repTsjDeviceModel)
        {
            _repEngineerRecord = repEngineerRecord;
            _repEngineerUser = repEngineerUser;
            _repFireUnit = repFireUnit;
            _repFireUnitUser = repFireUnitUser;
            _repFireUnitUserRole = repFireUnitUserRole;
            _repFireElectricDevice = repFireElectricDevice;
            _repTsjDeviceModel = repTsjDeviceModel;
        }
        /// <summary>
        /// 添加施工记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddEngineerRecord(AddEngineerRecordInput input)
        {
            Valid.Exception(_repFireElectricDevice.Count(item => item.DeviceSn.Equals(input.DeviceSn)) > 0, $"已存在编号为{input.DeviceSn}的设施");

            int fireUnitId = input.FireUnitId;
            if (fireUnitId == 0)
            {
                Valid.Exception(string.IsNullOrEmpty(input.FireUnitName) || string.IsNullOrEmpty(input.ContractName) || string.IsNullOrEmpty(input.ContractPhone) || input.AreaId == 0 || input.Lat == 0 || input.Lng == 0, "用户名称、区域、坐标位置、联系人和手机号必须填写");
                Valid.Exception(_repFireUnit.Count(item => item.Name.Equals(input.FireUnitName)) > 0, $"已存在名称为{input.FireUnitName}的用户");
                Valid.Exception(_repFireUnitUser.Count(item => item.Account.Equals(input.ContractPhone)) > 0, $"已存在手机号为{input.ContractPhone}的用户");

                var user = await _repEngineerUser.GetAsync(input.EngineerUserId);
                // 添加防火单位
                fireUnitId = await _repFireUnit.InsertAndGetIdAsync(new FireUnit()
                {
                    Name = input.FireUnitName,
                    AreaId = input.AreaId,
                    Address = input.FireUnitName,
                    ContractName = input.ContractName,
                    ContractPhone = input.ContractPhone,
                    Lng = input.Lng,
                    Lat = input.Lat,
                    CreationTime = DateTime.Now,
                    FireDeptId = user.FireDeptId,
                    LegalPerson = input.ContractName,
                    LegalPersonPhone = input.ContractPhone,
                });
                // 添加防火单位管理员
                var userId = await _repFireUnitUser.InsertAndGetIdAsync(new FireUnitUser()
                {
                    Account = input.ContractPhone,
                    FireUnitID = fireUnitId,
                    Name = input.ContractName,
                    Password = MD5Encrypt.Encrypt("123" + input.ContractPhone, 16), // 默认密码123
                    Status = NormalStatus.Enabled
                });
                await _repFireUnitUserRole.InsertAsync(new FireUnitUserRole()
                {
                    AccountID = userId,
                    Role = FireUnitRole.FireUnitManager
                });
            }
            // 添加电气火灾设备
            var deviceId = await _repFireElectricDevice.InsertAndGetIdAsync(new FireElectricDevice()
            {
                CreationTime = DateTime.Now,
                DeviceSn = input.DeviceSn,
                DataRate = "2小时",
                DeviceModel = _repTsjDeviceModel.FirstOrDefault(item => item.DeviceType.Equals(TsjDeviceType.FireElectric)).Model,
                FireUnitId = fireUnitId,
                ExistAmpere = true,
                ExistTemperature = true,
                MaxAmpere = input.MaxAmpere,
                PhaseType = input.PhaseType,
                MaxL = input.MaxL,
                MaxN = input.MaxN,
                MaxL1 = input.MaxL1,
                MaxL2 = input.MaxL2,
                MaxL3 = input.MaxL3,
                MinAmpere = 0,
                MinL = 0,
                MinN = 0,
                MinL1 = 0,
                MinL2 = 0,
                MinL3 = 0,
                EnableCloudAlarm = input.EnableCloudAlarm,
                EnableAlarmSwitch = input.EnableAlarmSwitch,
                EnableEndAlarm = input.EnableEndAlarm,
                EnableSMS = input.EnableSMS,
                SMSPhones = input.SMSPhones,
                NetComm = "NB-IoT",
                State = FireElectricDeviceState.Offline
            });
            // 添加工程施工记录
            await _repEngineerRecord.InsertAsync(new EngineerRecord()
            {
                EngineerUserId = input.EngineerUserId,
                FireElectricDeviceId = deviceId,
                FireUnitId = fireUnitId,
            });
        }
    }
}
