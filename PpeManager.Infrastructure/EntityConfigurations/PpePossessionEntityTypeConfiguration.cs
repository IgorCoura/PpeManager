using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    class PpePossessionEntityTypeConfiguration : IEntityTypeConfiguration<PpePossession>
    {
        public void Configure(EntityTypeBuilder<PpePossession> builder)
        {
            builder.ToTable("ppePossessions", PpeManagerContext.DEFAULT_SCHEMA);
            
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.IsValid);

            builder.Property(x => x.Id).UseHiLo("ppePossessionseq", PpeManagerContext.DEFAULT_SCHEMA);

            builder.Property(x => x.IsDelivered)
                .IsRequired();

            builder.Property(x => x.DueDate)
                .IsRequired(false);

            builder.HasOne(x => x.Worker)
                .WithMany(x => x.PpePossessions)
                .HasForeignKey(x => x.WorkerId)
                .IsRequired();

            builder.HasOne(x => x.Ppe)
                .WithMany()
                .HasForeignKey(x => x.PpeId)
                .IsRequired();


            builder.HasMany(x => x.PossessionRecords)
                .WithOne(x => x.PpePossession)
                .HasForeignKey(x => x.PpePossessionId)
                .IsRequired(false);
        }
    }
}
