using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.ValueObjects
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constuctor_NullEmail_ShouldThrow()
        {
            new Email(null!);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_EmailWithoutAtSymbol_ShouldThrow()
        {
            new Email("example.com");
        }

        [TestMethod]
        public void Constructor_ValidEmail_ShouldCreateSuccessfully()
        {
            new Email("example@gmail.com");
        }
    }
}
