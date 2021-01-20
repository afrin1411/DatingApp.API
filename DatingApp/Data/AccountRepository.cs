using AutoMapper;
using DatingApp.DTOs;
using DatingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetLoginuser(string userName)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(x => x.UserName == userName.ToLower());
        }

        public async Task SaveUser(AppUser user)
        {
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _context.AppUsers.AnyAsync(x => x.UserName == userName.ToLower());
        }


    }
}
