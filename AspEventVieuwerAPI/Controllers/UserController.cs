using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspEventVieuwerAPI.Authentication;
using AutoMapper;
using Contracts;
using Contracts.Logger;
using Contracts.Logic;
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
        private IUserLogic _userLogic;

        public UserController(ILoggerManager logger, IUserLogic userLogic)
        {
            _logger = logger;
            _userLogic = userLogic;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userLogic.GetById(id);

                if (user == null)
                {
                    return NotFound();
                }
                
                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Login")]
        public IActionResult GetUserByLogin([FromBody]UserForCreationDto user)
        {
            try
            {
                var userData = _userLogic.GetUserByLogin(user.username, user.password);

                if (userData == null)
                {
                    return NotFound(false);
                }

                return Ok(userData);
            }
            catch (Exception ex)
            {
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

                UserDto userDto = _userLogic.Create(user);
                if (userDto == null)
                {
                    return Problem("Username already exists");
                }
                
                return GetUserById(userDto.id);
            }
            catch (Exception ex)
            {
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

                bool succes = _userLogic.Update(user);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("User is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                bool succes = _userLogic.Delete(id);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("User is delted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}