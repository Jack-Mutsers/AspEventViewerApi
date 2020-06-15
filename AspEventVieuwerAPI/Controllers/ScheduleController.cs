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
        private IScheduleRepository _repository;
        private IMapper _mapper;

        public ScheduleController(ILoggerManager logger, IScheduleRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllSchedules()
        {
            try
            {
                var schedules = _repository.GetAll();

                _logger.LogInfo($"Returned all Schedules from database.");

                var Result = _mapper.Map<IEnumerable<ArtistDto>>(schedules);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSchedules action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByStage/{id}")]
        public IActionResult GetByStage(int id)
        {
            try
            {
                var schedule = _repository.GetByStage(id);

                if (schedule == null)
                {
                    _logger.LogError($"Schedules with stage id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                 
                _logger.LogInfo($"Returned Schedules with stage id: {id}");

                var Result = _mapper.Map<IEnumerable<ScheduleDto>>(schedule);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByStageWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByStageWithDetails/{id}")]
        public IActionResult GetByStageWithDetails(int id)
        {
            try
            {
                var schedule = _repository.GetByStageWithDetails(id);

                if (schedule == null)
                {
                    _logger.LogError($"Schedules with stage id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Schedules with stage id: {id}");

                var Result = _mapper.Map<IEnumerable<ScheduleDto>>(schedule);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByStageWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetScheduleById/{id}")]
        public IActionResult GetScheduleById(int id)
        {
            try
            {
                var schedule = _repository.GetById(id);

                if (schedule == null)
                {
                    _logger.LogError($"Schedule with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Schedule with id: {id}");

                var Result = _mapper.Map<ScheduleDto>(schedule);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetScheduleById action: {ex.Message}");
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

                var DataEntity = _mapper.Map<Schedule>(schedule);

                _repository.Create(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<ArtistDto>(DataEntity);

                return Ok("Schedule is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateSchedule action: {ex.Message}");
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

                var DataEntity = _repository.GetById(schedule.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Schedule with id: {schedule.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(schedule, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                return Ok("Schedule is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateSchedule action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSchedule(int id)
        {
            try
            {
                var schedule = _repository.GetById(id);
                if (schedule == null)
                {
                    _logger.LogError($"Schedule with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Delete(schedule);
                _repository.Save();

                return Ok("Schedule is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteSchedule action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}