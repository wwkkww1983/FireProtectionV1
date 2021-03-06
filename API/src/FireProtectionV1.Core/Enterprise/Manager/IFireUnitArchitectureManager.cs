﻿using Abp.Domain.Services;
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
        /// 获取某个防火单位的所有建筑信息（常用于建筑树状展示）
        /// </summary>
        /// <param name="fireunitId"></param>
        /// <returns></returns>
        Task<List<GetFireUnitArchitectureOutput>> GetListByFireUnitId(int fireunitId);
        /// <summary>
        /// 根据建筑ID获取下属楼层（常用于建筑与楼层级联下拉框）
        /// </summary>
        /// <param name="architectureId"></param>
        /// <returns></returns>
        Task<List<GetFloorListOutput>> GetFloorsByArchitectureId(int architectureId);
        /// <summary>
        /// 根据楼层Id获取楼层信息
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        Task<FireUnitArchitectureFloor> GetFloorById(int floorId);
    }
}
