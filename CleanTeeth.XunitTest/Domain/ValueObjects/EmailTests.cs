using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.XunitTest.Domain.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Constructor_ValidEmail_ShouldSetValue()
        {
            // Arrange
            var validEmail = "test@example.com";

            // Act
            var email = new Email(validEmail);

            // Assert 
            Assert.Equal(validEmail, email.Value);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Constructor_NullOrWhiteSpace_ShouldThrowBusinessRuleException(string? badInput)
        {
            // Act & Assert 
            var ex = Assert.Throws<BusinessRuleException>(() => new Email(badInput!));
            Assert.Contains("required", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("Invalid email")]
        [InlineData("test.com")]
        [InlineData("test")]
        public void Constructor_EmailWithoutAtSymbol_ShouldThrowsBusinessRuleException(string input)
        {
            // Act & Assert
            var ex = Assert.Throws<BusinessRuleException>(() => new Email(input));
            Assert.Contains("invalid", ex.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}
