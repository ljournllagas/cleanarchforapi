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
    public class GetCustomersQuery : IRequest<ActionResult<GetCustomersDto>>
    {
        public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ActionResult<GetCustomersDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public  async Task<ActionResult<GetCustomersDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
            {

                var obj = await _context.Customers.ProjectTo<GetCustomerDto>(_mapper.ConfigurationProvider).ToListAsync();
                
                return new OkObjectResult(obj);
            }
        }
    }
}
