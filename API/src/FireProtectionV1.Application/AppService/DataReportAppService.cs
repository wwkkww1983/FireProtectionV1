using FireProtectionV1.DataReportCore.Dto;
using FireProtectionV1.DataReportCore.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class DataReportAppService : AppServiceBase
    {
        IDataReportManager _manager;

        public DataReportAppService(IDataReportManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 数据监控页
        /// </summary>
        /// <returns></returns>
        public async Task<List<DataMinotorOutput>> GetDataMinotor()
        {
            return await _manager.GetDataMinotor();
        }
    }
}
