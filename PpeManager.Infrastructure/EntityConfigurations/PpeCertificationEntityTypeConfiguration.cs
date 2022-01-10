using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    class PpeCertificationEntityTypeConfiguration : IEntityTypeConfiguration<PpeCertification>
    {
        public void Configure(EntityTypeBuilder<PpeCertification> builder)
        {
            builder.ToTable("ppeCertifications", PpeManagerContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(X => X.IsValid);


            builder.Property(x => x.Id)
                .UseHiLo("ppeCertificationseq", PpeManagerContext.DEFAULT_SCHEMA);

            builder.Property(x => x.ApprovalCertificateNumber)
                .HasConversion(x => x.ToString(), x => x)
                .IsRequired();
            builder.Property(x => x.Validity)
                .IsRequired();
            builder.Property(x => x.Durability)
                .IsRequired();

            builder.HasOne(x => x.Ppe)
                .WithMany(x => x.PpeCertifications)
                .HasForeignKey(x => x.PpeId);
        }
    }
}
