using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    internal class PossessionRecordEntityTypeConfiguration : IEntityTypeConfiguration<PossessionRecord>
    {
        public void Configure(EntityTypeBuilder<PossessionRecord> builder)
        {
            builder.ToTable("possessionRecord", PpeManagerContext.DEFAULT_SCHEMA);

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
            builder.Property(x => x.FilePath)
                .IsRequired(false);
            builder.Property(x => x.Quantity);

            builder.HasOne(x => x.PpeCertification)
                .WithMany()
                .HasForeignKey(x => x.PpeCertificationId)
                .IsRequired();

        }
    }
}
