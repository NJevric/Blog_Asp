using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(80);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(30);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(50);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();

            builder.HasMany(u => u.UserUseCases).WithOne(uuc => uuc.User).HasForeignKey(uuc => uuc.UserId);
            builder.HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        }
    }
}
