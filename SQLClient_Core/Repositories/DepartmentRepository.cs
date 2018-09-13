using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SQLClient.Models;

namespace SQLClient.Repositories
{
    class DepartmentRepository
    {
        private readonly List<Department> cache = new List<Department>();

        public bool Create(string[] props)
        {
            Department newElement = new Department();
            MapPropsToDepartment(newElement, props);

            SqlCommand cmd = new SqlCommand("spCrReDepartment", Program.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cache.Add(newElement);

            return true;
        }

        public Department Read(int Id)
        {
            Department result = cache.Find(e => e.Id == Id);
            if (result == null)
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                result = Program.connection.QuerySingleOrDefault<Department>("SELECT Id, Country, City, ZIP, Street, CreationTime FROM viDepartment WHERE Id=@Id", param);
                if (result == null)
                {
                    return result;
                }
                cache.Add(result);
            }
            return result;
        }

        public List<Department> ReadAll()
        {
            return cache;
        }
        public bool Update(int Id, string[] props)
        {
            Department element = cache.Find(e => e.Id == Id);
            if (element == null)
            {
                return false;
            }
            MapPropsToDepartment(element, props);

            return true;
        }

        public bool Delete(int Id)
        {
            Department element = cache.Find(e => e.Id == Id);
            if (element != null)
            {
                cache.Remove(element);
                SqlCommand cmd = new SqlCommand("spDeleteDepartment", Program.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                var result = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                cmd.ExecuteNonQuery();
                return result.Value != null;
            }
            return false;
        }

        private bool MapPropsToDepartment(Department Department, string[] args)
        {
            if (Department == null)
            {
                return false;
            }
            foreach (string argument in args)
            {
                string propName = argument.Split("=")[0];
                string propValue = argument.Split("=")[1];
                try
                {
                    switch (propName)
                    {
                        case "DepartmentName":
                            Department.DepartmentName = propValue;
                            break;
                        case "CompanyId":
                            Department.CompanyId = Int32.Parse(propValue);
                            break;
                        case "ManagerId":
                            Department.ManagerId = Int32.Parse(propValue);
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please specify a number");
                }
            }
            return true;
        }
    }
}
