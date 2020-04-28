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
    public class StageController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public StageController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllStages()
        {
            try
            {
                var stages = _repository.Stage.GetAll();

                if (stages == null)
                {
                    _logger.LogError($"stages with EventDate id: , hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned stages with EventDate id:");

                    var Result = _mapper.Map<IEnumerable<StageDto>>(stages);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStageByEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetStageByEventDate")]
        public IActionResult GetStageByEventDate(int id)
        {
            try
            {
                var stages = _repository.Stage.GetAllByEventDate(id);

                if (stages == null)
                {
                    _logger.LogError($"stages with EventDate id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned stages with EventDate id: {id}");

                    var Result = _mapper.Map<IEnumerable<StageDto>>(stages);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStageByEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("{id}", Name = "GetStageById")]
        public IActionResult GetStageById(int id)
        {
            try
            {
                var stage = _repository.Stage.GetById(id);

                if (stage == null)
                {
                    _logger.LogError($"Stage with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Stage with id: {id}");

                    var Result = _mapper.Map<StageDto>(stage);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStageById action: {ex.Message}");
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

                var DataEntity = _mapper.Map<Stage>(stage);

                _repository.Stage.CreateStage(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<StageDto>(DataEntity);

                return Ok("Stage is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStage action: {ex.Message}");
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

                var DataEntity = _repository.Stage.GetById(stage.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Stage with id: {stage.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(stage, DataEntity);

                _repository.Stage.UpdateStage(DataEntity);
                _repository.Save();

                return Ok("Stage is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateStage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStage(int id)
        {
            try
            {
                var stage = _repository.Stage.GetById(id);
                if (stage == null)
                {
                    _logger.LogError($"Stage with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Stage.DeleteStage(stage);
                _repository.Save();

                return Ok("Stage is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteStage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}