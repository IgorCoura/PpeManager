using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    internal class PpeEntityTypeConfiguration : IEntityTypeConfiguration<Ppe>
    {
        public void Configure(EntityTypeBuilder<Ppe> builder)
        {
            builder.ToTable("ppes", PpeManagerContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(X => X.IsValid);
            builder.Property(x => x.Id)
                .UseHiLo("ppeseq", PpeManagerContext.DEFAULT_SCHEMA);
            builder.Property(x => x.Name)
                .HasConversion(x => x.ToString(), x => x)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasConversion(x => x.ToString(), x => x)
                .IsRequired();

            builder.HasMany(x => x.PpeCertifications)
                .WithOne(x=> x.Ppe)
                .HasForeignKey("PpeId")
                .IsRequired();
        }
    }
}
