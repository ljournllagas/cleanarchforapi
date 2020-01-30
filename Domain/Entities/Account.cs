using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Account : AuditableEntity
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public AccountLevel AccountLevel { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}