using System.Data.SqlClient;

namespace SQLClient_Web.Helpers
{
    public interface IDataBaseContext
    {
        SqlConnection Connection { get; }
    }
}
