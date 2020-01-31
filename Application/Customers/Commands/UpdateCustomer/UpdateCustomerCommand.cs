using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string EmailAddress { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
        {
            private readonly IApplicationDbContext _context;
          
            public UpdateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Customers.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Customers), request.Id);
                }

                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.Age = request.Age;
                entity.EmailAddress = request.EmailAddress;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}