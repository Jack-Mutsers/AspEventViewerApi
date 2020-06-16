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
    public class ArtistGenreLogic : IArtistGenreLogic
    {
        private ILoggerManager _logger;
        private IArtistGenreRepository _repository;
        private IMapper _mapper;

        public ArtistGenreLogic(ILoggerManager logger, IArtistGenreRepository artistGenreRepository, IMapper mapper)
        {
            _logger = logger;
            _repository = artistGenreRepository;
            _mapper = mapper;
        }

        public bool Create(ArtistGenreForCreationDto artistGenreForCreation)
        {
            try
            {
                ArtistGenre DataEntity = _mapper.Map<ArtistGenre>(artistGenreForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateArtist action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int event_id, int genre_id)
        {
            try
            {
                var artistGenreDto = GetByIds(event_id, genre_id);

                if (artistGenreDto == null)
                {
                    return false;
                }

                ArtistGenre artistGenre = _mapper.Map<ArtistGenre>(artistGenreDto);

                _repository.Delete(artistGenre);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteArtistGenre action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool DeleteByArtist(int id)
        {
            try
            {
                var artistGenreDtos = GetByArtist(id);

                if (artistGenreDtos == null)
                {
                    _logger.LogError($"EventGenres with artist id: {id}, haven't been found in db.");
                    return false;
                }

                foreach (ArtistGenreDto artistGenreDto in artistGenreDtos)
                {
                    ArtistGenre artistGenre = _mapper.Map<ArtistGenre>(artistGenreDto);
                    _repository.Delete(artistGenre);
                }

                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteByArtist action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<ArtistGenreDto> GetByArtist(int artist_id)
        {
            try
            {
                IEnumerable<ArtistGenre> artistGenres = _repository.GetByArtist(artist_id);

                if (artistGenres == null)
                {
                    _logger.LogError($"ArtistGenre with id: {artist_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned ArtistGenre with event id: {artist_id}");

                IEnumerable<ArtistGenreDto> Result = _mapper.Map<IEnumerable<ArtistGenreDto>>(artistGenres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByArtist action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<ArtistGenreDto> GetByArtistWithDetails(int artist_id)
        {
            try
            {
                IEnumerable<ArtistGenre> artistGenres = _repository.GetByArtistWithDetails(artist_id);

                if (artistGenres == null)
                {
                    _logger.LogError($"ArtistGenre with id: {artist_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned ArtistGenre with event id: {artist_id}");

                IEnumerable<ArtistGenreDto> Result = _mapper.Map<IEnumerable<ArtistGenreDto>>(artistGenres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByArtistWithDetails action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<ArtistGenreDto> GetByGenre(int genre_id)
        {
            try
            {
                IEnumerable<ArtistGenre> artistGenres = _repository.GetByGenre(genre_id);

                if (artistGenres == null)
                {
                    _logger.LogError($"artistGenre with genre id: {genre_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned artistGenre with genre id: {genre_id}");

                IEnumerable<ArtistGenreDto> Result = _mapper.Map<IEnumerable<ArtistGenreDto>>(artistGenres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByGenre action: {ex.Message}");
                throw new Exception();
            }
        }

        public ArtistGenreDto GetByIds(int artist_id, int genre_id)
        {
            try
            {
                var eventGenre = _repository.GetRecord(artist_id, genre_id);

                if (eventGenre == null)
                {
                    _logger.LogError($"EventGenre with artist id: {artist_id} and genre id: {genre_id}, hasn't been found in db.");
                    return null;
                }

                ArtistGenreDto eventGenreDto = _mapper.Map<ArtistGenreDto>(eventGenre);

                return eventGenreDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ArtistGenreLogic.GetByIds action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
