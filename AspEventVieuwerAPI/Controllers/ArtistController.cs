using AspEventVieuwerAPI.Authentication;
using Contracts.Logger;
using Contracts.Logic;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspEventVieuwerAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private ILoggerManager _logger;
        private IArtistLogic _logic;

        public ArtistController(ILoggerManager logger, IArtistLogic logic)
        {
            _logger = logger;
            _logic = logic;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<ArtistDto> artist = _logic.GetAll();

                return Ok(artist);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetArtistsByEventDate/{event_date_id}")]
        public IActionResult GetArtistsByEventDate(int event_date_id)
        {
            try
            {
                var artists = _logic.GetArtistsByEventDate(event_date_id);

                if (artists == null)
                {
                    return NotFound();
                }

                return Ok(artists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetArtistById/{id}")]
        public IActionResult GetArtistById(int id)
        {
            try
            {
                var art = _logic.GetArtistById(id);

                if (art == null)
                {
                    return NotFound();
                }

                return Ok(art);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetArtistByIdWithDetails(int id)
        {
            try
            {
                var art = _logic.GetArtistByIdWithDetails(id);

                if (art == null)
                {
                    return NotFound();
                }

                return Ok(art);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateArtist([FromBody]ArtistForCreationDto artist)
        {
            try
            {
                if (artist == null)
                {
                    _logger.LogError("Artist object sent from client is null.");
                    return BadRequest("Artist object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Artist object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _logic.CreateArtist(artist);

                return Ok("Artist is created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateArtist([FromBody]ArtistForUpdateDto artist)
        {
            try
            {
                if (artist == null)
                {
                    _logger.LogError("Artist object sent from client is null.");
                    return BadRequest("Artist object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Artist object sent from client.");
                    return BadRequest("Invalid model object");
                }

                bool succes = _logic.UpdateArtist(artist);

                if (succes == false)
                {
                    return NotFound();
                }

                return Ok("Artist is updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArtist(int id)
        {
            try
            {
                bool succes = _logic.DeleteArtist(id);

                if (!succes) 
                {
                    return NotFound();
                }
                
                return Ok("Artist is delted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}