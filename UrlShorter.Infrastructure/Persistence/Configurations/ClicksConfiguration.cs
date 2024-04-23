using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShorter.Domain.Entities;

namespace UrlShorter.Infrastructure.Persistence.Configurations
{
    public class ClicksConfiguration : IEntityTypeConfiguration<Clicks>
    {
        public void Configure(EntityTypeBuilder<Clicks> builder)
        {
            builder.ToTable("Clicks");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Url).WithMany(c => c.Clicks).HasForeignKey(c => c.UrlId);
            builder.Property(p => p.UserAgentString).IsRequired(false);

        }
    }
}
