using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Property(x => x.PpesNotDelivered)
                .IsRequired();
            builder.Property(x => x.DueDate)
                .IsRequired(false);


            builder.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .IsRequired();

            builder.HasMany(x => x.PpePossessions)
                .WithOne(x => x.Worker);

            builder.HasMany(x => x.Ppes)
                .WithMany(x => x.Workers);

        }
    }
}
