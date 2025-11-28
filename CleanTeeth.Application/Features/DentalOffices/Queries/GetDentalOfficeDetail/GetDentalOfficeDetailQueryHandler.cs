using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail
{
    public class GetDentalOfficeDetailQueryHandler : IRequestHandler<GetDentalOfficeDetailQuery, DentalOfficeDetailDto>
    {
        private readonly IDentalOfficeRepository repository;

        public GetDentalOfficeDetailQueryHandler(IDentalOfficeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DentalOfficeDetailDto> Handle(GetDentalOfficeDetailQuery query)
        {
            var dentalOffice = await repository.GetById(query.Id);

            if (dentalOffice is null)
            {
                throw new NotFoundException();
            }

            var dto = new DentalOfficeDetailDto { Id = dentalOffice.Id, Name = dentalOffice.Name };

            return dto;
        }
    }
}
