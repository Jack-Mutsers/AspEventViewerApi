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
    public class ScheduleItemLogic : IScheduleItemLogic
    {
        private ILoggerManager _logger;
        private IScheduleItemRepository _repository;
        private IMapper _mapper;

        public ScheduleItemLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new ScheduleItemRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(ScheduleItemForCreationDto scheduleItemForCreation)
        {
            try
            {
                ScheduleItem DataEntity = _mapper.Map<ScheduleItem>(scheduleItemForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateScheduleItem action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var scheduleItemDto = GetById(id);
                if (scheduleItemDto == null)
                {
                    _logger.LogError($"ScheduleItem with id: {id}, hasn't been found in db.");
                    return false;
                }

                ScheduleItem scheduleItem = _mapper.Map<ScheduleItem>(scheduleItemDto);

                _repository.Delete(scheduleItem);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteScheduleItem action: {ex.Message}");
                throw new Exception();
            }
        }

        public ScheduleItemDto GetById(int item_id)
        {
            throw new NotImplementedException();
        }

        public ScheduleItemDto GetByIdWithDetails(int item_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ScheduleItemDto> GetBySchedule(int schedule_id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ScheduleItemForUpdateDto scheduleItemForUpdate)
        {
            try
            {
                var scheduleItemDto = GetById(scheduleItemForUpdate.id);

                if (scheduleItemDto == null)
                {
                    return false;
                }

                ScheduleItem DataEntity = _mapper.Map<ScheduleItem>(scheduleItemDto);

                _mapper.Map(scheduleItemForUpdate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated ScheduleItem with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateScheduleItem action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
