using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Manager;
using FireProtectionV1.Enterprise.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.AppService
{
    /// <summary>
    /// 防火单位建筑
    /// </summary>
    public class FireUnitArchitectureAppService : HttpContextAppService
    {
        IFireUnitArchitectureManager _fireUnitArchitectureManager;

        public FireUnitArchitectureAppService(
            IFireUnitArchitectureManager fireUnitArchitectureManager,
            IHttpContextAccessor httpContext)
            : base(httpContext)
        {
            _fireUnitArchitectureManager = fireUnitArchitectureManager;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public async Task Add(AddFireUnitArchitectureInput input)
        {
            await _fireUnitArchitectureManager.Add(input);
        }

        /// <summary>
        /// 修改建筑信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateFireUnitArchitectureInput input)
        {
            await _fireUnitArchitectureManager.Update(input);
        }

        /// <summary>
        /// 修改楼层信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateFloor(UpdateFireUnitArchitectureFloorInput input)
        {
            await _fireUnitArchitectureManager.UpdateFloor(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            await _fireUnitArchitectureManager.Delete(id);
        }

        /// <summary>
        /// 获取单个建筑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FireUnitArchitecture> GetById(int id)
        {
            return await _fireUnitArchitectureManager.GetById(id);
        }

        /// <summary>
        /// 获取某个防火单位的所有建筑信息（常用于建筑树状展示）
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <returns></returns>
        public async Task<List<GetFireUnitArchitectureOutput>> GetListByFireUnitId(int fireunitId)
        {
            return await _fireUnitArchitectureManager.GetListByFireUnitId(fireunitId);
        }

        /// <summary>
        /// 根据建筑ID获取下属楼层（常用于建筑与楼层级联下拉框）
        /// </summary>
        /// <param name="architectureId"></param>
        /// <returns></returns>
        public async Task<List<FireUnitArchitectureFloor>> GetFloorsByArchitectureId(int architectureId)
        {
            return await _fireUnitArchitectureManager.GetFloorsByArchitectureId(architectureId);
        }
    }
}
