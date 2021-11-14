using DAL.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Model
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }

        public int Age
        {
            get 
            {
                int age = DateTime.Now.Year - DateOfBrith.Year;
                if (DateTime.Now.DayOfYear < DateOfBrith.DayOfYear)
                    age--;

                return age;
            }
        }

        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }

        public IFormFile File { get; set; }

        public DepartmentModel Department { get; set; }
    }
}
