using Application.Accounts.Queries.Dtos;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Customers.Queries.Dtos
{
    public class GetCustomerDto : IMapFrom<Customer>
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }

        public int Age { get; set; }

        public string EmailAddress { get; set; }

        public ICollection<AccountDto> Accounts { get; set; }
       
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, GetCustomerDto>()
                .ForMember(d => d.Fullname, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        }
    }
}
