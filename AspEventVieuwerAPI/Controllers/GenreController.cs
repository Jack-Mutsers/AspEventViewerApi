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
using Logics;
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
        private IGenreLogic _genreLogic;
        private IEventGenreLogic _eventGenreLogic;
        private IArtistGenreLogic _artistGenreLogic;

        public GenreController(ILoggerManager logger, IGenreLogic genreLogic, IEventGenreLogic eventGenreLogic, IArtistGenreLogic artistGenreLogic)
        {
            _logger = logger;
            _genreLogic = genreLogic;
            _eventGenreLogic = eventGenreLogic;
            _artistGenreLogic = artistGenreLogic;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var genres = _genreLogic.GetAllGenres();

                if (genres == null)
                {
                    return NotFound();
                }

                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            try
            {
                var genre = _genreLogic.GetById(id);

                if (genre == null)
                {
                    return NotFound();
                }

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByEvent/{event_id}")]
        public IActionResult GetByEvent(int event_id)
        {
            try
            {
                IEnumerable<EventGenreDto> genres = _eventGenreLogic.GetByEvent(event_id);

                if (genres == null)
                {
                    return NotFound();
                }

                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("GetByEventWithDetails/{event_id}")]
        public IActionResult GetByEventWithDetails(int event_id)
        {
            try
            {
                IEnumerable<EventGenreDto> genres = _eventGenreLogic.GetByEventWithDetails(event_id);

                if (genres == null)
                {
                    return NotFound();
                }

                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("GetByArtist/{artist_id}")]
        public IActionResult GetByArtist(int artist_id)
        {
            try
            {
                IEnumerable<GenreDto> genres = _genreLogic.GetByArtist(artist_id);

                if (genres == null)
                {
                    return NotFound();
                }

                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}