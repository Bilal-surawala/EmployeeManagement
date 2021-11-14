using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Search(string name, Gender? gender, int departmentId);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> GetEmployeeByEmail(string email);
        /// <summary>
        /// Filters a collection of Employee based on a predicate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>collection of Employee</returns>
        IEnumerable<Employee> GetEmployeesByCustomeFilter(Func<Employee, bool> predicate);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
