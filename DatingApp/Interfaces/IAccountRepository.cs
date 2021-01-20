using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.DTOs;
using DatingApp.Entities;

namespace DatingApp.Interfaces
{
    public interface IAccountRepository
    {
        //Task<UserDto> Register(RegisterDto registerDto);
        Task SaveUser(AppUser user);

        Task<bool> UserExists(string userName);

        Task<AppUser> GetLoginuser(string userName);
    }
}
