using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.ValueObjects
{
    [TestClass]
    public class TimeIntervalTests
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_StartIsAfterEndTime_ShouldThrow()
        {
            new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddHours(-1));
        }

        [TestMethod]
        public void Constructor_ValidTimeInterval_ShouldCreateSuccessfully()
        {
            new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddHours(1));
        }
    }
}
