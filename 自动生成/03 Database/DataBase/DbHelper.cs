using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataBase
{
    public class DbHelper
    {
        private static IDbConnection GetConnection()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbname"].ConnectionString);
        }

        public static int Execute(string sql,object model)
        {
            using (var connection = DbHelper.GetConnection())
            {
               return connection.Execute(sql, model);
            }
        }
        public static T ExecuteScalar<T>(string sql, object model)
        {
            using (var connection = DbHelper.GetConnection())
            {
                return connection.ExecuteScalar<T>(sql, model);
            }
        }
        public static IEnumerable<T> Query<T>(string sql,object model)
        {
            using (var connection = DbHelper.GetConnection())
            {
                return connection.Query<T>(sql, model);
            }
        }
        public static object ExecuteScalar(string sql, object model)
        {
            using (var connection = DbHelper.GetConnection())
            {
                return connection.ExecuteScalar(sql, model);
            }
        }
        public static void Tran(Action<IDbConnection, IDbTransaction> action)
        {
            using (var connection = DbHelper.GetConnection())
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action(connection, transaction);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }
             
    }
}
