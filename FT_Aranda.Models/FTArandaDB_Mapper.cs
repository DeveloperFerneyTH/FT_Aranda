using FT_Aranda.Models.Entities;
using FT_Aranda.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FT_Aranda.Models
{
    public class FTArandaDB_Mapper : AutoMapper.Profile
    {
        public FTArandaDB_Mapper()
        {
            CreateMap<Login, LoginViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
