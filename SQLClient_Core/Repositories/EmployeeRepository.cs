using SQLClient.Models;

namespace SQLClient.Repositories
{
    class EmployeeRepository : Repository<Entity>
    {
        public void Update(int Id, Employee newProps)
        {
            Employee element = (Employee)context.Find(e => e.Id == Id);

            element.FirstName = newProps.FirstName ?? element.FirstName;
            element.LastName = newProps.LastName ?? element.LastName;
            element.Gender = newProps.Gender ?? element.Gender;
            element.BirthDate = newProps.BirthDate ?? element.BirthDate;
            element.CreationTime = newProps.CreationTime ?? element.CreationTime;
        }
    }
}
