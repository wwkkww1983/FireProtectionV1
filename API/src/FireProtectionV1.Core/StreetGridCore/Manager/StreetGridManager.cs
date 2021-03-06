﻿using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.StreetGridCore.Manager
{
    public class StreetGridUserManager : DomainService, IStreetGridUserManager
    {
        IRepository<StreetGridUser> _streetGridUserRepository;

        public StreetGridUserManager(IRepository<StreetGridUser> streetGridUserRepository)
        {
            _streetGridUserRepository = streetGridUserRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultDto<GetStreetListOutput>> GetList(GetStreetGridUserListInput input)
        {
            var streetGridUsers = _streetGridUserRepository.GetAll();

            var expr = ExprExtension.True<StreetGridUser>()
             .IfAnd(!string.IsNullOrEmpty(input.GridName), item => item.GridName.Contains(input.GridName));

            streetGridUsers = streetGridUsers.Where(expr);
            var temp = from a in streetGridUsers
                       select new GetStreetListOutput
                       {
                           Name=a.Name,
                           Phone=a.Phone,
                           GridName=a.GridName,
                           Street=a.Street,
                           Community=a.Community,
                           id=a.Id,
                           isDeleted=a.IsDeleted,
                           CreationTime = a.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")
                       };

            List<GetStreetListOutput> list = temp
                .OrderByDescending(item => item.CreationTime)
                .Skip(input.SkipCount).Take(input.MaxResultCount)
                .ToList();
            var tCount = streetGridUsers.Count();

            return Task.FromResult(new PagedResultDto<GetStreetListOutput>(tCount, list));
        }

        /// <summary>
        /// 街道网格Excel导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<List<StreetGridUser>> GetStreetGridExcel(GetStreetGridUserListInput input)
        {
            var streetGridUsers = _streetGridUserRepository.GetAll();

            var expr = ExprExtension.True<StreetGridUser>()
             .IfAnd(!string.IsNullOrEmpty(input.GridName), item => item.GridName.Contains(input.GridName));

            streetGridUsers = streetGridUsers.Where(expr);

            List<StreetGridUser> list = streetGridUsers
                .OrderByDescending(item => item.CreationTime)
                .ToList();

            return Task.FromResult<List<StreetGridUser>>(list);
        }
    }
}
