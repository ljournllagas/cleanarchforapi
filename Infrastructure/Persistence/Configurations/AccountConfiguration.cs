using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(t => t.AccountNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.AccountName)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
