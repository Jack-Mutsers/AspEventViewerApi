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
    public class EventController : ControllerBase
    {
        private ILoggerManager _logger;
        private IEventLogic _eventLogic;
        private IEventGenreLogic _eventGenreLogic;
        private IMapper _mapper;

        public EventController(ILoggerManager logger, IEventLogic eventLogic, IEventGenreLogic eventGenreLogic, IMapper mapper)
        {
            _logger = logger;
            _eventLogic = eventLogic;
            _eventGenreLogic = eventGenreLogic;
            _mapper = mapper;
        }

        [HttpGet("GetAllEvents")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<EventDto> events = _eventLogic.GetAllEvents();

                if (events == null)
                {
                    return NotFound();
                }

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllActiveEvents")]
        public IActionResult GetAllActiveEvents()
        {
            try
            {
                IEnumerable<EventDto> events = _eventLogic.GetAllActiveEvents();

                if (events == null)
                {
                    return NotFound();
                }

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("SortEventData")]
        public IActionResult SortEventData([FromBody] OrderRequest orderRequest)
        {
            try
            {
                IEnumerable<EventDto> sorted_events = _eventLogic.SortEventData(orderRequest);

                return Ok(sorted_events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetByIdWithDetails(int id)
        {
            try
            {
                var @event = _eventLogic.GetByIdWithDetails(id);

                if (@event == null)
                {
                    return NotFound();
                }

                return Ok(@event);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetEventById(int id)
        {
            try
            {
                var @event = _eventLogic.GetById(id);

                if (@event == null)
                {
                    return NotFound();
                }

                return Ok(@event);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllByGenre/{genre_id}")]
        public IActionResult GetAllByGenre(int genre_id)
        {
            try
            {
                IEnumerable<EventDto> Result = _eventLogic.GetAllByGenre(genre_id);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetbyName/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var Result = _eventLogic.GetByName(name);

                return Ok(Result);
            }
            catch (Exception ex)
            {
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

                bool succes = _eventLogic.Create(@event);

                return Ok(true);
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
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

                bool succes = _eventLogic.Update(@event);

                if (!succes)
                {
                    return NotFound();
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                bool succes = _eventLogic.Delete(id);

                if (!succes)
                {
                    return NotFound();
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}