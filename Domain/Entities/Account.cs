using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Account : AuditableEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; }

        [Required]
        public AccountLevel AccountLevel { get; set; }

        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}