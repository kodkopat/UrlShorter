using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShorter.Domain.Entities;

namespace UrlShorter.Infrastructure.Persistence.Configurations
{
    public class UrlsConfiguration : IEntityTypeConfiguration<Urls>
    {
        public void Configure(EntityTypeBuilder<Urls> builder)
        {
            builder.ToTable("Urls");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Url).IsRequired();
            builder.Property(p => p.Key).IsRequired();
        }
    }
}
