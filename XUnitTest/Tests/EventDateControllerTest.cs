using AspEventVieuwerAPI.Controllers;
using Xunit;
using XUnitTest.Resources;

namespace XUnitTest.Tests
{
    public class EventDateControllerTest
    {
        private ControllerRequrements requrements;
        private EventDateController _controller;

        public EventDateControllerTest()
        {
            requrements = new ControllerRequrements();
            _controller = new EventDateController(requrements.logger, requrements.repository.EventDate, requrements.mapper);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
