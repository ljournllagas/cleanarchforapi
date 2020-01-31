using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Customers.Queries.Dtos
{
    public class GetCustomersDto : IMapFrom<Customer>
    {
        public List<GetCustomerDto> Customers { get; set; }
    }
}
