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
    public class GenreController : ControllerBase
    {
        private ILoggerManager _logger;
        private IGenreRepository _GenreRepository;
        private IEventGenreRepository _EventGenreRepository;
        private IArtistGenreRepository _ArtistGenreRepository;
        private IMapper _mapper;

        public GenreController(ILoggerManager logger, IGenreRepository genreRepository, IEventGenreRepository eventGenreRepository, IArtistGenreRepository artistGenreRepository, IMapper mapper)
        {
            _logger = logger;
            _GenreRepository = genreRepository;
            _EventGenreRepository = eventGenreRepository;
            _ArtistGenreRepository = artistGenreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var art = _GenreRepository.GetAllGenres();

                _logger.LogInfo($"Returned all Genre from database.");

                var Result = _mapper.Map<IEnumerable<GenreDto>>(art);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllGenres action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            try
            {
                var art = _GenreRepository.GetById(id);

                if (art == null)
                {
                    _logger.LogError($"Genre with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _logger.LogInfo($"Returned Genre with id: {id}");

                var Result = _mapper.Map<GenreDto>(art);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByEvent/{event_id}")]
        public IActionResult GetByEvent(int event_id)
        {
            try
            {
                IEnumerable<EventGenre> genres = _EventGenreRepository.GetByEvent(event_id);

                if (genres == null)
                {
                    _logger.LogError($"Genre with id: {event_id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Genre with event id: {event_id}");

                var Result = _mapper.Map<IEnumerable<EventGenreDto>>(genres);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("GetByEventWithDetails/{event_id}")]
        public IActionResult GetByEventWithDetails(int event_id)
        {
            try
            {
                IEnumerable<EventGenre> genres = _EventGenreRepository.GetByEventWithDetails(event_id);

                if (genres == null)
                {
                    _logger.LogError($"Genre with id: {event_id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Genre with event id: {event_id}");

                var Result = _mapper.Map<IEnumerable<EventGenreDto>>(genres);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("GetByArtist/{artist_id}")]
        public IActionResult GetByArtist(int artist_id)
        {
            try
            {
                IEnumerable<Genre> genres = _GenreRepository.GetByArtist(artist_id);

                if (genres == null)
                {
                    _logger.LogError($"Genre with id: {artist_id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Genre with artist id: {artist_id}");

                var Result = _mapper.Map<IEnumerable<GenreDto>>(genres);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}