using System.Data;
using System.Data.SqlClient;

namespace UserManager.BusinessLogic.DataAccess
{
    public class DatabaseContext
    {
        public DatabaseContext(string conectionString)
        {
            ConnectionString = conectionString;
        }

        public string ConnectionString { get; }

        protected virtual IDbConnection GetConnection() => new SqlConnection(ConnectionString);

        /// <summary>
        /// Returns an opened connection <see cref="IDbConnection"/>
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetOpenedConnection()
        {
            var connection = GetConnection();
            connection.Open();
            return connection;
        }
    }
}
