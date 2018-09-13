using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SQLClient.Models;

namespace SQLClient.Repositories
{
    class CompanyRepository
    {
        private readonly List<Company> cache = new List<Company>();

        public bool Create(string[] props)
        {
            Company newElement = new Company();
            MapPropsToCompany(newElement, props);
            newElement.CreationTime = new System.DateTime();

            SqlCommand cmd = new SqlCommand("spCrReCompany", Program.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cache.Add(newElement);

            return true;
        }

        public Company Read(int Id)
        {
            Company result = cache.Find(e => e.Id == Id);
            if (result == null)
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                result = Program.connection.QuerySingleOrDefault<Company>("SELECT CompanyName, CreationTime FROM viCompany WHERE Id=@Id", param);
                if (result == null)
                {
                    return result;
                }
                cache.Add(result);
            }
            return result;
        }

        public List<Company> ReadAll()
        {
            return cache;
        }
        public bool Update(int Id, string[] props)
        {
            Company element = cache.Find(e => e.Id == Id);
            if (element == null)
            {
                return false;
            }
            MapPropsToCompany(element, props);

            return true;
        }

        public bool Delete(int Id)
        {
            Company element = cache.Find(e => e.Id == Id);
            if (element != null)
            {
                cache.Remove(element);
                SqlCommand cmd = new SqlCommand("spDeleteCompany", Program.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                var result = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                cmd.ExecuteNonQuery();
                return result.Value != null;
            }
            return false;
        }

        private bool MapPropsToCompany(Company Company, string[] args)
        {
            if (Company == null)
            {
                return false;
            }
            foreach (string argument in args)
            {
                string propName = argument.Split("=")[0];
                string propValue = argument.Split("=")[1];
                switch (propName)
                {
                    case "CompanyName":
                        Company.CompanyName = propValue;
                        break;
                }
            }
            return true;
        }
    }
}
