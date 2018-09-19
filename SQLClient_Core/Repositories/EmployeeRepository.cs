using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SQLClient.Models;

namespace SQLClient.Repositories
{
    class EmployeeRepository
    {
        private readonly List<Employee> cache = new List<Employee>();

        public bool Create(string[] props)
        {
            Employee newElement = new Employee();
            MapPropsToEmployee(newElement, props);

            SqlCommand cmd = new SqlCommand("spCrReEmployee", Program.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cache.Add(newElement);

            return true;
        }

        public Employee Read(int Id)
        {
            Employee result = cache.Find(e => e.Id == Id);
            if (result == null)
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                result = Program.connection.QuerySingleOrDefault<Employee>("SELECT Id, Country, City, ZIP, Street, CreationTime FROM viEmployee WHERE Id=@Id", param);
                if (result == null)
                {
                    return result;
                }
                cache.Add(result);
            }
            return result;
        }

        public List<Employee> ReadAll()
        {
            return cache;
        }
        public bool Update(int Id, string[] props)
        {
            Employee element = cache.Find(e => e.Id == Id);
            if (element == null)
            {
                return false;
            }
            MapPropsToEmployee(element, props);

            return true;
        }

        public bool Delete(int Id)
        {
            Employee element = cache.Find(e => e.Id == Id);
            if (element != null)
            {
                cache.Remove(element);
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", Program.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                var result = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                cmd.ExecuteNonQuery();
                return result.Value != null;
            }
            return false;
        }

        private bool MapPropsToEmployee(Employee Employee, string[] args)
        {
            if (Employee == null)
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
                        case "FirstName":
                            Employee.FirstName = propValue;
                            break;
                        case "LastName":
                            Employee.LastName = propValue;
                            break;
                        case "Gender":
                            Employee.Gender = propValue;
                            break;
                        case "BirtDate":
                            Employee.BirthDate = DateTime.Parse(propValue);
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
