using System;

namespace SQLClient.Models
{
    class Department : Entity
    {
        public string DepartmentName { get; set; }
        public int? CompanyId { get; set; }
        public int? ManagerId { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
