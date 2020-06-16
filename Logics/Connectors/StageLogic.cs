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
    public class StageLogic : IStageLogic
    {
        private ILoggerManager _logger;
        private IStageRepository _repository;
        private IMapper _mapper;

        public StageLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new StageRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(StageForCreationDto stageForCreation)
        {
            try
            {
                Stage DataEntity = _mapper.Map<Stage>(stageForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStage action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var stageDto = GetById(id);
                if (stageDto == null)
                {
                    _logger.LogError($"Stage with id: {id}, hasn't been found in db.");
                    return false;
                }

                Stage stage = _mapper.Map<Stage>(stageDto);

                _repository.Delete(stage);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteStage action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<StageDto> GetAll()
        {
            try
            {
                var stages = _repository.GetAll();

                if (stages == null)
                {
                    _logger.LogError($"stages with EventDate id: , hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned stages with EventDate id:");

                var Result = _mapper.Map<IEnumerable<StageDto>>(stages);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStageByEventDate action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<StageDto> GetAllByEventDate(int event_date_id)
        {
            try
            {
                IEnumerable<Stage> stages = _repository.GetAllByEventDate(event_date_id);

                if (stages == null)
                {
                    _logger.LogError($"stages with EventDate id: {event_date_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned stages with EventDate id: {event_date_id}");

                IEnumerable<StageDto> Result = _mapper.Map<IEnumerable<StageDto>>(stages);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStageByEventDate action: {ex.Message}");
                throw new Exception();
            }
        }

        public StageDto GetById(int stage_id)
        {
            try
            {
                var stage = _repository.GetById(stage_id);

                if (stage == null)
                {
                    _logger.LogError($"Stage with id: {stage_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Stage with id: {stage_id}");

                var Result = _mapper.Map<StageDto>(stage);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStageById action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Update(StageForUpdateDto stageForUpdate)
        {
            try
            {
                var artistDto = GetById(stageForUpdate.id);

                if (artistDto == null)
                {
                    return false;
                }

                Stage DataEntity = _mapper.Map<Stage>(artistDto);

                _mapper.Map(stageForUpdate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated Stage with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateStage action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
