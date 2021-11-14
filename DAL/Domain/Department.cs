using DAL.Data;
using System.Collections.Generic;

namespace DAL.Domain
{
    public class Department : BaseEntity
    {
        public string DepartmentName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
