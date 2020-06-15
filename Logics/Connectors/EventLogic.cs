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

        public EventLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new EventRepository(repositoryContext);
            _mapper = mapper;
        }

    }
}
