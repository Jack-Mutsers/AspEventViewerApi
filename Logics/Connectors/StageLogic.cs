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

    }
}
