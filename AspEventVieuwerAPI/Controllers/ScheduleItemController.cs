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
    public class ScheduleItemController : ControllerBase
    {
        private ILoggerManager _logger;
        private IScheduleItemLogic _scheduleItemLogic;

        public ScheduleItemController(ILoggerManager logger, IScheduleItemLogic scheduleItemLogic)
        {
            _logger = logger;
            _scheduleItemLogic = scheduleItemLogic;
        }


        [HttpGet("GetBySchedule/{id}")]
        public IActionResult GetScheduleItemBySchedule(int id)
        {
            try
            {
                var scheduleItems = _scheduleItemLogic.GetBySchedule(id);

                if (scheduleItems == null)
                {
                    return NotFound();
                }
                 
                return Ok(scheduleItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetScheduleItemById(int id)
        {
            try
            {
                var scheduleItems = _scheduleItemLogic.GetById(id);

                if (scheduleItems == null)
                {
                    return NotFound();
                }
                 
                return Ok(scheduleItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetScheduleItemByIdWithDetails(int id)
        {
            try
            {
                var scheduleItems = _scheduleItemLogic.GetByIdWithDetails(id);

                if (scheduleItems == null)
                {
                    return NotFound();
                }
                 
                return Ok(scheduleItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateScheduleItem([FromBody]ScheduleItemForCreationDto scheduleItem)
        {
            try
            {
                if (scheduleItem == null)
                {
                    _logger.LogError("ScheduleItem object sent from client is null.");
                    return BadRequest("ScheduleItem object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ScheduleItem object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _scheduleItemLogic.Create(scheduleItem);

                return Ok("ScheduleItem is created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateScheduleItem([FromBody]ScheduleItemForUpdateDto scheduleItem)
        {
            try
            {
                if (scheduleItem == null)
                {
                    _logger.LogError("ScheduleItem object sent from client is null.");
                    return BadRequest("ScheduleItem object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ScheduleItem object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _scheduleItemLogic.Update(scheduleItem);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("ScheduleItem is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScheduleItem(int id)
        {
            try
            {
                bool succes = _scheduleItemLogic.Delete(id);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("ScheduleItem is delted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}