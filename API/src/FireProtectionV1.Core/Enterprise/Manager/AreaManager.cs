using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public class AreaManager : DomainService, IAreaManager
    {
        IRepository<Area> _areaRepository;
        public AreaManager(IRepository<Area> areaRepository)
        {
            _areaRepository = areaRepository;
        }

        /// <summary>
        /// 根据Id获取完整区域名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFullAreaName(int id)
        {
            string areaName = "";
            var area = await _areaRepository.GetAsync(id);
            Valid.Exception(area == null, "未找到指定区域信息");

            var codes = area.AreaPath.Split('-');
            foreach (var code in codes)
            {
                areaName += _areaRepository.Single(a => a.AreaCode.Equals(code));
            }

            return areaName;
        }
    }
}
