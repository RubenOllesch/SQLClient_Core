using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace SQLClient_Web.Helpers
{
    public class DataBaseContext : IDataBaseContext
    {
        private string connectionString;
        public DataBaseContext(IOptions<DataBaseSettings> options)
        {
            connectionString = options.Value.ConnectionString;
        }
        public SqlConnection Connection
        {
            get
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
        }
    }
}
