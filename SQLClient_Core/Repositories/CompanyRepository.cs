using SQLClient.Models;

namespace SQLClient.Repositories
{
    class CompanyRepository : Repository<Entity>
    {
        public void Update(int Id, Company newProps)
        {
            Company element = (Company) context.Find(e => e.Id == Id);

            element.CompanyName = newProps.CompanyName ?? element.CompanyName;
            element.CreationTime = newProps.CreationTime ?? element.CreationTime;
        }
    }
}
