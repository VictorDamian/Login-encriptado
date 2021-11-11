using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public abstract class MasterRepository:Repository
    {
        protected List<SqlParameter> param;
        protected void ExecuteNonQuery(string transactionSQL)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = transactionSQL;
                    command.CommandType = CommandType.StoredProcedure;
                    foreach(SqlParameter item in param)
                    {
                        command.Parameters.Add(item);
                    }
                    command.ExecuteNonQuery();
                    param.Clear();
                }
            }
        }
        protected DataTable ExecuteReader(string transactionSQL)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = transactionSQL;
                    command.CommandType = CommandType.Text;
                    foreach(SqlParameter item in param)
                    {
                        command.Parameters.Add(item);
                    }
                    SqlDataReader reader = command.ExecuteReader();
                    using (var table = new DataTable())
                    {
                        table.Load(reader);
                        reader.Dispose();
                        return table;
                    }
                }
            }
        }
    }
}
