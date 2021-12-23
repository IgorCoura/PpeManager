using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    class PpePossessionEntityTypeConfiguration : IEntityTypeConfiguration<PpePossession>
    {
        public void Configure(EntityTypeBuilder<PpePossession> builder)
        {
            builder.ToTable("ppePossession", PpeManagerContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(X => X.Notifications);
            builder.Ignore(X => X.IsValid);
            builder.Property(x => x.DeliveryDate)
                .IsRequired();
            builder.Property(x => x.Validity)
                .IsRequired();
            builder.Property(x => x.Confirmation)
                .IsRequired();
            builder.Property(x => x.SupportingDocument);
            builder.Property(x => x.Quantity);


            builder.HasOne(x => x.PpeCertification)
                .WithMany()
                .HasForeignKey("PpeCertificationId")
                .IsRequired();
        }
    }
}
