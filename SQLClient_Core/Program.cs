using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace SQLClient
{
    class Program
    {  
		private static readonly string connString = "Server=tappqa;Database=Training-RE-CompanyDB;Integrated Security=true";
        public static readonly SqlConnection connection = new SqlConnection(connString);

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
            Controller controller = new Controller();
            while (true)
            {
                string rawInput = Console.ReadLine();
                string[] arguments = rawInput.Split(" ");

                switch (arguments[0]) {
                    case "table":
                        controller.Table(arguments.Skip(1).ToArray());
                        break;
                    case "create":
                        controller.Create(arguments.Skip(1).ToArray());
                        break;
                    case "read":
                        controller.Read(arguments.Skip(1).ToArray());
                        break;
                    case "update":
                        break;
                    case "delete":
                        controller.Delete(arguments.Skip(1).ToArray());
                        break;
                    default:
                        break;
                }
            }

        }

        
    }
}
