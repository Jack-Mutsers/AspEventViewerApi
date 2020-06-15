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
        //private IEventDateRepository _repository;
        private IEventDateLogic _eventDateLogic;
        private IArtistLogic _artistLogic;
        private IMapper _mapper;

        public EventDateController(ILoggerManager logger, IEventDateLogic eventDateLogic, IArtistLogic artistLogic, IMapper mapper)
        {
            _logger = logger;
            //_repository = repository;
            _eventDateLogic = eventDateLogic;
            _artistLogic = artistLogic;
            _mapper = mapper;
        }

        [HttpGet("GetEventDateById/{id}")]
        public IActionResult GetEventDateById(int id)
        {
            try
            {
                var eventDate = _eventDateLogic.GetById(id);

                if (eventDate == null)
                {
                    return NotFound();
                }

                return Ok(eventDate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetEventDateByIdWithDetails(int id)
        {
            try
            {
                var eventDate = _eventDateLogic.GetByIdWithDetails(id);

                if (eventDate == null)
                {
                    return NotFound();
                }

                eventDate.artists = _artistLogic.GetArtistsByEventDate(eventDate.id);

                return Ok(eventDate);
            }
            catch (Exception ex)
            {
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

                bool success = _eventDateLogic.Create(eventDate);

                return Ok("EventDate is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
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

                bool succes = _eventDateLogic.Update(eventDate);
                
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("EventDate is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEventDate(int id)
        {
            try
            {
                bool succes = _eventDateLogic.Delete(id);

                if (!succes)
                {
                    return NotFound();
                }

                return Ok("EventDate is deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}