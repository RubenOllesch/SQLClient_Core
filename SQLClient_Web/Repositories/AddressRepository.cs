using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Dapper;

using SQLClient_Web.Models;
using SQLClient_Web.Helpers;

namespace SQLClient_Web.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        /*
        private static AddressRepository instance;

        public static AddressRepository Instance
        {
            get {
                if (instance == null)
                {
                    instance = new AddressRepository();
                }
                return instance;
                }
        }

        private AddressRepository() {}
        */

        private IDataBaseContext context;

        public AddressRepository(IDataBaseContext context)
        {
            this.context = context;
        }

        public int Create(Address address)
        {
            using (SqlConnection connection = context.Connection)
            {
                try
                {
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
            using (SqlConnection connection = context.Connection)
            {
                try
                {
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
            using (SqlConnection connection = context.Connection)
            {
                try
                {
                    return connection.Query<Address>("SELECT Id, Country, City, ZIP, Street, CreationTime FROM viAddress");
                }
                catch
                {
                    throw;
                }
            }
        }

        public int Update(Address address)
        {
            using (SqlConnection connection = context.Connection)
            {
                try
                {
                    var parameters = new DynamicParameters();
                    SqlCommand cmd = new SqlCommand("spCrReAddress", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    parameters.Add("@Id", address.Id);
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

        public bool Delete(int Id)
        {
            using(SqlConnection connection = context.Connection)
            {
                try
                {
                    var parameters = new DynamicParameters();
                    SqlCommand cmd = new SqlCommand("spDeleteAddress", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    parameters.Add("@Id", Id);
                    parameters.Add("returnValue", null, DbType.Int32, ParameterDirection.ReturnValue);

                    connection.Execute("spDeleteAddress", parameters, commandType: CommandType.StoredProcedure);

                    return parameters.Get<Int32>("returnValue") > 0;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
