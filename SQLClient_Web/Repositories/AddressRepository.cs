using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

using SQLClient_Web.Models;

namespace SQLClient_Web.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private static AddressRepository instance;

        public static AddressRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new AddressRepository();
            }
            return instance;
        }

        //private AddressRepository() {}

        public int Create(Address address)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", null);
                    parameters.Add("@Country", address.Country);
                    parameters.Add("@ZIP", address.ZIP);
                    parameters.Add("@City", address.City);
                    parameters.Add("@Street", address.Street);
                    parameters.Add("returnValue", null, DbType.Int32, ParameterDirection.ReturnValue);

                    connection.Execute("spCrReAddress", parameters, commandType: CommandType.StoredProcedure);

                    return parameters.Get<Int32>("returnValue");
                }
                catch 
                {
                    throw;
                }
            }
        }

        public Address Read(int Id)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", Id);
                    return connection.QuerySingleOrDefault<Address>("SELECT Id, Country, City, ZIP, Street, CreationTime FROM viAddress WHERE Id=@Id", parameters);
                }
                catch
                {
                    throw;
                }
            }
        }

        public IEnumerable<Address> ReadAll()
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    connection.Open();
                    return connection.Query<Address>("SELECT Id, Country, City, ZIP, Street, CreationTime FROM viAddress");
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool Update(Address address)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    SqlCommand cmd = new SqlCommand("spCrReAddress", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", address.Id);
                    cmd.Parameters.AddWithValue("@Country", address.Country);
                    cmd.Parameters.AddWithValue("@ZIP", address.ZIP);
                    cmd.Parameters.AddWithValue("@City", address.City);
                    cmd.Parameters.AddWithValue("@Street", address.Street);
                    var result = cmd.Parameters.Add("returnValue", SqlDbType.Int);

                    cmd.ExecuteNonQuery();

                    return result != null;
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool Delete(int Id)
        {
            using(SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    connection.Open();
                    var parameters = new DynamicParameters();
                    SqlCommand cmd = new SqlCommand("spDeleteAddress", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlParameter result = new SqlParameter("returnValue", SqlDbType.Int);
                    result.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(result);

                    cmd.ExecuteNonQuery();

                    return cmd.Parameters["returnValue"].Value != null;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
