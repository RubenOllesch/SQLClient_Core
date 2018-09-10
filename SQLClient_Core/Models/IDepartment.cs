using System;
using System.Data;

namespace SQLClient.Models
{
    public interface IDepartment
    {
        void Create(string departmentName, int managerId);
        DataTable Read(int id);
        void Update(int id, string departmentName, int managerId);
        void Delete(int id);
    }
}
