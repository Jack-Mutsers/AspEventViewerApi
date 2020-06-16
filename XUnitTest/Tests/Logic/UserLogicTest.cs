using AspEventVieuwerAPI.Controllers;
using Contracts.Logic;
using Contracts.Repository;
using Entities.DataTransferObjects;
using Entities.Models;
using Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Xunit;
using XUnitTest.Resources;

namespace XUnitTest.Tests
{
    public class UserLogicTest
    {
        private ControllerRequrements requrements;
        private IUserLogic _userLogic;
        private IPreferenceLogic _preferenceLogic;
        private IPreferenceRepository _PreferenceRepository;
        private IUserRepository _userRepository;

        public UserLogicTest()
        {
            requrements = new ControllerRequrements();
            //_controller = new GenreController(requrements.logger, requrements.repository.Genre, requrements.repository.EventGenre, requrements.repository.ArtistGenre, requrements.mapper);
            
        }

        private void Setup_succes()
        {
            /* setup */
            IUserRepository userRepository = new TestRepository.succes.UserRepository();
            IPreferenceRepository preferenceRepository = new TestRepository.succes.PreferenceRepository();
            _preferenceLogic = new PreferenceLogic(logger: requrements.logger, preferenceRepository: preferenceRepository, mapper: requrements.mapper);
            _userLogic = new UserLogic(logger: requrements.logger, userRepository: userRepository, preferenceLogic: _preferenceLogic, mapper: requrements.mapper);
        }

        private void Setup_Preference_null()
        {
            /* setup */
            IUserRepository userRepository = new TestRepository.succes.UserRepository();
            _userLogic = new UserLogic(logger: requrements.logger, userRepository: userRepository, preferenceLogic: null, mapper: requrements.mapper);
        }

        private void Setup_Error()
        {
            /* setup */
            IUserRepository userRepository = new TestRepository.Error.UserRepository();
            IPreferenceRepository preferenceRepository = new TestRepository.Error.PreferenceRepository();
            _preferenceLogic = new PreferenceLogic(logger: requrements.logger, preferenceRepository: preferenceRepository, mapper: requrements.mapper);
            _userLogic = new UserLogic(logger: requrements.logger, userRepository: userRepository, preferenceLogic: _preferenceLogic, mapper: requrements.mapper);
        }

        private void Setup_Empty()
        {
            /* setup */
            IUserRepository userRepository = new TestRepository.Empty.UserRepository();
            IPreferenceRepository preferenceRepository = new TestRepository.Empty.PreferenceRepository();
            _preferenceLogic = new PreferenceLogic(logger: requrements.logger, preferenceRepository: preferenceRepository, mapper: requrements.mapper);
            _userLogic = new UserLogic(logger: requrements.logger, userRepository: userRepository, preferenceLogic: _preferenceLogic, mapper: requrements.mapper);
        }

        [Fact]
        public void GetUserByLogin_succes()
        {
            /* setup */
            Setup_succes();
            string username = "tuser1";
            string password = "P@ssword1";

            /* Test */
            var user = _userLogic.GetUserByLogin(username, password);

            /* Assert */

            Assert.True(user.GetType() == typeof(UserDto));
            Assert.Equal("test user 1", user.name);
            Assert.Equal(4, user.preference.Count());
            Assert.Equal(1, user.right.id);
        }

        [Fact]
        public void GetUserByLogin_IncorrectPassword()
        {
            /* setup */
            Setup_succes();
            string username = "tuser1";
            string password = "hallo";

            /* Test */
            var user = _userLogic.GetUserByLogin(username, password);

            /* Assert */

            Assert.Null(user);
        }

        [Fact]
        public void GetUserByLogin_NoLogin()
        {
            /* setup */
            Setup_succes();
            string username = "";
            string password = "";

            /* Test */
            var user = _userLogic.GetUserByLogin(username, password);

            /* Assert */

            Assert.Null(user);
        }

        [Fact]
        public void GetUserByLogin_exeption()
        {
            /* setup */
            Setup_Empty();
            string username = "tuser1";
            string password = "P@ssword1";

            /* Test */
            Assert.Throws<Exception>(() => _userLogic.GetUserByLogin(username, password));
        }

        [Fact]
        public void GetById_succes()
        {
            /* setup */
            Setup_succes();
            int user_id = 1;

            /* Test */
            var user = _userLogic.GetById(user_id);

            /* Assert */

            Assert.True(user.GetType() == typeof(UserDto));
            Assert.Equal("test user 1", user.name);
            Assert.Equal(4, user.preference.Count());
            Assert.Equal(1, user.right.id);
        }

        [Fact]
        public void GetById_IncorrectId()
        {
            /* setup */
            Setup_succes();
            int user_id = 10;

            /* Test */
            var user = _userLogic.GetById(user_id);

            /* Assert */

            Assert.Null(user);
        }

        [Fact]
        public void GetById_exeption()
        {
            /* setup */
            Setup_Empty();
            int user_id = 1;

            /* Test */
            Assert.Throws<Exception>(() => _userLogic.GetById(user_id));
        }

        [Fact]
        public void CreateUser_succes()
        {
            /* setup */
            Setup_succes();
            UserForCreationDto user = new UserForCreationDto()
            {
                name = "jack",
                password = "Password1!",
                username = "jackm",
                right_id = 2
            };

            /* Test */
            UserDto userDto = _userLogic.Create(user);

            /* Assert */
            Assert.True(userDto.GetType() == typeof(UserDto));
            Assert.Equal("jack", userDto.name);
            Assert.Equal(2, userDto.right.id);
        }

        [Fact]
        public void CreateUser_NoName()
        {
            /* setup */
            Setup_succes();
            UserForCreationDto user = new UserForCreationDto()
            {
                name = "",
                password = "Password1!",
                username = "jackm2",
                right_id = 2
            };

            /* Test */
            var userDto = _userLogic.Create(user);

            /* Assert */
            Assert.Null(userDto);

        }

        [Fact]
        public void CreateUser_exeption()
        {
            /* setup */
            Setup_Error();
            UserForCreationDto user = new UserForCreationDto()
            {
                name = "jack",
                password = "Password1!",
                username = "jackm",
                right_id = 2
            };

            /* Test */
            Assert.Throws<Exception>(() => _userLogic.Create(user));
        }

        [Fact]
        public void DeleteUser_succes()
        {
            /* setup */
            Setup_succes();
            int user_id = 1;

            /* Test */
            bool succes = _userLogic.Delete(user_id);

            /* Assert */
            Assert.True(succes);
        }

        [Fact]
        public void DeleteUser_InvalidId()
        {
            /* setup */
            Setup_succes();
            int user_id = 10;

            /* Test */
            bool succes = _userLogic.Delete(user_id);

            /* Assert */
            Assert.False(succes);
        }

        [Fact]
        public void DeleteUser_exeption()
        {
            /* setup */
            Setup_Error();
            int user_id = 2;

            /* Test */
            Assert.Throws<Exception>(() => _userLogic.Delete(user_id));
        }

        [Fact]
        public void DeleteUser_preference_null()
        {
            /* setup */
            Setup_Preference_null();
            int user_id = 3;

            /* Test */
            Assert.Throws<Exception>(() => _userLogic.Delete(user_id));
        }

        [Fact]
        public void UpdateUser_succes()
        {
            /* setup */
            Setup_succes();
            UserForUpdateDto userForUpdate = new UserForUpdateDto()
            {
                id = 1,
                name = "jack",
                password = "Password2!",
                right_id = 2,
                preferences = new List<PreferenceForCreationDto>()
                {
                    new PreferenceForCreationDto(){ genre_id = 7, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 8, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 9, user_id = 1 },
                }
            };

            /* Test */
            bool succes = _userLogic.Update(userForUpdate);

            /* Assert */
            Assert.True(succes);
        }

        [Fact]
        public void UpdateUser_InvalidId()
        {
            /* setup */
            Setup_succes();
            UserForUpdateDto userForUpdate = new UserForUpdateDto()
            {
                id = 0,
                name = "jack",
                password = "Password2!",
                right_id = 2,
                preferences = new List<PreferenceForCreationDto>()
                {
                    new PreferenceForCreationDto(){ genre_id = 7, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 8, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 9, user_id = 1 },
                }
            };

            /* Test */
            bool succes = _userLogic.Update(userForUpdate);

            /* Assert */
            Assert.False(succes);
        }

        [Fact]
        public void UpdateUser_NoPreference()
        {
            /* setup */
            Setup_succes();
            UserForUpdateDto userForUpdate = new UserForUpdateDto()
            {
                id = 0,
                name = "jack",
                password = "Password2!",
                right_id = 2,
                preferences = null
            };

            /* Test */
            bool succes = _userLogic.Update(userForUpdate);

            /* Assert */
            Assert.False(succes);
        }

        [Fact]
        public void UpdateUser_exeption()
        {
            /* setup */
            Setup_Error();
            UserForUpdateDto userForUpdate = new UserForUpdateDto()
            {
                id = 1,
                name = "jack",
                password = "Password2!",
                right_id = 2,
                preferences = new List<PreferenceForCreationDto>()
                {
                    new PreferenceForCreationDto(){ genre_id = 7, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 8, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 9, user_id = 1 },
                }
            };

            /* Test */
            Assert.Throws<Exception>(() => _userLogic.Update(userForUpdate));
        }

        [Fact]
        public void UpdateUser_preference_null()
        {
            /* setup */
            Setup_Preference_null();
            UserForUpdateDto userForUpdate = new UserForUpdateDto()
            {
                id = 1,
                name = "jack",
                password = "Password2!",
                right_id = 2,
                preferences = new List<PreferenceForCreationDto>()
                {
                    new PreferenceForCreationDto(){ genre_id = 7, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 8, user_id = 1 },
                    new PreferenceForCreationDto(){ genre_id = 9, user_id = 1 },
                }
            };

            /* Test */
            Assert.Throws<Exception>(() => _userLogic.Update(userForUpdate));
        }

        
    }
}

/*
        UserDto GetUserByLogin(string username, string password);
        UserDto GetById(int User_id);
        UserDto Create(UserForCreationDto userForCreation);
        bool Update(UserForUpdateDto userForUpdate);
        bool Delete(int id);
*/