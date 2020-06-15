using System;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace AspEventVieuwerAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class EventDateController : ControllerBase
    {
        private ILoggerManager _logger;
        private IEventDateRepository _repository;
        private IMapper _mapper;

        public EventDateController(ILoggerManager logger, IEventDateRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetEventDateById/{id}")]
        public IActionResult GetEventDateById(int id)
        {
            try
            {
                var eventDate = _repository.GetById(id);

                if (eventDate == null)
                {
                    _logger.LogError($"EventDate with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned EventDate with id: {id}");

                var Result = _mapper.Map<EventDateDto>(eventDate);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventDateById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetEventDateByIdWithDetails(int id)
        {
            try
            {
                var eventDate = _repository.GetByIdWithDetails(id);

                if (eventDate == null)
                {
                    _logger.LogError($"EventDate with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned EventDate with id: {id}");

                var Result = _mapper.Map<EventDateDto>(eventDate);

                /*ArtistController artistController = new ArtistController(_logger, new ArtistRepository(_repository.RepositoryContext), _mapper);*/ //temp
                //Result.artists = artistController.GetArtistsByEventDate(Result.id);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventDateById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateEventDate([FromBody]EventDateForCreationDto eventDate)
        {
            try
            {
                if (eventDate == null)
                {
                    _logger.LogError("EventDate object sent from client is null.");
                    return BadRequest("EventDate object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid EventDate object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var DataEntity = _mapper.Map<EventDate>(eventDate);

                _repository.Create(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<EventDateDto>(DataEntity);

                return Ok("EventDate is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateEventDate([FromBody]EventDateForUpdateDto eventDate)
        {
            try
            {
                if (eventDate == null)
                {
                    _logger.LogError("EventDate object sent from client is null.");
                    return BadRequest("EventDate object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid EventDate object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var DataEntity = _repository.GetById(eventDate.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"EventDate with id: {eventDate.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(eventDate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                return Ok("EventDate is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEventDate(int id)
        {
            try
            {
                var eventDate = _repository.GetById(id);
                if (eventDate == null)
                {
                    _logger.LogError($"EventDate with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Delete(eventDate);
                _repository.Save();

                return Ok("EventDate is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}