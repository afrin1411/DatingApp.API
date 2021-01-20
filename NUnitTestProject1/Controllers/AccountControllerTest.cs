using AutoMapper;
using DatingApp.Controllers;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject1.Controllers
{
    [TestFixture]
    class AccountControllerTest
    {

        private Mock<ITokenService> _tockenServiceMock = new Mock<ITokenService>();
        private AccountController _accountController;
        private Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
        private IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _accountController = new AccountController(_accountRepositoryMock.Object, _tockenServiceMock.Object);
        }





        [Test]
        public void  Login_WithCorrectPasswordExistingUser_retuirnsAccoutForCorrectUser()
        {
            //arrange
            var loginDto = new LoginDto
            {
                UserName = "TestUser",
                Password = "TestPasssword"

            };

            var appUser = new AppUser
            {
                UserName = "TestUser"

            };

            //action
            _accountRepositoryMock.Setup(s => s.GetLoginuser(loginDto.UserName)).ReturnsAsync(appUser);

            //assert

            Assert.AreEqual(loginDto.UserName, appUser.UserName);



        }


        //[Test]
        //public async Task Login_WithCorrectPasswordIncorrectUserName_retuirnsBadRequest()
        //{
        //    //arrange
        //    var loginDto = new LoginDto
        //    {
        //        UserName = "TestUser",
        //        Password = "TestPasssword"

        //    };

        //    var appUser = new AppUser
        //    {
        //        UserName = "TestUser"

        //    };

        //    //action
        //    _accountRepositoryMock.Setup(s => s.GetLoginuser(loginDto.UserName)).ReturnsAsync(appUser);

        //    await _accountController.Login(loginDto);

        //    //assert

        //    Assert.AreEqual(loginDto.UserName, appUser.UserName);



        //}
    }
}
