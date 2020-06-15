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
using Logics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Logics.EventSorter;

namespace AspEventVieuwerAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private ILoggerManager _logger;
        private IEventRepository _repository;
        private IEventGenreRepository _EventGenreRepository;
        private IMapper _mapper;

        public EventController(ILoggerManager logger, IEventRepository repository, IEventGenreRepository eventGenreRepository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _EventGenreRepository = eventGenreRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllEvents")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Event> @events = _repository.GetAllEvents();

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

        [HttpGet("GetAllActiveEvents")]
        public IActionResult GetAllActiveEvents()
        {
            try
            {
                IEnumerable<Event> @events = _repository.GetAllActiveEvents();

                _logger.LogInfo($"Returned all active Events from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(@events);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllActiveEvents action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("SortEventData")]
        public IActionResult SortEventData([FromBody] OrderRequest orderRequest)
        {
            try
            {
                IEnumerable<Event> sorted_events = null;

                if(orderRequest.FieldName == "name")
                    sorted_events =_repository.GetSortedByName(orderRequest.Ascending);

                else if (orderRequest.FieldName == "startdate")
                    sorted_events = GetEventByStartDate(orderRequest.Ascending);

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
                var @event = _repository.GetByIdWithDetails(id);

                if (@event == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return NotFound();
                }


                _logger.LogInfo($"Returned Event with id: {id}");

                EventDto eventDto = _mapper.Map<EventDto>(@event);

                //DatePlanningController datePlanningController = new DatePlanningController(_logger, _repository, _mapper);

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
                var @event = _repository.GetById(id);

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

        [HttpGet("GetAllByGenre/{genre_id}")]
        public IActionResult GetAllByGenre(int genre_id)
        {
            try
            {
                IEnumerable<Event> @events = genre_id > 0 ?
                    _EventGenreRepository.GetEventsByGenre(genre_id) : 
                    _repository.GetAllActiveEvents();

                _logger.LogInfo($"Returned all Events with genre id: {genre_id} from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(@events);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllByGenre action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("GetbyName/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                IEnumerable<Event> events = name == "" ? 
                    _repository.GetAllActiveEvents() :
                    _repository.GetByName(name);

                _logger.LogInfo($"Returned all Events with names that contain: {name} from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(events);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        public IEnumerable<Event> GetEventByStartDate(bool ascending)
        {
            try
            {
                IEnumerable<Event> @events = _repository.GetSortedByStartDate(ascending);

                _logger.LogInfo($"Returned all Events from database.");

                List<Event> eventsList = new List<Event>();
                foreach(Event @event in @events.ToList())
                {
                    IEnumerable<EventGenre> eventGenres = _EventGenreRepository.GetByEventWithDetails(@event.id);
                    @event.genre = _mapper.Map<ICollection<EventGenre>>(eventGenres);
                    eventsList.Add(@event);
                }

                return eventsList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return new List<Event>();
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

                _repository.Create(DataEntity);
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

                var DataEntity = _repository.GetById(@event.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Event with id: {@event.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(@event, DataEntity);

                IEnumerable<EventGenre> eventGenres = _EventGenreRepository.GetByEvent(DataEntity.id);
                foreach (EventGenre eventGenre in eventGenres)
                {
                    _EventGenreRepository.Delete(eventGenre);
                }

                _repository.Update(DataEntity);
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
                var @event = _repository.GetById(id);
                if (@event == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Delete(@event);
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