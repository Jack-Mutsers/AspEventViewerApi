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
    public class EventController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public EventController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var @event = _repository.Event.GetAllEvents();

                _logger.LogInfo($"Returned all Events from database.");

                var Result = _mapper.Map<IEnumerable<EventDto>>(@event);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public IActionResult GetEventById(int id)
        {
            try
            {
                var @event = _repository.Event.GetById(id);

                if (@event == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Event with id: {id}");

                    var Result = _mapper.Map<EventDto>(@event);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody]EventForCreationDto @event)
        {
            try
            {
                if (@event == null)
                {
                    _logger.LogError("Event object sent from client is null.");
                    return BadRequest("Event object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var DataEntity = _mapper.Map<Event>(@event);

                _repository.Event.CreateEvent(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<EventDto>(DataEntity);

                return Ok("Event is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateEvent([FromBody]EventForUpdateDto @event)
        {
            try
            {
                if (@event == null)
                {
                    _logger.LogError("Event object sent from client is null.");
                    return BadRequest("Event object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var DataEntity = _repository.Event.GetById(@event.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Event with id: {@event.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(@event, DataEntity);

                _repository.Event.UpdateEvent(DataEntity);
                _repository.Save();

                return Ok("Event is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                var @event = _repository.Event.GetById(id);
                if (@event == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Event.DeleteEvent(@event);
                _repository.Save();

                return Ok("Event is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}