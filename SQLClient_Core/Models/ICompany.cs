using System;
using System.Data;

namespace SQLClient.Models
{
    public interface ICompany
    {
        void Create(string name);
        DataTable Read(int id);
        void Update(int id, string name);
        void Delete(int id);
    }
}
