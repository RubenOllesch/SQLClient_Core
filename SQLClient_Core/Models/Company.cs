using System;

namespace SQLClient.Models
{
    class Company : Entity
    {
        public string CompanyName { get; set; }
        public DateTime? CreationTime { get; set; }

    }
}
