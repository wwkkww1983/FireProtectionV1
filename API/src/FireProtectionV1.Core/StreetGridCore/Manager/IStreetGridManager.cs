﻿using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using FireProtectionV1.StreetGridCore.Dto;
using FireProtectionV1.StreetGridCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.StreetGridCore.Manager
{
    public interface IStreetGridUserManager : IDomainService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<StreetGridUser>> GetList(GetStreetGridUserListInput input);
    }
}
