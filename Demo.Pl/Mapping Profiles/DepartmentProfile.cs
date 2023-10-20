using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;

namespace Demo.Pl.Mapping_Profiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
