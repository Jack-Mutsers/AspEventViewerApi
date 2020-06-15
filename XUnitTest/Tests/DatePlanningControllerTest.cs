using AspEventVieuwerAPI.Controllers;
using Xunit;
using XUnitTest.Resources;

namespace XUnitTest.Tests
{
    public class DatePlanningControllerTest
    {
        private ControllerRequrements requrements;
        private DatePlanningController _controller;

        public DatePlanningControllerTest()
        {
            requrements = new ControllerRequrements();
            //_controller = new DatePlanningController(requrements.logger, requrements.repository.DatePlanning, requrements.repository.EventDate, requrements.mapper);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
