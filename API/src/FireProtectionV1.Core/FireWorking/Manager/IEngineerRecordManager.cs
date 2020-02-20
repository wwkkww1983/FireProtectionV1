using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IEngineerRecordManager : IDomainService
    {
        /// <summary>
        /// 添加施工记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddEngineerRecord(AddEngineerRecordInput input);
    }
}
