using DatingApp.Controllers;
using NUnit.Framework;
using Moq;
using DatingApp.Interfaces;
using AutoMapper;
using System.Threading.Tasks;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject1
{

    [TestFixture]
    public class UserControllerTest
    {
        private UsersController _usersController;
        private Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();

        //private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private IMapper _mapper;

     



        //public UserControllerTest()
        //{

        //    _mapperMock = new Mock<IMapper>();
        //    _usersController = new UsersController(_userRepoMock.Object, _mapperMock.Object);



        //}

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
            _usersController = new UsersController(_userRepoMock.Object, _mapper);

        }

        [Test]
        public async Task GetUsersTest_ReurnUser_By_Name()
        {
            //Arrange
           // var id = 0;
            var userName = "lisa";

            var userDto = new AppUser
            {
               // Id = id,
                UserName = userName
            };

          

           _userRepoMock.Setup(x => x.GetUserByNameAsync(userName)).ReturnsAsync(userDto);
          // _mapperMock.Setup(x => x.Map<AppUser, MemberDto(userDto)>(It.IsAny<AppUser>())).Returns(memberDto);

            //Act
            var user = await _usersController.GetUser(userName);

            //Assert

            Assert.AreEqual(userName,user.Value.UserName);
        }



        [Test]
        public async Task GetUsersTest_ReurnUser_By_Id()
        {
            //Arrange
            // var id = 0;
            var userName = "lisa";

            var userDto = new AppUser
            {
                // Id = id,
                UserName = userName
            };



            _userRepoMock.Setup(x => x.GetUserByNameAsync(userName)).ReturnsAsync(userDto);
            // _mapperMock.Setup(x => x.Map<AppUser, MemberDto(userDto)>(It.IsAny<AppUser>())).Returns(memberDto);

            //Act
            var user = await _usersController.GetUser(userName);

            //Assert

            Assert.AreEqual(userName, user.Value.UserName);
        }


        [Test]
        public async Task GetUsersTest_ReurnAllUsers()
        {
            var userDto = new List<AppUser>
            {
               //new AppUser{ UserName="Test User 1"},
                 new AppUser{ UserName="Test User 2"}
            };

            _userRepoMock.Setup(x => x.GetUserAsync()).ReturnsAsync(userDto);

            //Action
            var actualUsers = await _usersController.GetUsers();
            var ExpectedUsersToReturn = _mapper.Map<IEnumerable<MemberDto>>(userDto);

            //Assert

            Assert.AreEqual(userDto, actualUsers);
           
        }
    }
}