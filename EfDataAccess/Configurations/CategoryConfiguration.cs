using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(x => x.Id).IsUnique();
           
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(c => c.PostCategories).WithOne(pc => pc.Category).HasForeignKey(pc => pc.CategoryId);
            
        }
    }
}
