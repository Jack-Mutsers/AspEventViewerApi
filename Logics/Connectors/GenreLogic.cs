using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql;
using Contracts.Repository;
using Contracts.Logic;
using Contracts.Logger;
using AutoMapper;
using Entities.DataTransferObjects;

namespace Logics
{
    public class GenreLogic : IGenreLogic
    {
        private ILoggerManager _logger;
        private IGenreRepository _repository;
        private IMapper _mapper;

        public GenreLogic(ILoggerManager logger, IGenreRepository genreRepository, IMapper mapper)
        {
            _logger = logger;
            _repository = genreRepository;
            _mapper = mapper;
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            try
            {
                var art = _repository.GetAllGenres();

                _logger.LogInfo($"Returned all Genre from database.");

                IEnumerable<GenreDto> Result = _mapper.Map<IEnumerable<GenreDto>>(art);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllGenres action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<GenreDto> GetByArtist(int artist_id)
        {
            try
            {
                IEnumerable<Genre> genres = _repository.GetByArtist(artist_id);

                if (genres == null)
                {
                    _logger.LogError($"Genre with artist id: {artist_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Genre with artist id: {artist_id}");

                var Result = _mapper.Map<IEnumerable<GenreDto>>(genres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<GenreDto> GetByEvent(int event_id)
        {
            try
            {
                IEnumerable<Genre> genres = _repository.GetByEvent(event_id);

                if (genres == null)
                {
                    _logger.LogError($"Genre with event id: {event_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Genre with event id: {event_id}");

                var Result = _mapper.Map<IEnumerable<GenreDto>>(genres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                throw new Exception();
            }
        }

        public GenreDto GetById(int genre_id)
        {
            try
            {
                var art = _repository.GetById(genre_id);

                if (art == null)
                {
                    _logger.LogError($"Genre with id: {genre_id}, hasn't been found in db.");
                    return null;
                }
                _logger.LogInfo($"Returned Genre with id: {genre_id}");

                GenreDto Result = _mapper.Map<GenreDto>(art);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
