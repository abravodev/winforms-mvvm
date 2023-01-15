using System.Data;
using System.Data.SqlClient;

namespace UserManager.BusinessLogic.DataAccess
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string conectionString)
        {
            _connectionString = conectionString;
        }

        public virtual SqlConnectionStringBuilder ConnectionInfo => new SqlConnectionStringBuilder(_connectionString);

        protected virtual IDbConnection GetConnection() => new SqlConnection(_connectionString);

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
