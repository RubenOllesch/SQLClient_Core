using System;

namespace SQLClient.Models
{
    class Employee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreationTime { get; set; }

    }
}
