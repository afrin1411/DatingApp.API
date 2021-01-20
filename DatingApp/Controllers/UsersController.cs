using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        //{
        //    var users = await _userRepository.GetUserAsync();
        //    var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
        //    return Ok(usersToReturn);

        //}

        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            var users = await _userRepository.GetUserAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            // return Ok(usersToReturn);

            return usersToReturn;

        }

        //apio/user/id
        [AllowAnonymous]
        [HttpGet("{name}")]     
        public async Task<ActionResult<MemberDto>> GetUser(string name)
        {
            var user= await _userRepository.GetUserByNameAsync(name);
            var userToReturn = _mapper.Map<MemberDto>(user);
            return userToReturn;
           
        }
    }
}
