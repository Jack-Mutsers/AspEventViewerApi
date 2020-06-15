﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspEventVieuwerAPI.Authentication;
using AutoMapper;
using Contracts;
using Contracts.Logger;
using Contracts.Repository;
using Entities.DataTransferObjects;
using Entities.Models;
using Logics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspEventVieuwerAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILoggerManager _logger;
        private IUserRepository _repository;
        private IMapper _mapper;

        public UserController(ILoggerManager logger, IUserRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _repository.GetById(id);

                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned User with id: {id}");

                var Result = _mapper.Map<UserDto>(user);
                return Ok(Result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Login")]
        public IActionResult GetUserByLogin([FromBody]UserForCreationDto user)
        {
            try
            {
                var userData = _repository.GetUserByLogin(user.username, user.password);

                if (userData == null)
                {
                    _logger.LogError($"Failed loggin attempt with username: {user.username}");
                    return NotFound(false);
                }

                Hasher hasher = new Hasher();
                bool valid = hasher.ValidatePassword(userData, user.password);
                if (valid == false)
                {
                    _logger.LogError($"Incorect password for account with username: {user.username}");
                    return NotFound(false);
                }

                _logger.LogInfo($"Returned User with id: {userData.id}");

                var Result = _mapper.Map<UserDto>(userData);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByLogin action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Register")]
        public IActionResult CreateUser([FromBody]UserForCreationDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User object sent from client.");
                    return BadRequest("Invalid model object");
                }

                User userCheck = _repository.GetUserByLogin(user.username, "");
                if (userCheck != null)
                {
                    _logger.LogError("Username is already in use");
                    return Problem("Username already exists");
                }

                Hasher hasher = new Hasher();
                user.password = hasher.HashPassword(user.password);

                var DataEntity = _mapper.Map<User>(user);

                _repository.Create(DataEntity);
                _repository.Save();

                //var createdEntity = _mapper.Map<UserDto>(DataEntity);

                //return Ok("User is created");
                return GetUserById(DataEntity.id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody]UserForUpdateDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User object sent from client.");
                    return BadRequest("Invalid model object");
                }

                Hasher hasher = new Hasher();
                user.password = hasher.HashPassword(user.password);

                var DataEntity = _repository.GetById(user.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"User with id: {user.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(user, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                return Ok("User is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Delete(user);
                _repository.Save();

                return Ok("User is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}