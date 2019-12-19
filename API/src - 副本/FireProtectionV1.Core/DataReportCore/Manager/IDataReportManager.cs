using Abp.Domain.Services;
using FireProtectionV1.DataReportCore.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DataReportCore.Manager
{
    public interface IDataReportManager : IDomainService
    {
        /// <summary>
        /// 数据监控页
        /// </summary>
        /// <returns></returns>
        Task<List<DataMinotorOutput>> GetDataMinotor();
    }
}
