using SQLClient.Models;

namespace SQLClient.Repositories
{
    class DepartmentRepository : Repository<Entity>
    {
        public void Update(int Id, Department newProps)
        {
            Department element = (Department) context.Find(e => e.Id == Id);

            element.DepartmentName = newProps.DepartmentName ?? element.DepartmentName;
            element.CompanyId = newProps.CompanyId ?? element.CompanyId;
            element.ManagerId = newProps.ManagerId ?? element.ManagerId;
            element.CreationTime = newProps.CreationTime ?? element.CreationTime;
        }
    }
}
