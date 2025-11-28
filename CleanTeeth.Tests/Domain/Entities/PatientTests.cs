using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class PatientTests
    {
        private Email _email = new Email("example@gmail.com");
        private string _name = "Sachintha";

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_NullName_ShouldThrow()
        {
            new Patient(null!, _email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_EmptyName_ShouldThrow()
        {
            new Patient("", _email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_WhitespaceName_ShouldThrow()
        {
            new Patient("    ", _email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_NullEmail_ShouldThrow()
        {
            new Patient(_name, email: null!);
        }

        [TestMethod]
        public void Constructor_ValidPatient_ShouldCreateSuccessfully()
        {
            new Patient(_name, _email);
        }
    }
}
