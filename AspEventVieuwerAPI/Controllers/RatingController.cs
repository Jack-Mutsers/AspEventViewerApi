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
    public class RatingController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public RatingController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //IEnumerable<Rating> GetByEventDate(int event_date_id);
        [HttpGet("{id}", Name = "GetByEventDate")]
        public IActionResult GetReviewByEventDate(int id)
        {
            try
            {
                var ratings = _repository.Rating.GetByEventDate(id);

                if (ratings == null)
                {
                    _logger.LogError($"Review with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Review with id: {id}");

                    var Result = _mapper.Map<IEnumerable<ReviewDto >> (ratings);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReviewByEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //void CreateRating(Rating rating);
        [HttpPost]
        public IActionResult CreateRating([FromBody]RatingForCreationDto rating)
        {
            try
            {
                if (rating == null)
                {
                    _logger.LogError("Rating object sent from client is null.");
                    return BadRequest("Rating object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Rating object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var DataEntity = _mapper.Map<Rating>(rating);

                _repository.Rating.CreateRating(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<RatingDto>(DataEntity);

                return Ok("Rating is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateRating action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}