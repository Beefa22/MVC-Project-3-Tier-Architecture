using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demo.Pl.Mapping_Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
