using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Mappings;
using AutoMapper;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<ActionResult<Guid>>, IMapFrom<CreateCustomerCommand>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCustomerCommand, Customer>();
        }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ActionResult<Guid>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateCustomerCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActionResult<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {

                var entity = _mapper.Map<Customer>(request);

                _context.Customers.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return new CreatedResult($"/api/customers/{entity.Id}", entity.Id);
            }
        }
    }
}