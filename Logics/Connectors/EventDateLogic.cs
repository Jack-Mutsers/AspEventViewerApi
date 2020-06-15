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
    public class EventDateLogic : IEventDateLogic
    {
        private ILoggerManager _logger;
        private IEventDateRepository _repository;
        private IMapper _mapper;

        public EventDateLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new EventDateRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(EventDateForCreationDto eventDateForCreation)
        {
            try
            {
                EventDate DataEntity = _mapper.Map<EventDate>(eventDateForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEventDate action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var eventDateDto = GetById(id);
                if (eventDateDto == null)
                {
                    _logger.LogError($"EventDate with id: {id}, hasn't been found in db.");
                    return false;
                }

                EventDate eventDate = _mapper.Map<EventDate>(eventDateDto);

                _repository.Delete(eventDate);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEventDate action: {ex.Message}");
                throw new Exception();
            }
        }

        public EventDateDto GetByDatePlanning(int date_planning_id)
        {
            throw new NotImplementedException();
        }

        public EventDateDto GetById(int event_date_id)
        {
            throw new NotImplementedException();
        }

        public EventDateDto GetByIdWithDetails(int event_date_id)
        {
            throw new NotImplementedException();
        }

        public bool Update(EventDateForUpdateDto eventDateForUpdate)
        {
            try
            {
                var eventDateDto = GetById(eventDateForUpdate.id);

                if (eventDateDto == null)
                {
                    return false;
                }

                EventDate DataEntity = _mapper.Map<EventDate>(eventDateDto);

                _mapper.Map(eventDateForUpdate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated EventDate with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEventDate action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
