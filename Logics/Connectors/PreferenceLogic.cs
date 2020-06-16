using AutoMapper;
using Contracts.Logger;
using Contracts.Logic;
using Contracts.Repository;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;

namespace Logics
{
    public class PreferenceLogic : IPreferenceLogic
    {
        private ILoggerManager _logger;
        private IPreferenceRepository _repository;
        private IMapper _mapper;

        public PreferenceLogic(ILoggerManager logger, IPreferenceRepository preferenceRepository, IMapper mapper)
        {
            _logger = logger;
            _repository = preferenceRepository;
            _mapper = mapper;
        }

        public bool Create(PreferenceForCreationDto preferenceForCreation)
        {
            try
            {
                Preference DataEntity = _mapper.Map<Preference>(preferenceForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePreference action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var preferenceDto = GetById(id);
                if (preferenceDto == null)
                {
                    _logger.LogError($"Preference with id: {id}, hasn't been found in db.");
                    return false;
                }

                Preference preference = _mapper.Map<Preference>(preferenceDto);

                _repository.Delete(preference);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePreference action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool DeleteByUser(int user_id)
        {
            try
            {
                _repository.DeleteByUser(user_id);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePreference action: {ex.Message}");
                throw new Exception();
            }
        }

        public PreferenceDto GetById(int preference_id)
        {
            try
            {
                var preference = _repository.GetById(preference_id);

                if (preference == null)
                {
                    _logger.LogError($"Preference with id: {preference_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Preference with id: {preference_id}");

                var Result = _mapper.Map<PreferenceDto>(preference);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPreferenceById action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<PreferenceDto> GetPreferenceByUser(int user_id)
        {
            try
            {
                var preference = _repository.GetPreferenceByUser(user_id);

                if (preference == null)
                {
                    _logger.LogError($"Preferences with user id: {user_id}, hasn't been found in db.");
                    return null;
                }

                _logger.LogInfo($"Returned Preferences with user id: {user_id}");

                var Result = _mapper.Map<IEnumerable<PreferenceDto>>(preference);
                return Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPreferenceByUser action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool UpdateByUser(int user_id, List<PreferenceForCreationDto> preferences)
        {
            try
            {
                bool success = DeleteByUser(user_id);

                if (!success)
                {
                    _logger.LogError($"Preferences with user id: {user_id}, haven't been found in db.");
                    return false;
                }

                foreach (PreferenceForCreationDto preference in preferences)
                {
                    success = Create(preference);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateByUser action: {ex.Message}");
                throw new Exception();
            }
        }

    }
}
