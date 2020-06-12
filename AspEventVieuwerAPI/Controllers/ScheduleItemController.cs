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
        private IScheduleItemRepository _repository;
        private IMapper _mapper;

        public ScheduleItemController(ILoggerManager logger, IScheduleItemRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("GetBySchedule/{id}")]
        public IActionResult GetScheduleItemBySchedule(int id)
        {
            try
            {
                var scheduleItems = _repository.GetBySchedule(id);

                if (scheduleItems == null)
                {
                    _logger.LogError($"ScheduleItems with schedule id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                 
                _logger.LogInfo($"Returned ScheduleItem with schedule id: {id}");

                var Result = _mapper.Map<ScheduleItemDto>(scheduleItems);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetScheduleItemBySchedule action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetScheduleItemById(int id)
        {
            try
            {
                var scheduleItem = _repository.GetById(id);

                if (scheduleItem == null)
                {
                    _logger.LogError($"ScheduleItem with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                 
                _logger.LogInfo($"Returned ScheduleItem with id: {id}");

                var Result = _mapper.Map<ScheduleItemDto>(scheduleItem);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetScheduleItemById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetScheduleItemByIdWithDetails(int id)
        {
            try
            {
                var scheduleItem = _repository.GetByIdWithDetails(id);

                if (scheduleItem == null)
                {
                    _logger.LogError($"ScheduleItem with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                 
                _logger.LogInfo($"Returned ScheduleItem with id: {id}");

                var Result = _mapper.Map<ScheduleItemDto>(scheduleItem);
                return Ok(Result);
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

                _repository.Create(DataEntity);
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

                var DataEntity = _repository.GetById(scheduleItem.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"ScheduleItem with id: {scheduleItem.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(scheduleItem, DataEntity);

                _repository.Update(DataEntity);
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
                var scheduleItem = _repository.GetById(id);
                if (scheduleItem == null)
                {
                    _logger.LogError($"ScheduleItem with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Delete(scheduleItem);
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