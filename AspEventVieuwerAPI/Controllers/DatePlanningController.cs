﻿using System;
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
    public class DatePlanningController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public DatePlanningController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllDates")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<DatePlanning> datePlannings = _repository.DatePlanning.GetAll();

                _logger.LogInfo($"Returned all Artists from database.");

                List<DatePlanning> plannings = new List<DatePlanning>();
                foreach (DatePlanning datePlanning in datePlannings)
                {
                    datePlanning.event_date.DatePlanning = null;
                    plannings.Add(datePlanning);
                }

                var Result = _mapper.Map<IEnumerable<DatePlanningDto>>(plannings);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet(Name = "GetNextEvent")]
        [Route("GetNextEvent")]
        public IActionResult GetNextEvent()
        {
            try
            {
                DatePlanning datePlanning = _repository.DatePlanning.GetNextEvent();

                _logger.LogInfo($"Returned all Artists from database.");

                datePlanning.event_date.DatePlanning = null;

                var Result = _mapper.Map<DatePlanningDto>(datePlanning);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetNextEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByEvent(int id)
        {
            try
            {
                IEnumerable<DatePlanning> datePlannings = _repository.DatePlanning.GetAllByEvent(id);

                _logger.LogInfo($"Returned all Artists from database.");

                var Result = _mapper.Map<IEnumerable<DatePlanningDto>>(datePlannings);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
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

                var DataEntity = _mapper.Map<DatePlanning>(datePlanning);

                _repository.DatePlanning.CreateDatePlanning(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<DatePlanningDto>(DataEntity);

                return Ok("date planning is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateDatePlanning action: {ex.Message}");
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

                var DataEntity = _repository.DatePlanning.GetById(datePlanning.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"date planning with id: {datePlanning.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(datePlanning, DataEntity);

                _repository.DatePlanning.UpdateDatePlanning(DataEntity);
                _repository.Save();

                return Ok("date planning is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateDatePlanning action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDatePlanning(int id)
        {
            try
            {
                var datePlanning = _repository.DatePlanning.GetById(id);
                if (datePlanning == null)
                {
                    _logger.LogError($"Date Planning with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.DatePlanning.DeleteDatePlanning(datePlanning);
                _repository.Save();

                return Ok("Date Planning is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteDatePlanning action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet(Name = "GetUpcommingEvent")]
        [Route("GetUpcommingEvent")]
        public DatePlanningDto GetUpcommingEventDate(int event_id)
        {
            try
            {
                DatePlanning datePlanning = _repository.DatePlanning.GetUpcomming(event_id);

                if (datePlanning == null)
                {
                    datePlanning = _repository.DatePlanning.GetLast(event_id);
                }

                if (datePlanning == null)
                {
                    return null;
                }

                datePlanning.event_date = _repository.EventDate.GetById(datePlanning.id);
                datePlanning.event_date.DatePlanning = null;

                DatePlanningDto datePlanningDto = _mapper.Map<DatePlanningDto>(datePlanning);

                ArtistController artistController = new ArtistController(_logger, _repository, _mapper);
                datePlanningDto.event_date.artists = artistController.GetArtistsByEventDate(datePlanning.event_date.id);
                datePlanningDto.event_date.@event = null;

                return datePlanningDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteDatePlanning action: {ex.Message}");
                return null;
            }
        }

        [HttpGet(Name = "GetFinishedEvent")]
        [Route("GetFinishedEvent")]
        public IEnumerable<DatePlanningDto> GetFinishedEventDates(int event_id)
        {
            try
            {
                IEnumerable<DatePlanning> datePlanning = _repository.DatePlanning.GetFinishedEventDates(event_id);

                List<DatePlanning> correctDatePlanning = new List<DatePlanning>();
                foreach (DatePlanning date in datePlanning)
                {
                    if (date.event_date.DatePlanning != null)
                    {
                        date.event_date.DatePlanning = null;
                    }
                    correctDatePlanning.Add(date);
                }

                IEnumerable<DatePlanningDto> datePlanningDto = _mapper.Map<IEnumerable<DatePlanningDto>>(correctDatePlanning);

                return datePlanningDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteDatePlanning action: {ex.Message}");
                return null;
            }
        }
    }
}