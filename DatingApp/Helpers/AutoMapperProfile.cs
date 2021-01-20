using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
            src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photos, PhotosDto>();

            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, RegisterDto>();
            CreateMap<AppUser, LoginDto>();
        }
    }
}
