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
    public class ArtistController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public ArtistController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var art = _repository.Artist.GetAllArtists();

                _logger.LogInfo($"Returned all Artists from database.");

                var Result = _mapper.Map<IEnumerable<ArtistDto>>(art);
                return Ok(Result);
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
                IEnumerable<Artist> artists = _repository.Artist.GetArtistsByEventDate(event_date_id);

                if (artists == null)
                {
                    _logger.LogError($"Artost with id: {artists}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Artist with id: {event_date_id}");

                var Result = _mapper.Map<IEnumerable<ArtistDto>>(artists);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetArtistById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetArtistById/{id}")]
        public IActionResult GetArtistById(int id)
        {
            try
            {
                var art = _repository.Artist.GetById(id);

                if (art == null)
                {
                    _logger.LogError($"Artist with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Artist with id: {id}");

                var Result = _mapper.Map<ArtistDto>(art);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetArtistById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByIdWithDetails/{id}")]
        public IActionResult GetArtistByIdWithDetails(int id)
        {
            try
            {
                var art = _repository.Artist.GetByIdWithDetails(id);

                if (art == null)
                {
                    _logger.LogError($"Artist with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Artist with id: {id}");

                var Result = _mapper.Map<ArtistDto>(art);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetArtistById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("ArtistByGenre/{id}")]
        //public IActionResult GetArtistByGenre(int id)
        //{
        //    try
        //    {
        //        var art = _repository.Artist.GetArtistsByGenre(id);

        //        if (art == null)
        //        {
        //            _logger.LogError($"Artost with id: {id}, hasn't been found in db.");
        //            return NotFound();
        //        }

        //        _logger.LogInfo($"Returned Artist with id: {id}");

        //        var Result = _mapper.Map<IEnumerable<ArtistDto>>(art);
        //        return Ok(Result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetArtistById action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

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

                var DataEntity = _mapper.Map<Artist>(artist);

                _repository.Artist.CreateArtist(DataEntity);
                _repository.Save();

                var createdEntity = _mapper.Map<ArtistDto>(DataEntity);

                return Ok("Artist is created");
                //return CreatedAtRoute("CategoryById", new { id = createdEntity.id }, createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateArtist action: {ex.Message}");
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

                var DataEntity = _repository.Artist.GetById(artist.id);
                if (DataEntity == null)
                {
                    _logger.LogError($"Artist with id: {artist.id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(artist, DataEntity);

                _repository.Artist.UpdateArtist(DataEntity);
                _repository.Save();

                return Ok("Artist is updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateArtist action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArtist(int id)
        {
            try
            {
                var artist = _repository.Artist.GetById(id);
                if (artist == null)
                {
                    _logger.LogError($"Artist with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Artist.DeleteArtist(artist);
                _repository.Save();

                return Ok("Artist is delted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteArtist action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}