using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.Property(pi => pi.Src).IsRequired();
            builder.HasIndex(pi => pi.Src).IsUnique();

            builder.Property(pi => pi.Alt).IsRequired();
        }
    }
}
