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
        /// 添加火警联网部件故障数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddNewDetectorFault(AddNewDetectorFaultInput input);
        /// <summary>
        /// 查找某个月份的火警联网部件故障数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IQueryable<Fault> GetFaultDataMonth(int year, int month);
    }
}
