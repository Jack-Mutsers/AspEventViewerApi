using AspEventVieuwerAPI;
using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var config = new MapperConfiguration(cfg => new MappingProfile());
            IMapper mapper = config.CreateMapper();

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            SqlData sqlData = new SqlData();
            optionsBuilder.UseSqlServer(sqlData.connectionString);

            _repository = new RepositoryWrapper(new RepositoryContext(optionsBuilder.Options));
        }
    }
}
