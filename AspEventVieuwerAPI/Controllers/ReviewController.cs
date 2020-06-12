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
    public class ReviewController : ControllerBase
    {
        private ILoggerManager _logger;
        private IReviewRepository _repository;
        private IMapper _mapper;

        public ReviewController(ILoggerManager logger, IReviewRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetByEventDate/{id}")]
        public IActionResult GetReviewByEventDate(int id)
        {
            try
            {
                var reviews = _repository.GetByEventDate(id);

                if (reviews == null)
                {
                    _logger.LogError($"Reviews with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                
                _logger.LogInfo($"Returned Reviews with id: {id}");

                var Result = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReviewByEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var reviews = _repository.GetAllOpenReviews();

                _logger.LogInfo($"Returned all Reviews from database.");

                var Result = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllReviews action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetReviewById(int id)
        {
            try
            {
                var review = _repository.GetById(id);

                if (review == null)
                {
                    _logger.LogError($"Review with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Review with id: {id}");

                var Result = _mapper.Map<ReviewDto>(review);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReviewById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpPost]
        public IActionResult CreateReview([FromBody]ReviewForCreationDto review)
        {
            try
            {
                if (review == null)
                {
                    _logger.LogError("Review object sent from client is null.");
                    return BadRequest("Review object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Review object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var DataEntity = _mapper.Map<Review>(review);

                _repository.Create(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<ReviewDto>(DataEntity);

                return Ok(true);
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateReview([FromBody]ReviewForUpdateDto review)
        {
            try
            {
                if (review == null)
                {
                    _logger.LogError("Review object sent from client is null.");
                    return BadRequest("Review object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Review object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var DataEntity = _repository.GetById(review.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Review with id: {review.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(review, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                return Ok("Review is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            try
            {
                var review = _repository.GetById(id);
                if (review == null)
                {
                    _logger.LogError($"Review with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Delete(review);
                _repository.Save();

                return Ok("Review is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //public IEnumerable<ReviewDto> GetByEventDate(int event_date_id)
        //{
        //    try
        //    {
        //        var reviews = _repository.Review.GetByEventDate(event_date_id);

        //        if (reviews == null)
        //        {
        //            _logger.LogError($"Reviews with id: {event_date_id}, hasn't been found in db.");
        //            return null;
        //        }

        //        _logger.LogInfo($"Returned Reviews with id: {event_date_id}");

        //        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetReviewByEventDate action: {ex.Message}");
        //        return null;
        //    }
        //}
    }
}