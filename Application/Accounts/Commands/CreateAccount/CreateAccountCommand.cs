using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IMapFrom<CreateAccountCommand>, IRequest<ActionResult<Guid>>
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public Guid CustomerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAccountCommand, Account>();
        }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ActionResult<Guid>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateAccountCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActionResult<Guid>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var customerEntity = await _context.Customers.FindAsync(request.CustomerId);

                if (customerEntity == null)
                {
                    throw new NotFoundException(nameof(Customers), request.CustomerId);
                }

                var accountEntity = _mapper.Map<Account>(request);

                _context.Accounts.Add(accountEntity);

                await _context.SaveChangesAsync(cancellationToken);

                return new CreatedResult($"/api/accounts/{accountEntity.Id}", accountEntity.Id);
            }
        }
    }
}
