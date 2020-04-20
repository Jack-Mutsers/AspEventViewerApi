using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspEventVieuwerAPI.Authentication;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public UserController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{username,password}", Name = "Login")]
        public IActionResult GetUserByLogin(string username, string password)
        {
            try
            {
                var user = _repository.User.GetUserByLogin(username, password);

                if (user == null)
                {
                    _logger.LogError($"Failed loggin attempt with username: {username} and password: {password}");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned User with id: {user.id}");

                    var Result = _mapper.Map<UserDto>(user);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByLogin action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _repository.User.GetById(id);

                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned User with id: {id}");

                    var Result = _mapper.Map<UserDto>(user);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
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

                var DataEntity = _mapper.Map<User>(user);

                _repository.User.CreateUser(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<UserDto>(DataEntity);

                return Ok("User is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
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

                var DataEntity = _repository.User.GetById(user.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"User with id: {user.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(user, DataEntity);

                _repository.User.UpdateUser(DataEntity);
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
                var user = _repository.User.GetById(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.User.DeleteUser(user);
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