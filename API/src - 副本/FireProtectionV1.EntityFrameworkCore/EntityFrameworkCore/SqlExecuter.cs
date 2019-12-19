using Abp.Dependency;
using Abp.EntityFrameworkCore;
using FireProtectionV1.Common.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireProtectionV1.EntityFrameworkCore
{
    public class SqlExecuter : ISqlExecuter, ITransientDependency
    {
        private readonly IDbContextProvider<FireProtectionV1DbContext> _dbContextProvider;

        public SqlExecuter(IDbContextProvider<FireProtectionV1DbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        public int Execute(string sql, params object[] parameters)
        {
            return _dbContextProvider.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。
        /// </summary>
        /// <typeparam name="T">查询所返回对象的类型</typeparam>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        public List<T> SqlQuery<T>(string sql, params object[] parameters) where T : class, new()
        {
            return _dbContextProvider.GetDbContext().Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public List<T> SqlQuery1<T>(string sql, params object[] parameters)
            where T : new()
        {
            //注意：不要对GetDbConnection获取到的conn进行using或者调用Dispose，否则DbContext后续不能再进行使用了，会抛异常
            var rtnList = new List<T>();
            var conn = _dbContextProvider.GetDbContext().Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                using (var command = conn.CreateCommand())
                //using (var tran = conn.BeginTransaction())
                {
                    command.CommandText = sql;
                    command.Parameters.AddRange(parameters);

                    var propts = typeof(T).GetProperties();

                    T model;
                    object val;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = new T();
                            foreach (var l in propts)
                            {
                                val = reader[l.Name];
                                if (val == DBNull.Value)
                                {
                                    l.SetValue(model, null);
                                }
                                else
                                {
                                    l.SetValue(model, val);
                                }
                            }
                            rtnList.Add(model);
                        }
                    }

                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return rtnList;
        }

    }
}
