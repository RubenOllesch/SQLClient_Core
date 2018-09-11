using SQLClient.Models;

namespace SQLClient.Repositories
{
    class AddressRepository : Repository<Entity>
    {
        public void Update(int Id, Address newProps)
        {
            Address element = (Address) context.Find(e => e.Id == Id);

            element.Country = newProps.Country ?? element.Country;
            element.City = newProps.City ?? element.City;
            element.ZIP = newProps.ZIP ?? element.ZIP;
            element.Street = newProps.Street ?? element.Street;
        }
    }
}
