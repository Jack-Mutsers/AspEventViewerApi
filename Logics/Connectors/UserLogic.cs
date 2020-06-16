using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql;
using Contracts.Repository;
using Contracts.Logic;
using Contracts.Logger;
using AutoMapper;
using Entities.DataTransferObjects;
using System.Linq;

namespace Logics
{
    public class UserLogic : IUserLogic
    {
        private ILoggerManager _logger;
        private IUserRepository _repository;
        private IPreferenceLogic _preferenceLogic;
        private IMapper _mapper;

        public UserLogic(ILoggerManager logger, IUserRepository userRepository, IPreferenceLogic preferenceLogic, IMapper mapper)
        {
            _logger = logger;
            _repository = userRepository;
            _preferenceLogic = preferenceLogic;
            _mapper = mapper;
        }

        public UserDto Create(UserForCreationDto userForCreation)
        {
            try
            {
                if (userForCreation.name == "" || userForCreation.password == "" || userForCreation.right_id == 0)
                {
                    _logger.LogError("invalid user data");
                    return null;
                }

                User userCheck = _repository.GetUserByLogin(userForCreation.username, "");
                if (userCheck != null)
                {
                    _logger.LogError("Username is already in use");
                    return null;
                }

                Hasher hasher = new Hasher();
                userForCreation.password = hasher.HashPassword(userForCreation.password);

                var DataEntity = _mapper.Map<User>(userForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return GetById(DataEntity.id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var userDto = GetById(id);
                if (userDto == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return false;
                }

                User user = _mapper.Map<User>(userDto);

                IEnumerable<PreferenceDto> preferences =  _preferenceLogic.GetPreferenceByUser(user.id);

                foreach (PreferenceDto preferenceDto in preferences)
                {
                    _preferenceLogic.Delete(preferenceDto.id);
                }

                _repository.Delete(user);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                throw new Exception();
            }
        }

        public UserDto GetById(int User_id)
        {
            try
            {
                var user = _repository.GetById(User_id);

                if (user == null)
                {
                    _logger.LogError($"User with id: {User_id}, hasn't been found in db.");
                    return null;
                }

                if (user.id == 0)
                {
                    _logger.LogError($"Empty User has been returned from the db");
                    throw new Exception();
                }

                _logger.LogInfo($"Returned User with id: {User_id}");

                var Result = _mapper.Map<UserDto>(user);

                Result.preference = _preferenceLogic.GetPreferenceByUser(Result.id);

                return Result;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                throw new Exception();
            }
        }

        public UserDto GetUserByLogin(string username, string password)
        {
            try
            {
                var userData = _repository.GetUserByLogin(username, password);

                if (userData == null)
                {
                    _logger.LogError($"Failed loggin attempt with username: {username}");
                    return null;
                }

                Hasher hasher = new Hasher();
                bool valid = hasher.ValidatePassword(userData, password);
                if (valid == false)
                {
                    _logger.LogError($"Incorect password for account with username: {username}");
                    return null;
                }

                _logger.LogInfo($"Returned User with id: {userData.id}");

                var Result = _mapper.Map<UserDto>(userData);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByLogin action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Update(UserForUpdateDto userForUpdate)
        {
            try
            {
                Hasher hasher = new Hasher();
                userForUpdate.password = hasher.HashPassword(userForUpdate.password);

                var DataEntity = _repository.GetById(userForUpdate.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"User with id: {userForUpdate.id}, hasn't been found in db.");
                    return false;
                }

                _mapper.Map(userForUpdate, DataEntity);

                bool succes = _preferenceLogic.UpdateByUser(userForUpdate.id, userForUpdate.preferences.ToList());

                if (!succes)
                {
                    _logger.LogError($"Failed to update Preferences of User with id: {userForUpdate.id}");
                    return false;
                }

                _repository.Update(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
