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
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ReviewController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //IEnumerable<Review> GetByEventDate(int event_date_id);
        [HttpGet("{id}", Name = "GetByEventDate")]
        public IActionResult GetReviewByEventDate(int id)
        {
            try
            {
                var reviews = _repository.Review.GetByEventDate(id);

                if (reviews == null)
                {
                    _logger.LogError($"Reviews with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Reviews with id: {id}");

                    var Result = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReviewByEventDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //IEnumerable<Review> GetAllOpenReviews();
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var reviews = _repository.Review.GetAllOpenReviews();

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

        //Review GetById(int review_id);
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetReviewById(int id)
        {
            try
            {
                var review = _repository.Review.GetById(id);

                if (review == null)
                {
                    _logger.LogError($"Review with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Review with id: {id}");

                    var Result = _mapper.Map<ReviewDto>(review);
                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetReviewById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //void CreateReview(Review review);
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

                _repository.Review.CreateReview(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<ReviewDto>(DataEntity);

                return Ok("Review is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //void UpdateReview(Review review);
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

                var DataEntity = _repository.Review.GetById(review.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Review with id: {review.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(review, DataEntity);

                _repository.Review.UpdateReview(DataEntity);
                _repository.Save();

                return Ok("Review is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //void DeleteReview(Review review);
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            try
            {
                var review = _repository.Review.GetById(id);
                if (review == null)
                {
                    _logger.LogError($"Review with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Review.DeleteReview(review);
                _repository.Save();

                return Ok("Review is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteReview action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}