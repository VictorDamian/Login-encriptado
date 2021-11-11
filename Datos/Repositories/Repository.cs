using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public abstract class Repository
    {
        private readonly string stringConn;
        public Repository()
        {
            stringConn = "Server = DANTES; Database = ENCRYPTPASS; Integrated Security = True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(stringConn);
        }
    }
}
