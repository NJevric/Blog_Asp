using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
           

            builder.Property(x => x.Content).IsRequired();
            

            builder.HasMany(p => p.PostCategories).WithOne(pc => pc.Post).HasForeignKey(pc => pc.PostId);
            builder.HasMany(p => p.PostImages).WithOne(pi => pi.Post).HasForeignKey(pi => pi.PostId);
            builder.HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(pi => pi.PostId);

        }
    }
}
