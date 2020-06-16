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
    public class ReviewController : ControllerBase
    {
        private ILoggerManager _logger;
        private IReviewLogic _reviewLogic;
        private IMapper _mapper;

        public ReviewController(ILoggerManager logger, IReviewLogic reviewLogic)
        {
            _logger = logger;
            _reviewLogic = reviewLogic;
        }

        [HttpGet("GetByEventDate/{id}")]
        public IActionResult GetReviewByEventDate(int id)
        {
            try
            {
                var reviews = _reviewLogic.GetByEventDate(id);

                if (reviews == null)
                {
                    return NotFound();
                }
                
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                IEnumerable<ReviewDto> reviews = _reviewLogic.GetAllOpenReviews();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetReviewById(int id)
        {
            try
            {
                var review = _reviewLogic.GetById(id);

                if (review == null)
                {
                    return NotFound();
                }

                return Ok(review);
            }
            catch (Exception ex)
            {
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

                bool succes = _reviewLogic.Create(review);

                return Ok(true);
            }
            catch (Exception ex)
            {
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

                bool succes = _reviewLogic.Update(review);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("Review is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            try
            {
                bool succes = _reviewLogic.Delete(id);
                if (!succes)
                {
                    return NotFound();
                }

                return Ok("Review is delted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}