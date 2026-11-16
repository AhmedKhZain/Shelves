using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Domain.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.HasOne(b => b.Authr)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthrId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(b => b.CurrentAvailabeCopies)
                .IsRequired()
                .HasDefaultValue(0);

        }
    }
}
