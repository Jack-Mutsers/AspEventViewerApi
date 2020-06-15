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
    public class EventGenreLogic : IEventGenreLogic
    {
        private ILoggerManager _logger;
        private IEventGenreRepository _repository;
        private IMapper _mapper;

        public EventGenreLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new EventGenreRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(EventGenreForCreationDto eventGenre)
        {
            try
            {
                EventGenre DataEntity = _mapper.Map<EventGenre>(eventGenre);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEvent action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int event_id, int genre_id)
        {
            try
            {
                var eventGenreDto = GetByIds(event_id, genre_id);

                if (eventGenreDto == null)
                {
                    return false;
                }

                EventGenre eventGenre = _mapper.Map<EventGenre>(eventGenreDto);

                _repository.Delete(eventGenre);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEventGenre action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool DeleteByEvent(int id)
        {
            try
            {
                var eventGenreDtos = GetByEvent(id);

                if (eventGenreDtos == null)
                {
                    _logger.LogError($"EventGenres with event id: {id}, haven't been found in db.");
                    return false;
                }

                foreach (EventGenreDto eventGenreDto in eventGenreDtos)
                {
                    EventGenre eventGenre = _mapper.Map<EventGenre>(eventGenreDto);
                    _repository.Delete(eventGenre);
                }

                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEventGenre action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventGenreDto> GetByEvent(int event_id)
        {
            try
            {
                IEnumerable<EventGenre> eventGenres = _repository.GetByEvent(event_id);

                if (eventGenres == null)
                {
                    _logger.LogError($"Genre with id: {event_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Genre with event id: {event_id}");

                IEnumerable<EventGenreDto> Result = _mapper.Map<IEnumerable<EventGenreDto>>(eventGenres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventGenreDto> GetByEventWithDetails(int event_id)
        {
            try
            {
                IEnumerable<EventGenre> eventGenres = _repository.GetByEventWithDetails(event_id);

                if (eventGenres == null)
                {
                    _logger.LogError($"Genre with id: {event_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Genre with event id: {event_id}");

                IEnumerable<EventGenreDto> Result = _mapper.Map<IEnumerable<EventGenreDto>>(eventGenres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventGenreDto> GetByGenre(int genre_id)
        {
            try
            {
                IEnumerable<EventGenre> eventGenres = _repository.GetByGenre(genre_id);

                if (eventGenres == null)
                {
                    _logger.LogError($"EventGenre with genre id: {genre_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned EventGenre with genre id: {genre_id}");

                IEnumerable<EventGenreDto> Result = _mapper.Map<IEnumerable<EventGenreDto>>(eventGenres);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventDto> GetEventsByGenre(int genre_id)
        {
            try
            {
                IEnumerable<Event> events = _repository.GetEventsByGenre(genre_id);

                if (events == null)
                {
                    _logger.LogError($"events with genre id: {genre_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned events with genre id: {genre_id}");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(events);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGenreById action: {ex.Message}");
                throw new Exception();
            }
        }

        public EventGenreDto GetByIds(int event_id, int genre_id)
        {
            try
            {
                var eventGenre = _repository.GetRecord(event_id, genre_id);

                if (eventGenre == null)
                {
                    _logger.LogError($"EventGenre with event id: {event_id} and genre id: {genre_id}, hasn't been found in db.");
                    return null;
                }

                EventGenreDto eventGenreDto = _mapper.Map<EventGenreDto>(eventGenre);

                return eventGenreDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIds action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
