using Microsoft.EntityFrameworkCore;
using Mission8.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mission8.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> TaskItem { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories (dropdown values from assignment)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Home" },
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
            );

            // Optional: seed a few starter tasks
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    TaskItemId = 1,
                    TaskName = "Finish Mission 8",
                    DueDate = DateTime.Today.AddDays(3),
                    Quadrant = 1,
                    CategoryId = 2,
                    Completed = false
                },
                new TaskItem
                {
                    TaskItemId = 2,
                    TaskName = "Weekly planning",
                    DueDate = DateTime.Today.AddDays(7),
                    Quadrant = 2,
                    CategoryId = 1,
                    Completed = false
                }
            );
        }
    }
}