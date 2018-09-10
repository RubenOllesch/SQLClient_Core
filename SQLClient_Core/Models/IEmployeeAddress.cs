using System;
using System.Data;

namespace SQLClient.Models
{
    public interface IEmployeeAddress
    {
        void Create(int employeeId, int addressId);
        DataTable Read(int id);
        void Delete(int employeeId, int addressId);
    }
}
