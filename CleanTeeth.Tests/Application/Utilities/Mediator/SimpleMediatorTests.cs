using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CleanTeeth.Tests.Application.Utilities.Mediator
{
    [TestClass]
    public class SimpleMediatorTests
    {
        public class TestRequest : IRequest<String>
        {
            public required string Name { get; set; }
        }

        public class TestRequestValidator : AbstractValidator<TestRequest>
        {
            public TestRequestValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        [TestMethod]
        public async Task Send_WithRegisteredHandler_ShouldHandlerExecute()
        {
            var request = new TestRequest() { Name = "ExampleName" };
            var handleMock = Substitute.For<IRequestHandler<TestRequest, string>>();
            var serviceProvider = Substitute.For<IServiceProvider>();

            serviceProvider
                .GetService(typeof(IRequestHandler<TestRequest, string>))
                .Returns(handleMock);

            var mediator = new SimpleMediator(serviceProvider);

            var result = await mediator.Send(request);

            await handleMock.Received(1).Handle(request);
        }

        [TestMethod]
        [ExpectedException(typeof(MediatorException))]
        public async Task Send_WithoutRegisterHandler_ShouldThrow()
        {
            var request = new TestRequest() { Name = "ExampleName" };
            var serviceProvider = Substitute.For<IServiceProvider>();

            serviceProvider
                .GetService(typeof(IRequestHandler<TestRequest, string>))
                .ReturnsNull();

            var mediator = new SimpleMediator(serviceProvider);

            var result = await mediator.Send(request);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidationException))]
        public async Task Send_InvalidCommand_ShouldThrow()
        {
            var request = new TestRequest() { Name = "" };
            var serviceProvider = Substitute.For<IServiceProvider>();
            var validator = new TestRequestValidator();

            serviceProvider
                .GetService(typeof(IValidator<TestRequest>))
                .Returns(validator);

            var mediator = new SimpleMediator(serviceProvider);

            var result = await mediator.Send(request);
        }

    };

}