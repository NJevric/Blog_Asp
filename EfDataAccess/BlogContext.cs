using Blog.Domain;
using EfDataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace EfDataAccess
{
    public class BlogContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configurations
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostImageConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // soft delete filters
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Post>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<PostImage>().HasQueryFilter(x => !x.IsDeleted);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.Now;
                            e.IsDeleted = false;
                            e.UpdatedAt = null;
                            e.DeletedAt = null;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.Now;
                            break;
                    }
                }
            }


            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=blog;Integrated Security=True");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCase { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
