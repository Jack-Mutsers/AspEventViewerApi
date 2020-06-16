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
    public class StageController : ControllerBase
    {
        private ILoggerManager _logger;
        private IStageLogic _stageLogic;

        public StageController(ILoggerManager logger, IStageLogic stageLogic)
        {
            _logger = logger;
            _stageLogic = stageLogic;
        }

        [HttpGet]
        public IActionResult GetAllStages()
        {
            try
            {
                var stages = _stageLogic.GetAll();

                if (stages == null)
                {
                    return NotFound();
                }

                return Ok(stages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetStageByEventDate/{event_date_id}")]
        public IActionResult GetStageByEventDate(int event_date_id)
        {
            try
            {
                IEnumerable<StageDto> stages = _stageLogic.GetAllByEventDate(event_date_id);

                if (stages == null)
                {
                    return NotFound();
                }
                
                return Ok(stages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("GetById/{id}")]
        public IActionResult GetStageById(int id)
        {
            try
            {
                var stage = _stageLogic.GetById(id);

                if (stage == null)
                {
                    return NotFound();
                }
                
                return Ok(stage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateStage([FromBody]StageForCreationDto stage)
        {
            try
            {
                if (stage == null)
                {
                    _logger.LogError("Stage object sent from client is null.");
                    return BadRequest("Stage object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Stage object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _stageLogic.Create(stage);

                return Ok("Stage is created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateStage([FromBody]StageForUpdateDto stage)
        {
            try
            {
                if (stage == null)
                {
                    _logger.LogError("Stage object sent from client is null.");
                    return BadRequest("Stage object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Stage object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _stageLogic.Update(stage);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("Stage is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStage(int id)
        {
            try
            {
                bool succes = _stageLogic.Delete(id);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("Stage is delted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}