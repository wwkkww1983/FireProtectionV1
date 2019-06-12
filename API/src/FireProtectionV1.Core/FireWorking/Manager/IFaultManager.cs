using Abp.Domain.Services;
using FireProtectionV1.FireWorking.Dto;
using FireProtectionV1.FireWorking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.FireWorking.Manager
{
    public interface IFaultManager : IDomainService
    {
        /// <summary>
        /// 新增故障
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddDataOutput> AddNewFault(AddNewFaultInput input);
        /// <summary>
        /// 查询所有故障
        /// </summary>
        /// <returns></returns>
        IQueryable<Fault> GetFaultDataAll();
        /// <summary>
        /// 查询指定年月故障
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IQueryable<Fault> GetFaultDataMonth(int year, int month);
        /// <summary>
        /// 查询待处理故障单位
        /// </summary>
        /// <returns></returns>
        IQueryable<PendingFaultFireUnitOutput> GetPendingFaultFireUnits();
    }
}
