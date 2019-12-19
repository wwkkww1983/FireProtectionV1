using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Infrastructure.Dto;
using FireProtectionV1.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Infrastructure.Manager
{
    public class AreaManager : DomainService, IAreaManager
    {
        IRepository<Area> _areaRep;
        public AreaManager(IRepository<Area> areaRepository)
        {
            _areaRep = areaRepository;
        }
        /// <summary>
        /// 根据父级区域Id查询子级区域数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<GetAreaOutput>> GetAreas(GetAreaInput input)
        {
            return Task.FromResult<List<GetAreaOutput>>(_areaRep.GetAll().Where(p => p.ParentId == input.ParentAreaId)
                .Select(p => new GetAreaOutput()
                {
                    AreaId = p.Id,
                    AreaName = p.Name
                }).ToList());
        }
        /// <summary>
        /// 根据Id获取完整区域名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFullAreaName(int id)
        {
            string areaName = "";
            var area = await _areaRep.GetAsync(id);
            Valid.Exception(area == null, "未找到指定区域信息");

            var codes = area.AreaPath.Split('-');
            foreach (var code in codes)
            {
                areaName += _areaRep.Single(a => a.AreaCode.Equals(code));
            }

            return areaName;
        }
    }
}
