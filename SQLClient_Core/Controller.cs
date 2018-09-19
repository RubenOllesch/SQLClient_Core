using SQLClient.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLClient
{
    class Controller
    {
        string currentTable = null;


        AddressRepository addressRepository = new AddressRepository();
        CompanyRepository companyRepository = new CompanyRepository();
        DepartmentRepository departmentRepository = new DepartmentRepository();
        EmployeeRepository employeeRepository = new EmployeeRepository();
        

        public void Table(string[] args)
        {
            // No Table specified
            if (args.Length < 1)
            {
                Console.WriteLine($"Current Table: {currentTable}");
                return;
            }
            // Table specified
            string table = args[0];
            DataTable dt = Program.connection.GetSchema("Tables");

            // Check if specified Table exists
            foreach (DataRow row in dt.Rows)
            {
                if ((string)row[2] == table)
                {
                    currentTable = table;
                    return;
                }
            }
            // Prints if Table was not found
            Console.WriteLine("This Table does not exist");
        }

        public void Create(string[] args)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                Console.WriteLine("Please select a Table first");
                return;
            }
            switch (currentTable)
            {
                case "Address":
                    addressRepository.Create(args);
                    break;
                case "Company":
                    companyRepository.Create(args);
                    break;
                case "Department":
                    departmentRepository.Create(args);
                    break;
                case "Employee":
                    employeeRepository.Create(args);
                    break;
            }
        }

        public void Read(string[] args)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                Console.WriteLine("Please select a Table first");
                return;
            }
            bool readAll;
            readAll = args[0] == "all";
            Int32.TryParse(args[0], out int Id);
            switch (currentTable)
            {
                case "Address":
                    if (readAll)
                    {
                        Display.Print(addressRepository.ReadAll());
                    }
                    else
                    {
                        Display.Print(addressRepository.Read(Id));
                    }
                    break;
                case "Company":
                    if (readAll)
                    {
                        Display.Print(companyRepository.ReadAll());
                    }
                    else
                    {
                        Display.Print(companyRepository.Read(Id));
                    }
                    break;
                case "Department":

                    break;
                case "Employee":

                    break;
            }
        }

        public void Delete(string[] args)
        {
            if (string.IsNullOrEmpty(currentTable))
            {
                Console.WriteLine("Please select a Table first");
                return;
            }
            Int32.TryParse(args[0], out int Id);
            bool result;
            switch (currentTable)
            {
                case "Address":
                    result = addressRepository.Delete(Id);
                    Console.WriteLine(result ? "Successfully deleted" : "Not found");
                    break;
                case "Company":
                    result = companyRepository.Delete(Id);
                    Console.WriteLine(result ? "Successfully deleted" : "Not found");
                    break;
                case "Department":
                    result = departmentRepository.Delete(Id);
                    Console.WriteLine(result ? "Successfully deleted" : "Not found");
                    break;
                case "Employee":
                    result = employeeRepository.Delete(Id);
                    Console.WriteLine(result ? "Successfully deleted" : "Not found");
                    break;
            }
        }
    }
}
