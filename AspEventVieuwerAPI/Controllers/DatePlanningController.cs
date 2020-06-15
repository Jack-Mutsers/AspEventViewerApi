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

namespace AspEventVieuwerAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class DatePlanningController : ControllerBase
    {
        private ILoggerManager _logger;
        //private IDatePlanningRepository _repository;
        //private IEventDateRepository _EventDateRepository;
        private IDatePlanningLogic _datePlanningLogic;
        private IEventDateLogic _eventDateLogic;
        private IArtistLogic _artistLogic;
        private IMapper _mapper;

        public DatePlanningController(ILoggerManager logger, IDatePlanningLogic datePlanningLogic, IEventDateLogic eventDateLogic, IArtistLogic artistLogic, IMapper mapper)
        {
            _logger = logger;
            _datePlanningLogic = datePlanningLogic;
            _eventDateLogic = eventDateLogic;
            _artistLogic = artistLogic;
            _mapper = mapper;
        }

        [HttpGet("GetAllDates")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<DatePlanningDto> datePlannings = _datePlanningLogic.GetAll();

                if (datePlannings == null)
                {
                    return NotFound();
                }

                return Ok(datePlannings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetNextEvent")]
        public IActionResult GetNextEvent()
        {
            try
            {
                DatePlanningDto datePlanning = _datePlanningLogic.GetNextEvent();

                if (datePlanning == null)
                {
                    return NotFound();
                }

                return Ok(datePlanning);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByEvent/{id}")]
        public IActionResult GetAllByEvent(int id)
        {
            try
            {
                IEnumerable<DatePlanningDto> datePlannings = _datePlanningLogic.GetAllByEvent(id);

                if (datePlannings == null)
                {
                    return NotFound();
                }

                return Ok(datePlannings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("GetById/{id}")]
        public IActionResult GetDatePlanningByid(int id)
        {
            try
            {
                DatePlanningDto datePlanning = _datePlanningLogic.GetById(id);

                if (datePlanning == null)
                {
                    return NotFound();
                }

                return Ok(datePlanning);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetDatePlanningByidWithDetails(int id)
        {
            try
            {
                DatePlanningDto datePlanning = _datePlanningLogic.GetByIdWithDetails(id);

                if (datePlanning == null)
                {
                    return NotFound();
                }

                return Ok(datePlanning);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetUpcommingEventDate/{event_id}")]
        public IActionResult GetUpcommingEventDate(int event_id)
        {
            try
            {
                DatePlanningDto datePlanning = _datePlanningLogic.GetUpcomming(event_id);

                if (datePlanning == null)
                {
                    return NotFound();
                }

                datePlanning.event_date = _eventDateLogic.GetByDatePlanning(datePlanning.id);
                datePlanning.event_date.artists = _artistLogic.GetArtistsByEventDate(datePlanning.event_date.id);

                return Ok(datePlanning);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetFinishedEventDates/{event_id}")]
        public IActionResult GetFinishedEventDates(int event_id)
        {
            try
            {
                IEnumerable<DatePlanningDto> datePlannings = _datePlanningLogic.GetFinishedEventDates(event_id);

                if (datePlannings == null)
                {
                    return NotFound();
                }

                return Ok(datePlannings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateDatePlanning([FromBody]DatePlanningForCreationDto datePlanning)
        {
            try
            {
                if (datePlanning == null)
                {
                    _logger.LogError("date planning object sent from client is null.");
                    return BadRequest("date planning object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid date planning object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool success = _datePlanningLogic.Create(datePlanning);

                return Ok("date planning is created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateDatePlanning([FromBody]DatePlanningForUpdateDto datePlanning)
        {
            try
            {
                if (datePlanning == null)
                {
                    _logger.LogError("date planning object sent from client is null.");
                    return BadRequest("date planning object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid date planning object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _datePlanningLogic.Update(datePlanning);

                if (!succes)
                {
                    return NotFound();
                }

                return Ok("date planning is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDatePlanning(int id)
        {
            try
            {
                bool succes = _datePlanningLogic.Delete(id);

                if (!succes)
                {
                    return NotFound();
                }

                return Ok("Date Planning is delted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }



    }
}