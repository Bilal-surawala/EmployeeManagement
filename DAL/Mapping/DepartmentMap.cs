using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(x => x.Id);
            Department[] departments = new Department[]
            { 
                new Department { Id = 1, DepartmentName = "IT" }, 
                new Department { Id = 2, DepartmentName = "HR" },
                new Department { Id = 3, DepartmentName = "Payroll" },
                new Department { Id = 4, DepartmentName = "Admin" }
            };
            builder.HasData(departments);
        }
    }
}
