using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class DentistTests
    {
        private Email _email = new Email("example@gmail.com");
        private string _name = "Prasad";

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_NullName_ShouldThrow()
        {
            new Dentist(null!, _email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_EmptyName_ShouldThrow()
        {
            new Dentist("", _email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_WhitespaceName_ShouldThrow()
        {
            new Dentist("    ", _email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_NullEmail_ShouldThrow()
        {
            new Dentist(_name, email: null!);
        }

        [TestMethod]
        public void Constructor_ValidDentist_ShouldCreateSuccessfully()
        {
            new Dentist(_name, _email);
        }
    }
}
