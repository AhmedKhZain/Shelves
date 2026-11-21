using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shelves.Domain.Persistence.Configurations;

internal class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(c => c.Email)
            .HasMaxLength(320)
            .IsRequired(false);

        builder.HasIndex(c => c.Email)
            .IsUnique(true);


        builder.Property(c => c.NationalID)
            .HasMaxLength(14)
            .IsRequired(false);

        builder.HasIndex(c => c.NationalID)
            .IsUnique(true);



        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired(false);

        builder.HasIndex(c => c.PhoneNumber)
            .IsUnique(true);



    }
}
