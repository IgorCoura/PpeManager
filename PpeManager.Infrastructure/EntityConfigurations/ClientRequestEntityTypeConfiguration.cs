using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopOnContainers.Services.Ordering.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Infrastructure.EntityConfigurations
{
    public class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
        {
            requestConfiguration.ToTable("requests", PpeManagerContext.DEFAULT_SCHEMA);
            requestConfiguration.HasKey(cr => cr.Id);
            requestConfiguration.Property(cr => cr.Name).IsRequired();
            requestConfiguration.Property(cr => cr.Time).IsRequired();
        }
    }
}
