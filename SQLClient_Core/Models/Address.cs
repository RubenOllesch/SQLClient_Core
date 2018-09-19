using System;

namespace SQLClient.Models
{
    public class Address : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Street { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
