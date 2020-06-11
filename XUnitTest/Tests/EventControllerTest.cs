using AspEventVieuwerAPI.Controllers;
using Xunit;
using XUnitTest.Resources;

namespace XUnitTest.Tests
{
    public class EventControllerTest
    {
        private ControllerRequrements requrements;
        private EventController _controller;

        public EventControllerTest()
        {
            requrements = new ControllerRequrements();
            _controller = new EventController(requrements.logger, requrements.repository, requrements.mapper);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
