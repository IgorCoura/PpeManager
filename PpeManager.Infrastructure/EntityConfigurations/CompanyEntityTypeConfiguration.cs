using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies", PpeManagerContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(X => X.IsValid);
            builder.Property(x => x.Id)
                .UseHiLo("companyseq", PpeManagerContext.DEFAULT_SCHEMA);
            builder.Property(x => x.Name)
                .HasConversion(x => x.ToString(), x => x)
                .IsRequired();
            builder.Property(x => x.Cnpj)
                .HasConversion(x => x.ToString(), x => x);
        }
    }
}
