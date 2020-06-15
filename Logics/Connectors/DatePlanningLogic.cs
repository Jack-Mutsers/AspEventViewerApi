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
    public class DatePlanningLogic : IDatePlanningLogic
    {
        private ILoggerManager _logger;
        private IDatePlanningRepository _repository;
        private IMapper _mapper;

        public DatePlanningLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new DatePlanningRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(DatePlanningForCreationDto datePlanningForCreation)
        {
            try
            {
                var DataEntity = _mapper.Map<DatePlanning>(datePlanningForCreation);

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

        public bool Delete(int id)
        {
            try
            {
                var datePlanningDto = GetById(id);
                if (datePlanningDto == null)
                {
                    return false;
                }

                DatePlanning datePlanning = _mapper.Map<DatePlanning>(datePlanningDto);

                _repository.Delete(datePlanning);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteArtist action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<DatePlanningDto> GetAll()
        {
            try
            {
                IEnumerable<DatePlanning> datePlannings = _repository.GetAll();

                if (datePlannings == null)
                {
                    _logger.LogError($"No DatePlannings have been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned all DatePlannings from database.");

                var Result = _mapper.Map<IEnumerable<DatePlanningDto>>(datePlannings);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<DatePlanningDto> GetAllByEvent(int event_id)
        {
            try
            {
                IEnumerable<DatePlanning> datePlannings = _repository.GetAllByEvent(event_id);

                if (datePlannings == null)
                {
                    _logger.LogError($"date Planning with id: {event_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned all Artists from database.");

                var Result = _mapper.Map<IEnumerable<DatePlanningDto>>(datePlannings);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                throw new Exception();
            }
        }

        public DatePlanningDto GetById(int planning_id)
        {
            try
            {
                DatePlanning datePlanning = _repository.GetById(planning_id);

                if (datePlanning == null)
                {
                    _logger.LogError($"date Planning with id: {planning_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned all Artists from database.");

                var Result = _mapper.Map<DatePlanningDto>(datePlanning);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                throw new Exception();
            }
        }

        public DatePlanningDto GetByIdWithDetails(int planning_id)
        {
            try
            {
                DatePlanning datePlanning = _repository.GetByIdWithDetails(planning_id);

                if (datePlanning == null)
                {
                    _logger.LogError($"date Planning with id: {planning_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned all date Plannings from database.");

                var Result = _mapper.Map<DatePlanningDto>(datePlanning);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdWithDetails action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<DatePlanningDto> GetFinishedEventDates(int event_id)
        {
            try
            {
                IEnumerable<DatePlanning> datePlannings = _repository.GetFinishedEventDates(event_id);

                if (datePlannings == null)
                {
                    _logger.LogError($"date Plannings with event id: {event_id}, hasn't been found in db.");
                    return null;
                }

                List<DatePlanning> correctDatePlanning = new List<DatePlanning>();

                foreach (DatePlanning date in datePlannings)
                {
                    if (date.event_date.DatePlanning != null)
                    {
                        date.event_date.DatePlanning = null;
                    }
                    correctDatePlanning.Add(date);
                }

                _logger.LogInfo($"Returned all date Plannings with event id: {event_id} from database.");

                IEnumerable<DatePlanningDto> datePlanningDto = _mapper.Map<IEnumerable<DatePlanningDto>>(correctDatePlanning);

                return datePlanningDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteDatePlanning action: {ex.Message}");
                throw new Exception();
            }
        }

        public DatePlanningDto GetLast(int event_id)
        {
            throw new NotImplementedException();
        }

        public DatePlanningDto GetNextEvent()
        {
            try
            {
                DatePlanning datePlanning = _repository.GetNextEvent();

                if (datePlanning == null)
                {
                    _logger.LogError($"no future date Planning has been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned all Artists from database.");

                var Result = _mapper.Map<DatePlanningDto>(datePlanning);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetNextEvent action: {ex.Message}");
                throw new Exception();
            }
        }

        public DatePlanningDto GetUpcomming(int event_id)
        {
            try
            {
                DatePlanning datePlanning = _repository.GetUpcomming(event_id);

                if (datePlanning == null)
                {
                    datePlanning = _repository.GetLast(event_id);
                }

                if (datePlanning == null)
                {
                    return null;
                }

                DatePlanningDto datePlanningDto = _mapper.Map<DatePlanningDto>(datePlanning);

                return datePlanningDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteDatePlanning action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Update(DatePlanningForUpdateDto datePlanningForUpdate)
        {
            try
            {
                var datePlanningDto = GetById(datePlanningForUpdate.id);

                if (datePlanningDto == null)
                {
                    return false;
                }

                DatePlanning DataEntity = _mapper.Map<DatePlanning>(datePlanningDto);

                _mapper.Map(datePlanningForUpdate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated DatePlanning with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateDatePlanning action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
