using AutoMapper;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.Pl.Mapping_Profiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(d=>d.Name,O=>O.MapFrom(S=>S.RoleName))
                .ReverseMap();
        }
    }
}
