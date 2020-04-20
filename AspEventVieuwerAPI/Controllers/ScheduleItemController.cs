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
    public class ScheduleItemController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ScheduleItemController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("{id}", Name = "GetBySchedule")]
        public IActionResult GetScheduleItemBySchedule(int id)
        {
            try
            {
                var scheduleItems = _repository.ScheduleItem.GetBySchedule(id);

                if (scheduleItems == null)
                {
                    _logger.LogError($"ScheduleItems with schedule id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ScheduleItem with schedule id: {id}");

                    var Result = _mapper.Map<ScheduleItemDto>(scheduleItems);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetScheduleItemBySchedule action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetScheduleItemById")]
        public IActionResult GetScheduleItemById(int id)
        {
            try
            {
                var scheduleItem = _repository.ScheduleItem.GetById(id);

                if (scheduleItem == null)
                {
                    _logger.LogError($"ScheduleItem with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ScheduleItem with id: {id}");

                    var Result = _mapper.Map<ScheduleItemDto>(scheduleItem);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetScheduleItemById action: {ex.Message}");
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

                var DataEntity = _mapper.Map<ScheduleItem>(scheduleItem);

                _repository.ScheduleItem.CreateScheduleItem(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<ArtistDto>(DataEntity);

                return Ok("ScheduleItem is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateScheduleItem action: {ex.Message}");
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

                var DataEntity = _repository.ScheduleItem.GetById(scheduleItem.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"ScheduleItem with id: {scheduleItem.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(scheduleItem, DataEntity);

                _repository.ScheduleItem.UpdateScheduleItem(DataEntity);
                _repository.Save();

                return Ok("ScheduleItem is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateScheduleItem action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScheduleItem(int id)
        {
            try
            {
                var scheduleItem = _repository.ScheduleItem.GetById(id);
                if (scheduleItem == null)
                {
                    _logger.LogError($"ScheduleItem with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.ScheduleItem.DeleteScheduleItem(scheduleItem);
                _repository.Save();

                return Ok("ScheduleItem is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteScheduleItem action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}