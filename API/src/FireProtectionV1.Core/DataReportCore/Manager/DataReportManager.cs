using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.DataReportCore.Dto;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.HydrantCore.Model;
using FireProtectionV1.MiniFireStationCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.DataReportCore.Manager
{
    public class DataReportManager : DomainService, IDataReportManager
    {
        IRepository<FireUnit> _fireUnitRepository;
        IRepository<MiniFireStation> _miniFireStationRepository;
        IRepository<Hydrant> _hydrantRepository;

        public DataReportManager(IRepository<FireUnit> fireunitRepository, IRepository<MiniFireStation> miniFireStationRepository, IRepository<Hydrant> hydrantRepository)
        {
            _fireUnitRepository = fireunitRepository;
            _miniFireStationRepository = miniFireStationRepository;
            _hydrantRepository = hydrantRepository;
        }

        /// <summary>
        /// 数据监控页
        /// </summary>
        /// <returns></returns>
        public async Task<List<DataMinotorOutput>> GetDataMinotor()
        {
            var fireUnitCount = await _fireUnitRepository.CountAsync();
            var miniFireStationCount = await _miniFireStationRepository.CountAsync();
            var hydrantCount = await _hydrantRepository.CountAsync();

            return new List<DataMinotorOutput>()
            {
                new DataMinotorOutput()
                {
                    DataTypeName = "防火单位",
                    JoinNumber = fireUnitCount
                },
                new DataMinotorOutput()
                {
                    DataTypeName = "微型消防站",
                    JoinNumber = miniFireStationCount
                },
                new DataMinotorOutput()
                {
                    DataTypeName = "市政消火栓",
                    JoinNumber = hydrantCount
                },
                new DataMinotorOutput()
                {
                    DataTypeName = "综合数据报表",
                    JoinNumber = 4
                }
            };
        }
    }
}
