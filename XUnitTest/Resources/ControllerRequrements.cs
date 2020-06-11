using AspEventVieuwerAPI;
using AutoMapper;
using Contracts;
using LoggerService;
using System;
using System.Collections.Generic;
using System.Text;
using TestRepository;

namespace XUnitTest.Resources
{
    public class ControllerRequrements
    {
        public IMapper mapper { get; private set; }
        public ILoggerManager logger { get; private set; }
        public IRepositoryWrapper repository { get; private set; }

        public ControllerRequrements()
        {
            var config = new MapperConfiguration(cfg => new MappingProfile());
            mapper = config.CreateMapper();
            logger = new LoggerManager();
            repository = new RepositoryWrapper();
        }
    }
}
