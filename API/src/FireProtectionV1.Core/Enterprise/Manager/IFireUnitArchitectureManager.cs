using Abp.Domain.Services;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public interface IFireUnitArchitectureManager : IDomainService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task Add(AddFireUnitArchitectureInput input);

        /// <summary>
        /// 修改建筑信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Update(UpdateFireUnitArchitectureInput input);

        /// <summary>
        /// 修改楼层信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateFloor(UpdateFireUnitArchitectureFloorInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 获取单个建筑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FireUnitArchitecture> GetById(int id);

        /// <summary>
        /// 获取某个防火单位的所有建筑信息
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <returns></returns>
        Task<List<GetFireUnitArchitectureOutput>> GetListByFireUnitId(int fireunitId);
    }
}
