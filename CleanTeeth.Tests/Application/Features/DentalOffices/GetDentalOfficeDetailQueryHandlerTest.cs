using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Domain.Entities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CleanTeeth.MsTests.Application.Features.DentalOffices
{
    [TestClass]
    public class GetDentalOfficeDetailQueryHandlerTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IDentalOfficeRepository repository;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private GetDentalOfficeDetailQueryHandler handler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDentalOfficeRepository>();
            handler = new GetDentalOfficeDetailQueryHandler(repository);
        }

        [TestMethod]
        public async Task Handle_DentalOfficeExist_ShouldReturnDentalOffice()
        {
            var dentalOffice = new DentalOffice("Dental Office A");
            var id = dentalOffice.Id;
            var query = new GetDentalOfficeDetailQuery { Id = id };

            repository.GetById(id).Returns(dentalOffice);

            var result = await handler.Handle(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual("Dental Office A", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_DentalOfficeNotExist_ShouldThorw()
        {
            var id = Guid.NewGuid();
            var query = new GetDentalOfficeDetailQuery { Id = id };

            repository.GetById(id).ReturnsNull();

            await handler.Handle(query);
        }
    }
}
