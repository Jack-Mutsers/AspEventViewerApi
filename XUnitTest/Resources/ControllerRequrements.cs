using AspEventVieuwerAPI;
using AutoMapper;
using Contracts;
using Contracts.Logger;
using Contracts.Repository;
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
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = mockMapper.CreateMapper();

            logger = new LoggerManager();
            repository = new RepositoryWrapper();
        }
    }
}
