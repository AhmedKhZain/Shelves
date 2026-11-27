using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Domain.Persistence.Configurations
{
    public class AutherConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(a => a.Books)
                .WithOne(b => b.Authr)
                .HasForeignKey(b => b.AuthrId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.BirthDate)
                .IsRequired(false);

            builder.Property(a => a.Bio)
                .IsRequired(false)
                .HasMaxLength(1000);

        }
    }
}
