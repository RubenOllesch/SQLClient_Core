using System;
using System.Collections.Generic;
using System.Data;
using SQLClient.Models;

namespace SQLClient
{
    class Display
    {
        static readonly int consoleHeight = Console.WindowHeight;
        static readonly int consoleWidth = Console.WindowWidth;

        public static void PrintHelp()
        {
            Console.WriteLine("Available commands:");

            PrintHorizontalLine('─');

            Console.WriteLine(" help | h");
            Console.WriteLine("     lists all available commands");

            PrintHorizontalLine('-');

            Console.WriteLine(" view <table>");
            Console.WriteLine("     prints out the content of the table");

            PrintHorizontalLine('-');

            Console.WriteLine(" cor <table> [param=value]");
            Console.WriteLine("     @Id is specified:     values will be updated");
            Console.WriteLine("     @Id is not specified: new entry will be created");

            PrintHorizontalLine('-');

            Console.WriteLine(" exit | q");
            Console.WriteLine("     exit the application");

            PrintHorizontalLine('─');
        }

        public static void PrintDefault()
        {
            Console.WriteLine("Unknown command. Type 'help' for a list of commands");
        }

        public static void Print(Address address)
        {
            if (address == null)
            {
                Console.WriteLine("No Results");
                return;
            }
            PrintHorizontalLine('─');

            List<string> rowValues = new List<string>
            {
                address.Country,
                address.City,
                address.ZIP,
                address.Street,
                address.CreationTime.ToString()
            };
            PrintRow(rowValues);

            PrintHorizontalLine('─');
        }

        public static void Print(Company company)
        {
            if (company == null)
            {
                Console.WriteLine("No Results");
                return;
            }
            PrintHorizontalLine('─');

            List<string> rowValues = new List<string>
            {
                company.CompanyName,
                company.CreationTime.ToString()
            };
            PrintRow(rowValues);

            PrintHorizontalLine('─');
        }


        public static void Print(List<Address> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("No Results");
                return;
            }
            PrintHorizontalLine('─');
            foreach (Address address in list)
            {
                Print(address);
            }
            PrintHorizontalLine('─');
        }

        public static void Print(List<Company> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("No Results");
                return;
            }
            PrintHorizontalLine('─');
            foreach (Company company in list)
            {
                Print(company);
            }
            PrintHorizontalLine('─');
        }

        private static void PrintRow(List<string> items)
        {
            int columnWidth = consoleWidth / items.Count;
            string row = "";
            foreach (string column in items)
            {
                row += PaddStringLeft(column, columnWidth);
            }
            Console.WriteLine(row);
        }

        private static string PaddStringLeft(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width);
            }
        }

        private static void PrintHorizontalLine(char type)
        {
            Console.WriteLine(new String(type, consoleWidth));
        }
    }
}