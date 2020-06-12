using AspEventVieuwerAPI.Controllers;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using XUnitTest.Resources;

namespace XUnitTest.Tests
{
    public class ArtistControllerTest
    {
        private ControllerRequrements requrements;
        private ArtistController _controller;

        public ArtistControllerTest()
        {
            requrements = new ControllerRequrements();
            _controller = new ArtistController(logger: requrements.logger, repository: requrements.repository.Artist, mapper: requrements.mapper);
        }

        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            var okResult = _controller.GetAll();

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetAll_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetAll() as OkObjectResult;

            // Assert
            List<ArtistDto> items = Assert.IsType<List<ArtistDto>>(okResult.Value);
            Assert.Equal(4, items.Count);
        }
    }
}
