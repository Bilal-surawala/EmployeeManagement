using System.Collections.Generic;

namespace ApiProject.Model
{
    public class DepartmentEmpsModel : DepartmentModel
    {
        public IEnumerable<EmployeeModel> EmployeeModels { get; set; }
    }
}
