using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string EmailAddress { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
        {
            public Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}