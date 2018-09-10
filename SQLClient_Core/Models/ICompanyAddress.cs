using System;
using System.Data;

namespace SQLClient.Models
{
    public interface ICompanyAddress
    {
        void Create(int companyId, int addressId);
        DataTable Read(int id);
        void Delete(int companyId, int addressId);
    }
}
