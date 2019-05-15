using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Common.DBContext
{
    public interface ISqlRepository : IRepository
    {
        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        int Execute(string sql, params object[] parameters);

        /// <summary>
        /// 执行语句返回datatable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">sql参数</param>
        /// <returns>dataset</returns>
        DataTable Query(string sql, params object[] parameters);

        /// <summary>
        /// DataTableToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<T> DataTableToList<T>(DataTable dt) where T : new();
    }
}
