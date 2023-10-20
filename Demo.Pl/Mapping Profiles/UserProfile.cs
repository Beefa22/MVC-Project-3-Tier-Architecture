using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Demo.Pl.Mapping_Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
