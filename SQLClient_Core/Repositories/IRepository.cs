namespace SQLClient.Repositories
{
    interface IRepository<E> where E : Models.Entity
    {
        void Create(E entity);
        E Read(int Id);
        void Update(int Id, E entity);
        void Delete(int Id);
    }
}
