using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SQLClient.Models;

namespace SQLClient.Repositories
{
    class AddressRepository
    {
        private readonly List<Address> cache = new List<Address>();

        public bool Create(string[] props)
        {
            Address newElement = new Address();
            MapPropsToAddress(newElement, props);

            SqlCommand cmd = new SqlCommand("spCrReAddress", Program.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cache.Add(newElement);

            return true;
        }

        public Address Read(int Id)
        {
            Address result = cache.Find(e => e.Id == Id);
            if (result == null)
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                result = Program.connection.QuerySingleOrDefault<Address>("SELECT Id, Country, City, ZIP, Street, CreationTime FROM viAddress WHERE Id=@Id", param);
                if (result == null)
                {
                    return result;
                }
                cache.Add(result);
            }
            return result;
        }

        public List<Address> ReadAll()
        {
            return cache;
        }
        public bool Update(int Id, string[] props)
        {
            Address element = cache.Find(e => e.Id == Id);
            if (element == null)
            {
                return false;
            }
            MapPropsToAddress(element, props);

            return true;
        }

        public bool Delete(int Id)
        {
            Address element = cache.Find(e => e.Id == Id);
            if (element != null)
            {
                cache.Remove(element);
                SqlCommand cmd = new SqlCommand("spDeleteAddress", Program.connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                var result = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);

                cmd.ExecuteNonQuery();
                return result.Value != null;
            }
            return false;
        }

        private bool MapPropsToAddress(Address address, string[] args)
        {
            if (address == null)
            {
                return false;
            }
            foreach (string argument in args)
            {
                string propName = argument.Split("=")[0];
                string propValue = argument.Split("=")[1];
                switch (propName)
                {
                    case "Country":
                        address.Country = propValue;
                        break;
                    case "City":
                        address.City = propValue;
                        break;
                    case "ZIP":
                        address.ZIP = propValue;
                        break;
                    case "Street":
                        address.Street = propValue;
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
    }
}
