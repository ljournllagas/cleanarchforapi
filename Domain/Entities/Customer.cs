using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class Customer : AuditableEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public int Age { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public ICollection<Account> Accounts { get; set; }

    }
}