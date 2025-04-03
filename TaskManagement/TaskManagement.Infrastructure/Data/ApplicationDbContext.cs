using Microsoft.EntityFrameworkCore;
using System;
using TaskManagement.TaskManagement.Core.Entities;

namespace TaskManagement.TaskManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TaskManage> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskManage>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(t => t.Description)
                    .HasMaxLength(1000);

                entity.Property(t => t.ExecutionDateTime)
                    //.HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(t => t.Status)
                    .HasConversion<int>();

                entity.Property(t => t.CreatedAt);
                    //.HasDefaultValueSql("GETDATE()");

                entity.Property(t => t.UpdatedAt);
                    //.HasDefaultValueSql("GETDATE()");

                entity.Property(t => t.IsActive).HasDefaultValue(true);
            });
        }


    }
}
