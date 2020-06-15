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
    public class ScheduleLogic : IScheduleLogic
    {
        private ILoggerManager _logger;
        private IScheduleRepository _repository;
        private IMapper _mapper;

        public ScheduleLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new ScheduleRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(ScheduleForCreationDto scheduleForCreation)
        {
            try
            {
                Schedule DataEntity = _mapper.Map<Schedule>(scheduleForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateSchedule action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var scheduleDto = GetById(id);
                if (scheduleDto == null)
                {
                    _logger.LogError($"Schedule with id: {id}, hasn't been found in db.");
                    return false;
                }

                Schedule schedule = _mapper.Map<Schedule>(scheduleDto);

                _repository.Delete(schedule);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteSchedule action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<ScheduleDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public ScheduleDto GetById(int Schedule_id)
        {
            throw new NotImplementedException();
        }

        public ScheduleDto GetByStage(int stage_id)
        {
            throw new NotImplementedException();
        }

        public ScheduleDto GetByStageWithDetails(int stage_id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ScheduleForUpdateDto scheduleForUpdate)
        {
            try
            {
                var scheduleDto = GetById(scheduleForUpdate.id);

                if (scheduleDto == null)
                {
                    return false;
                }

                Schedule DataEntity = _mapper.Map<Schedule>(scheduleDto);

                _mapper.Map(scheduleForUpdate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated Schedule with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateSchedule action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
