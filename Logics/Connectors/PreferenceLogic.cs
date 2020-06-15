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
    public class PreferenceLogic : IPreferenceLogic
    {
        private ILoggerManager _logger;
        private IPreferenceRepository _repository;
        private IMapper _mapper;

        public PreferenceLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new PreferenceRepository(repositoryContext);
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

        public PreferenceDto GetById(int preference_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PreferenceDto> GetPreferenceByUser(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
