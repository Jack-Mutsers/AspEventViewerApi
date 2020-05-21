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
using Newtonsoft.Json;
using static Logics.ObjectSorter;

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

        [HttpGet("GetAllEvents")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Event> @events = _repository.Event.GetAllEvents();

                _logger.LogInfo($"Returned all Events from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(@events);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("GetAllInOrder")]
        public IActionResult GetAllInOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                IEnumerable<Event> @events = _repository.Event.GetAllEvents();

                IEnumerable<Event> sorted_events = @events.AsQueryable().OrderByField(orderRequest);

                _logger.LogInfo($"Returned all Events from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(sorted_events);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetByIdWithDetails(int id)
        {
            try
            {
                var @event = _repository.Event.GetByIdWithDetails(id);

                if (@event == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return NotFound();
                }


                _logger.LogInfo($"Returned Event with id: {id}");

                EventDto eventDto = _mapper.Map<EventDto>(@event);

                DatePlanningController datePlanningController = new DatePlanningController(_logger, _repository, _mapper);

                return Ok(eventDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
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


                _logger.LogInfo($"Returned Event with id: {id}");

                EventDto eventDto = _mapper.Map<EventDto>(@event);

                return Ok(eventDto);

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

                return Ok(true);
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

                IEnumerable<EventGenre> eventGenres = _repository.EventGenre.GetByEvent(DataEntity.id);
                foreach (EventGenre eventGenre in eventGenres)
                {
                    _repository.EventGenre.DeleteEventGenre(eventGenre);
                }

                _repository.Event.UpdateEvent(DataEntity);
                _repository.Save();

                return Ok(true);
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

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}