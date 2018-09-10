using System;
using System.Data;

namespace SQLClient.Models
{
	public interface IEmployee
    {
        void Create(string firstName, string lastName, int gender, DateTime birthDate, int departmentId);
        DataTable Read(int id);
        void Update(int id, string firstName, string lastName, int gender, DateTime birthDate, int departmentId);
        void Delete(int id);
    }
}
