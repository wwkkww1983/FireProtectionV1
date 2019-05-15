using FireProtectionV1.Infrastructure.Dto;
using FireProtectionV1.Infrastructure.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    public class AreaAppService: AppServiceBase
    {
        IAreaManager _areaManager;
        public AreaAppService(IAreaManager areaManager)
        {
            _areaManager = areaManager;
        }
        /// <summary>
        /// 根据父级区域Id查询子级区域数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetAreaOutput>> GetAreas(GetAreaInput input)
        {
            return await _areaManager.GetAreas(input);
        }
        /// <summary>
        /// 查询成华区子级区域数组
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetAreaOutput>> GetAreasChenghua()
        {
            return await _areaManager.GetAreas(new GetAreaInput() { ParentAreaId = 32949 });
        }
    }
}
