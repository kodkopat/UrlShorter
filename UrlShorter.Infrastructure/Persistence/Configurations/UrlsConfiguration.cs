using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            builder.Property(p => p.ShortUrl).IsRequired();
            builder.Property(p => p.Coubt).HasDefaultValue(0);
        }
    }
}
