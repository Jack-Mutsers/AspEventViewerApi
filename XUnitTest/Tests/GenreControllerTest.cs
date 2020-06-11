using AspEventVieuwerAPI.Controllers;
using Xunit;
using XUnitTest.Resources;

namespace XUnitTest.Tests
{
    public class GenreControllerTest
    {
        private ControllerRequrements requrements;
        private GenreController _controller;

        public GenreControllerTest()
        {
            requrements = new ControllerRequrements();
            _controller = new GenreController(requrements.logger, requrements.repository, requrements.mapper);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
