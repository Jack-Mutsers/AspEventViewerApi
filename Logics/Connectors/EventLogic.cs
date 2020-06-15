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
    public class EventLogic : IEventLogic
    {
        private ILoggerManager _logger;
        private IEventRepository _repository;
        private IMapper _mapper;

        public EventLogic()
        {
        }

        public EventLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new EventRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(EventForCreationDto eventForCreation)
        {
            try
            {
                Event DataEntity = _mapper.Map<Event>(eventForCreation);

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

                _repository.Delete(@event);
                _repository.Save();

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
            throw new NotImplementedException();
        }

        public IEnumerable<EventDto> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public EventDto GetById(int event_id)
        {
            throw new NotImplementedException();
        }

        public EventDto GetByIdWithDetails(int event_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventDto> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventDto> GetSortedByName(bool ascending)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventDto> GetSortedByStartDate(bool ascending)
        {
            throw new NotImplementedException();
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

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated Event with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEvent action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
