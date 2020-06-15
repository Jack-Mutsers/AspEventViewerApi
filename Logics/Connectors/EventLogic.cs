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
using System.Linq;

namespace Logics
{
    public class EventLogic : IEventLogic
    {
        private ILoggerManager _logger;
        private IEventRepository _eventRepository;
        private IEventGenreRepository _eventGenreRepository;
        private IMapper _mapper;

        public EventLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _eventRepository = new EventRepository(repositoryContext);
            _eventGenreRepository = new EventGenreRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(EventForCreationDto eventForCreation)
        {
            try
            {
                Event DataEntity = _mapper.Map<Event>(eventForCreation);

                _eventRepository.Create(DataEntity);
                _eventRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEvent action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var eventDto = GetById(id);
                if (eventDto == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return false;
                }

                Event @event = _mapper.Map<Event>(eventDto);

                _eventRepository.Delete(@event);
                _eventRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEvent action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventDto> GetAllActiveEvents()
        {
            try
            {
                IEnumerable<Event> events = _eventRepository.GetAllActiveEvents();

                if (events == null)
                {
                    _logger.LogError($"No events have been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned all Events from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(events);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllActiveEvents action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventDto> GetAllEvents()
        {
            try
            {
                IEnumerable<Event> events = _eventRepository.GetAllEvents();

                if (events == null)
                {
                    _logger.LogError($"No events have been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned all Events from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(events);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                throw new Exception();
            }
        }

        public EventDto GetById(int event_id)
        {
            try
            {
                var @event = _eventRepository.GetById(event_id);

                if (@event == null)
                {
                    _logger.LogError($"Event with id: {event_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Event with id: {event_id}");

                EventDto eventDto = _mapper.Map<EventDto>(@event);

                return eventDto;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventById action: {ex.Message}");
                throw new Exception();
            }
        }

        public EventDto GetByIdWithDetails(int event_id)
        {
            try
            {
                var @event = _eventRepository.GetByIdWithDetails(event_id);

                if (@event == null)
                {
                    _logger.LogError($"Event with id: {event_id}, hasn't been found in db.");
                    return null;
                }


                _logger.LogInfo($"Returned Event with id: {event_id}");

                EventDto eventDto = _mapper.Map<EventDto>(@event);

                return eventDto;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventById action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventDto> GetByName(string name)
        {
            try
            {
                IEnumerable<Event> events = name == "" ?
                    _eventRepository.GetAllActiveEvents() :
                    _eventRepository.GetByName(name);

                _logger.LogInfo($"Returned all Events with names that contain: {name} from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(events);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByName action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventDto> SortEventData(OrderRequest orderRequest)
        {
            try
            {
                IEnumerable<Event> sorted_events = null;

                if (orderRequest.FieldName == "name")
                    sorted_events = _eventRepository.GetSortedByName(orderRequest.Ascending);

                else if (orderRequest.FieldName == "startdate")
                    sorted_events = GetEventByStartDate(orderRequest.Ascending);

                _logger.LogInfo($"Returned all Events from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(sorted_events);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<Event> GetEventByStartDate(bool ascending)
        {
            try
            {
                IEnumerable<Event> @events = _eventRepository.GetSortedByStartDate(ascending);

                _logger.LogInfo($"Returned all Events from database.");

                List<Event> eventsList = new List<Event>();
                foreach (Event @event in @events.ToList())
                {
                    IEnumerable<EventGenre> eventGenres = _eventGenreRepository.GetByEventWithDetails(@event.id);
                    @event.genre = _mapper.Map<ICollection<EventGenre>>(eventGenres);
                    eventsList.Add(@event);
                }

                return eventsList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Update(EventForUpdateDto eventForUpdate)
        {
            try
            {
                var eventDto = GetById(eventForUpdate.id);

                if (eventDto == null)
                {
                    return false;
                }

                Event DataEntity = _mapper.Map<Event>(eventDto);

                _mapper.Map(eventForUpdate, DataEntity);

                IEnumerable<EventGenre> eventGenres = _eventGenreRepository.GetByEvent(DataEntity.id);
                foreach (EventGenre eventGenre in eventGenres)
                {
                    _eventGenreRepository.Delete(eventGenre);
                }
                _eventGenreRepository.Save();

                _eventRepository.Update(DataEntity);
                _eventRepository.Save();

                _logger.LogError($"Updated Event with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEvent action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<EventDto> GetAllByGenre(int genre_id)
        {
            try
            {
                IEnumerable<Event> @events = genre_id > 0 ?
                    _eventGenreRepository.GetEventsByGenre(genre_id) :
                    _eventRepository.GetAllActiveEvents();

                _logger.LogInfo($"Returned all Events with genre id: {genre_id} from database.");

                IEnumerable<EventDto> Result = _mapper.Map<IEnumerable<EventDto>>(@events);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllByGenre action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
