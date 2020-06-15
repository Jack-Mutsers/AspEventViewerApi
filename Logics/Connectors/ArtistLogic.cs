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
    public class ArtistLogic : IArtistLogic
    {
        private ILoggerManager _logger;
        private IArtistRepository _repository;
        private IMapper _mapper;

        public ArtistLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new ArtistRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool CreateArtist(ArtistForCreationDto artist)
        {
            try
            {
                var DataEntity = _mapper.Map<Artist>(artist);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateArtist action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool DeleteArtist(int id)
        {
            try
            {
                var artistDto = GetArtistById(id);
                if (artistDto == null)
                {
                    _logger.LogError($"Artist with id: {id}, hasn't been found in db.");
                    return false;
                }

                Artist artist = _mapper.Map<Artist>(artistDto);

                _repository.Delete(artist);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteArtist action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<ArtistDto> GetAll()
        {
            try
            {
                IEnumerable<Artist> artists = _repository.GetAllArtists();

                _logger.LogInfo($"Returned all Artists from database.");

                var Result = _mapper.Map<IEnumerable<ArtistDto>>(artists);

                return Result;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                throw new Exception();
            }
        }

        public ArtistDto GetArtistById(int id)
        {
            try
            {
                var artist = _repository.GetById(id);

                if (artist == null)
                {
                    _logger.LogError($"Artist with id: {id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogError($"Returned Artist with id: {id}");

                ArtistDto Result = _mapper.Map<ArtistDto>(artist);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetArtistById action: {ex.Message}");
                throw new Exception();
            }
        }

        public ArtistDto GetArtistByIdWithDetails(int id)
        {
            try
            {
                var artist = _repository.GetByIdWithDetails(id);

                if (artist == null)
                {
                    _logger.LogError($"Artist with id: {id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogError($"Returned Artist with id: {id}");

                ArtistDto Result = _mapper.Map<ArtistDto>(artist);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetArtistByIdWithDetails action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<ArtistDto> GetArtistsByEventDate(int event_date_id)
        {
            try
            {
                var artists = _repository.GetArtistsByEventDate(event_date_id);

                if (artists == null)
                {
                    _logger.LogError($"No artists that are linked to the event date with the id: {event_date_id} have been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Artists that are linked to the event date with the id: {event_date_id}");

                IEnumerable<ArtistDto> Result = _mapper.Map<IEnumerable<ArtistDto>>(artists);

                return Result;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetArtistsByEventDate action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool UpdateArtist(ArtistForUpdateDto artist)
        {
            try
            {
                var artistDto = GetArtistById(artist.id);

                if (artistDto == null)
                {
                    _logger.LogError($"Artist with id: {artist.id}, hasn't been found in db.");
                    return false;
                }

                Artist DataEntity = _mapper.Map<Artist>(artistDto);

                _mapper.Map(artist, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateArtist action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
