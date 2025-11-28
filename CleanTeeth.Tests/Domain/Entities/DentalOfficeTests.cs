using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class DentalOfficeTests
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_NullName_ShouldThrow()
        {
            new DentalOffice(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_EmptyName_ShouldThrow()
        {
            new DentalOffice("");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_WhitespaceName_ShouldThrow()
        {
            new DentalOffice("    ");
        }

        [TestMethod]
        public void Constructor_ValidDentalOffice_ShouldCreateSuccessfully()
        {
            new DentalOffice("New York");
        }
    }
}
