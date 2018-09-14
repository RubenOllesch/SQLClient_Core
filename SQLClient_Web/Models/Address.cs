using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLClient_Web.Models
{
    public class Address
    {
        public int? Id { get; set; }
        public string Country { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
