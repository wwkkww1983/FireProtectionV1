using Abp.Dependency;
using Abp.EntityFrameworkCore;
using FireProtectionV1.Common.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace FireProtectionV1.EntityFrameworkCore
{
    public class SqlRepository : ISqlRepository, ITransientDependency
    {
        private readonly IDbContextProvider<FireProtectionV1DbContext> _dbContextProvider;

        public SqlRepository(IDbContextProvider<FireProtectionV1DbContext> dbContextProvider)
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

        public List<T> Query<T>(string sql, params object[] parameters) where T : class, new()
        {
            var db = _dbContextProvider.GetDbContext().Database;
            var conn = db.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();


            DataTable dt = new DataTable();


            RawSqlCommand rawSqlCommand = db.GetService<IRawSqlCommandBuilder>().Build(sql, parameters);

            RelationalDataReader query = rawSqlCommand.RelationalCommand.ExecuteReader(db.GetService<IRelationalConnection>(), parameterValues: rawSqlCommand.ParameterValues);


            DbDataReader dr = query.DbDataReader;


            int fieldCount = dr.FieldCount;

            //获取schema并填充第一行数据
            if (dr.Read())
            {
                for (int i = 0; i < fieldCount; i++)
                {
                    string colName = dr.GetName(i);
                    dt.Columns.Add(colName, dr.GetFieldType(i));
                }
                DataRow newrow = dt.NewRow();
                for (int i = 0; i < fieldCount; i++)
                {
                    newrow[i] = dr[i];
                }
                dt.Rows.Add(newrow);
            }
            //填充后续数据
            while (dr.Read())
            {
                DataRow newrow = dt.NewRow();
                for (int i = 0; i < fieldCount; i++)
                {
                    newrow[i] = dr[i];
                }
                dt.Rows.Add(newrow);
            }
            dt.AcceptChanges();

            return ConvertToModel<T>(dt);
            //return await Task.Run(() =>
            //{

            //    var db = _dbContextProvider.GetDbContext().Database;
            //    var conn = db.GetDbConnection();
            //    if (conn.State != ConnectionState.Open)
            //        conn.Open();


            //    DataTable dt = new DataTable();


            //    RawSqlCommand rawSqlCommand = db.GetService<IRawSqlCommandBuilder>().Build(sql, parameters);

            //    RelationalDataReader query = rawSqlCommand.RelationalCommand.ExecuteReader(db.GetService<IRelationalConnection>(), parameterValues: rawSqlCommand.ParameterValues);


            //    DbDataReader dr = query.DbDataReader;


            //    int fieldCount = dr.FieldCount;

            //    //获取schema并填充第一行数据
            //    if (dr.Read())
            //    {
            //        for (int i = 0; i < fieldCount; i++)
            //        {
            //            string colName = dr.GetName(i);
            //            dt.Columns.Add(colName, dr.GetFieldType(i));
            //        }
            //        DataRow newrow = dt.NewRow();
            //        for (int i = 0; i < fieldCount; i++)
            //        {
            //            newrow[i] = dr[i];
            //        }
            //        dt.Rows.Add(newrow);
            //    }
            //    //填充后续数据
            //    while (dr.Read())
            //    {
            //        DataRow newrow = dt.NewRow();
            //        for (int i = 0; i < fieldCount; i++)
            //        {
            //            newrow[i] = dr[i];
            //        }
            //        dt.Rows.Add(newrow);
            //    }
            //    dt.AcceptChanges();

            //    return dt.ToEnumerable<T>();
            //});
        }

        public static List<T> ConvertToModel<T>(DataTable dt) where T:new()
        {
            // 定义集合    
            List<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType == typeof(bool) && value.GetType() == typeof(ulong))
                            {
                                pi.SetValue(t, (ulong)value == 0 ? false : true);
                            }
                            else
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
