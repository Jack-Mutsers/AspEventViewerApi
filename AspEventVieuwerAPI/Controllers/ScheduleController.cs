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
    public class ScheduleController : ControllerBase
    {
        private ILoggerManager _logger;
        private IScheduleLogic _scheduleLogic;

        public ScheduleController(ILoggerManager logger, IScheduleLogic scheduleLogic)
        {
            _logger = logger;
            _scheduleLogic = scheduleLogic;
        }

        [HttpGet]
        public IActionResult GetAllSchedules()
        {
            try
            {
                var schedules = _scheduleLogic.GetAll();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByStage/{id}")]
        public IActionResult GetByStage(int id)
        {
            try
            {
                var schedules = _scheduleLogic.GetByStage(id);

                if (schedules == null)
                {
                    return NotFound();
                }
                
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByStageWithDetails/{id}")]
        public IActionResult GetByStageWithDetails(int id)
        {
            try
            {
                var schedules = _scheduleLogic.GetByStageWithDetails(id);

                if (schedules == null)
                {
                    return NotFound();
                }

                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetScheduleById/{id}")]
        public IActionResult GetScheduleById(int id)
        {
            try
            {
                var schedule = _scheduleLogic.GetById(id);

                if (schedule == null)
                {
                    return NotFound();
                }

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateSchedule([FromBody]ScheduleForCreationDto schedule)
        {
            try
            {
                if (schedule == null)
                {
                    _logger.LogError("Schedule object sent from client is null.");
                    return BadRequest("Schedule object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Schedule object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _scheduleLogic.Create(schedule);

                return Ok("Schedule is created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateSchedule([FromBody]ScheduleForUpdateDto schedule) 
        {
            try
            {
                if (schedule == null)
                {
                    _logger.LogError("Schedule object sent from client is null.");
                    return BadRequest("Schedule object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Schedule object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _scheduleLogic.Update(schedule);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("Schedule is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSchedule(int id)
        {
            try
            {
                bool succes = _scheduleLogic.Delete(id);
                if (!succes)
                {
                    return NotFound();
                }
                
                return Ok("Schedule is delted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}