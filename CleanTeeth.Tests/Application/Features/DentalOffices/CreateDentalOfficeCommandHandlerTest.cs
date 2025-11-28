using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Domain.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CleanTeeth.MsTests.Application.Features.DentalOffices
{
    [TestClass]
    public class CreateDentalOfficeCommandHandlerTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IDentalOfficeRepository repository;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IUnitOfWork unitOfWork;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private CreateDentalOfficeCommandHandler handler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDentalOfficeRepository>();
            unitOfWork = Substitute.For<IUnitOfWork>();
            handler = new CreateDentalOfficeCommandHandler(repository, unitOfWork);
        }

        [TestMethod]
        public async Task Handle_ValidCommand_ShouldReturnsDentalOfficeId()
        {
            var command = new CreateDentalOfficeCommand { Name = "Dental Office A" };

            var dentalOffice = new DentalOffice("Dental Office A");

            repository.Add(Arg.Any<DentalOffice>()).Returns(dentalOffice);

            var result = await handler.Handle(command);

            await repository.Received(1).Add(Arg.Any<DentalOffice>());
            await unitOfWork.Received(1).Commit();

            Assert.AreEqual(dentalOffice.Id, result);
        }

        [TestMethod]
        public async Task Handle_WhenTheresAnError_ShouldRollback()
        {
            var command = new CreateDentalOfficeCommand { Name = "Dental Office A" };

            repository.Add(Arg.Any<DentalOffice>()).Throws<Exception>();

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await handler.Handle(command);
            });

            await unitOfWork.Received(1).Rollback();
        }
    }
}
