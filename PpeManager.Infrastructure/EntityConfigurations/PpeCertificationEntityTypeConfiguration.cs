using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .HasConversion(x => x.ToString(), x=> x)
                .IsRequired();
            builder.Property(x => x.Validity)
                .IsRequired();
            builder.Property(x => x.Durability)
                .IsRequired();
        }
    }
}
