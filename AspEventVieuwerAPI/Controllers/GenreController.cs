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
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public GenreController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
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
                var art = _repository.Genre.GetAllGenres();

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

        [HttpGet("{id}", Name = "GetGenreById")]
        public IActionResult GetGenreById(int id)
        {
            try
            {
                var art = _repository.Genre.GetById(id);

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

    }
}