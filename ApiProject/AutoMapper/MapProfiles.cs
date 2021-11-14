using ApiProject.Model;
using AutoMapper;
using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.AutoMapper
{
    /// <summary>
    /// https://code-maze.com/automapper-net-core/
    /// </summary>
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Employee, EmployeeModel>()
                .ForMember(dest => dest.Department, opts => opts.MapFrom(src => src.Department))
                .ForPath(dest => dest.Department.Name, opts => opts.MapFrom(src => src.Department.DepartmentName));

            CreateMap<Department, DepartmentModel>()
                .ForMember(dest =>
                    dest.Name,
                    opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.TotalEmployees, opt => opt.MapFrom(src => src.Employees.Count));

            CreateMap<Department, DepartmentEmpsModel>()
                .ForMember(dest =>
                    dest.Name,
                    opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.EmployeeModels, opts => opts.MapFrom(src => src.Employees));
        }
    }
}
