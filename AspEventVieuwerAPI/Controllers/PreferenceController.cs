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
    public class PreferenceController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public PreferenceController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetPreferenceByUser")]
        public IActionResult GetByUser(int id)
        {
            try
            {
                var preference = _repository.Preference.GetPreferenceByUser(id);

                if (preference == null)
                {
                    _logger.LogError($"Preferences with user id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                 
                _logger.LogInfo($"Returned Preferences with user id: {id}");

                var Result = _mapper.Map<IEnumerable<PreferenceDto >> (preference);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPreferenceByUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetPreferenceById")]
        public IActionResult GetPreferenceById(int id)
        {
            try
            {
                var preference = _repository.Preference.GetById(id);

                if (preference == null)
                {
                    _logger.LogError($"Preference with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                
                _logger.LogInfo($"Returned Preference with id: {id}");

                var Result = _mapper.Map<PreferenceDto>(preference);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPreferenceById action: {ex.Message}");
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

                var DataEntity = _mapper.Map<Preference>(preference);

                _repository.Preference.CreatePreference(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<PreferenceDto>(DataEntity);

                return Ok("Preference is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePreference action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdatePreference([FromBody]ArtistForUpdateDto preference)
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

                var DataEntity = _repository.Preference.GetById(preference.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Preference with id: {preference.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(preference, DataEntity);

                _repository.Preference.UpdatePreference(DataEntity);
                _repository.Save();

                return Ok("Preference is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePreference action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeletePreference(int id)
        {
            try
            {
                var preference = _repository.Preference.GetById(id);
                if (preference == null)
                {
                    _logger.LogError($"Preference with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Preference.DeletePreference(preference);
                _repository.Save();

                return Ok("Preference is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePreference action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}