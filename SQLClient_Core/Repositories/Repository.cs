using System.Collections.Generic;
using System.Reflection;

namespace SQLClient.Repositories
{
    class Repository<E> : IRepository<E> where E : Models.Entity
    {
        protected readonly List<E> context = new List<E>();

        public void Create(E entity)
        {
            context.Add(entity);
        }

        public E Read(int Id)
        {
            return context.Find(e => e.Id == Id);
        }

        public virtual void Update(int Id, E newProps)
        {
        }
        
        public void Delete(int Id)
        {
            E element = context.Find(e => e.Id == Id);
            context.Remove(element);
        }

    }
}
