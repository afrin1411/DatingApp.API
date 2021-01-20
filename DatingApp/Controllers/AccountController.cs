using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using DatingApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{

    public class AccountController : BaseApiController
    {
       // private readonly DataContext _context;
        private readonly ITokenService _tokeService;
        private readonly IAccountRepository _accountRepository;

        //public AccountController(DataContext context, ITokenService tokenService)
        //{
        //    _context = context;
        //    _tokeService = tokenService;
        //}


        public AccountController(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _tokeService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //if (await UserExists(registerDto.UserName)) return BadRequest("User name is taken");
            if (await _accountRepository.UserExists(registerDto.UserName)) return BadRequest("User name is taken");

            using var hmac= new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            //_context.AppUsers.Add(user);
            //await _context.SaveChangesAsync();

            await _accountRepository.SaveUser(user);
            var userToken=_tokeService.CreateToken(user);
            return new UserDto
            {
                UserName = user.UserName,
                Token = userToken
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //var user =await  _context.AppUsers.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
            var user = await _accountRepository.GetLoginuser(loginDto.UserName.ToLower());
            if (user == null)
                return Unauthorized("Invalid User Name");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

           for(var i=0; i< computedPasswordHash.Length;i++)
            {
                if (computedPasswordHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }
            var userToken = _tokeService.CreateToken(user);
            return new UserDto
            {
                UserName = user.UserName,
                Token = userToken
            };

        }

        //public async Task<bool> UserExists(string userName)
        //{
        //  return  await _context.AppUsers.AnyAsync(x => x.UserName == userName.ToLower());
        //}
    }
}
