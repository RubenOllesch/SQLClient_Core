using System;
using System.Data;

namespace SQLClient.Models
{
    public interface IAddress
    {
        void Create(string country, string city, string zip, string street);
        DataTable Read(int id);
        void Update(int id, string country, string city, string zip, string street);
        void Delete(int id);
    }
}
