using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rinaldis.Core.Entities.Enquiry;
using Rinaldis.Shared.Enums;

namespace Rinaldis.Infrastructure.Data.EntityConfigurations;

public sealed class EnquiryConfiguration : IEntityTypeConfiguration<Enquiry>
{
    public void Configure(EntityTypeBuilder<Enquiry> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Phone).HasMaxLength(30);
        builder.Property(e => e.Message).IsRequired().HasMaxLength(2000);
        builder.Property(e => e.Status)
            .HasConversion<int>()
            .HasDefaultValue(EnquiryStatus.New);
        builder.HasIndex(e => e.Email);
    }
}
