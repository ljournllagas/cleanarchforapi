using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Customers.Queries.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<ActionResult<GetCustomerDto>>
    {
        public Guid Id { get; set; }

        public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, ActionResult<GetCustomerDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActionResult<GetCustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Customers.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Customers), request.Id);
                }
          
                var obj = _context.Customers.ProjectTo<GetCustomerDto>(_mapper.ConfigurationProvider).FirstOrDefault(a => a.Id == request.Id);

                return new OkObjectResult(obj);
            }
        }
    }
}
