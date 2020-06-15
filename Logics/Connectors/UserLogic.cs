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

namespace Logics
{
    public class UserLogic : IUserLogic
    {
        private ILoggerManager _logger;
        private IUserRepository _repository;
        private IMapper _mapper;

        public UserLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new UserRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(UserForCreationDto userForCreation)
        {
            try
            {
                User DataEntity = _mapper.Map<User>(userForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
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
            throw new NotImplementedException();
        }

        public UserDto GetUserByLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserForUpdateDto userForUpdate)
        {
            try
            {
                var userDto = GetById(userForUpdate.id);

                if (userDto == null)
                {
                    return false;
                }

                User DataEntity = _mapper.Map<User>(userDto);

                _mapper.Map(userForUpdate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated user with id: {DataEntity.id}");

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
