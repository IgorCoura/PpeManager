using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    class WorkerEntityTypeConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("workers", PpeManagerContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(X => X.IsValid);
            builder.Property(x => x.Id)
                .UseHiLo("workerseq", PpeManagerContext.DEFAULT_SCHEMA);

            builder.Property(x => x.Name)
                .HasConversion(x => x.ToString(), x => x)
                .HasColumnName("Name")
                .IsRequired();
            builder.Property(x => x.Cpf)
                .HasConversion(x => x.ToString(), x => x)
                .IsRequired();
            builder.Property(x => x.IsOpenPpePossessionProcess)
                .IsRequired();
            builder.Property(x => x.RegistrationNumber)
                .IsRequired();
            builder.Property(x => x.Role)
                .IsRequired();
            builder.Property(x => x.AdmissionDate)
                .IsRequired();


            builder.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey("CompanyId")
                .IsRequired();

            builder.HasMany(x => x.PpePossessions)
                .WithOne(x => x.Worker)
                .HasForeignKey("WorkerId")
                .IsRequired();

            builder.HasMany(x => x.Ppes)
                .WithMany(x => x.Workers);

        }
    }
}
