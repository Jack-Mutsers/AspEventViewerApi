using AspEventVieuwerAPI.Authentication;
using AutoMapper;
using Contracts.Logger;
using Contracts.Logic;
using Contracts.Repository;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspEventVieuwerAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private ILoggerManager _logger;
        private IPreferenceLogic _preferenceLogic;
        private IMapper _mapper;

        public PreferenceController(ILoggerManager logger, IPreferenceLogic preferenceLogic)
        {
            _logger = logger;
            _preferenceLogic = preferenceLogic;
        }

        [HttpGet("{id}")]
        public IActionResult GetByUser(int id)
        {
            try
            {
                var preference = _preferenceLogic.GetPreferenceByUser(id);

                if (preference == null)
                {
                    return NotFound();
                }
                 
                return Ok(preference);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPreferenceById(int id)
        {
            try
            {
                var preference = _preferenceLogic.GetById(id);

                if (preference == null)
                {
                    return NotFound();
                }
                
                return Ok(preference);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreatePreference([FromBody]PreferenceForCreationDto preference)
        {
            try
            {
                if (preference == null)
                {
                    _logger.LogError("Preference object sent from client is null.");
                    return BadRequest("Preference object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Preference object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _preferenceLogic.Create(preference);

                return Ok("Preference is created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePreference([FromBody]List<PreferenceForCreationDto> preferences, int id)
        {
            try
            {
                if (preferences == null)
                {
                    _logger.LogError("Preference object sent from client is null.");
                    return BadRequest("Preference object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Preference object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var success = _preferenceLogic.UpdateByUser(id, preferences);
                if (!success)
                {
                    return NotFound();
                }

                return Ok("Preference is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeletePreference(int id)
        {
            try
            {
                bool preference = _preferenceLogic.Delete(id);

                if (!preference)
                {
                    return NotFound();
                }

                return Ok("Preference is deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}