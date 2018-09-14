using System.Collections.Generic;

namespace SQLClient_Web.Repositories
{
    public interface IRepository<T> where T : class
    {
        int Create(T obj);
        T Read(int id);
        IEnumerable<T> ReadAll();
        bool Update(T obj);
        bool Delete(int id);
    }
}