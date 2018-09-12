using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace SQLClient
{
    class Program
    {  
		static string connString = "Server=tappqa;Database=Training-RE-CompanyDB;Integrated Security=true";
        static SqlConnection connection = new SqlConnection(connString);
        static string currentTable = "";

        public static void Main(string[] args)
        {
            try
            {
                connection.Open();
            }
            catch
            {
                throw new Exception("Could not establish a connection");
            }
            while (true)
            {
                string rawInput = Console.ReadLine();
                string[] arguments = rawInput.Split(" ");

                switch (arguments[0]) {
                    case "table":
                        Table(arguments.Skip(1).ToArray());
                        break;
                    case "create":
                        Create(arguments.Skip(1).ToArray());
                        break;
                    case "read":
                        break;
                    case "update":
                        break;
                    case "delete":
                        break;
                }
            }

        }

        private static void Table(string[] args)
        {
            // No Table specified
            if (args.Length < 1)
            {
                Console.WriteLine($"Current Table: {currentTable}");
                return;
            }
            // Table specified
            string table = args[0];
            DataTable dt = connection.GetSchema("Tables");

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

        private static void Create(string[] args)
        {
            if (currentTable == "")
            {
                Console.WriteLine("Please select a Table first");
                return;
            }
            
        }
    }
}
