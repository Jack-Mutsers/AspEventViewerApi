using AspEventVieuwerAPI.Controllers;
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
            _controller = new ArtistController(requrements.logger, requrements.repository, requrements.mapper);
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
            OkObjectResult okResult = _controller.GetAll() as OkObjectResult;

            // Assert
            List<Artist> items = Assert.IsType<List<Artist>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
    }
}

/*
{
    "Error mapping types.

    Mapping types:
    List`1 -> IEnumerable`1
    System.Collections.Generic.List`1
    [
        [
            Entities.Models.Artist, 
            Entities, 
            Version=1.0.0.0, 
            Culture=neutral, 
            PublicKeyToken=null
        ]
    ] -> System.Collections.Generic.IEnumerable`1
    [
        [
            Entities.DataTransferObjects.ArtistDto,
            Entities,
            Version=1.0.0.0,
            Culture=neutral,
            PublicKeyToken=null
        ]
    ]"
}

*/