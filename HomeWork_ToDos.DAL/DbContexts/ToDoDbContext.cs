using HomeWork_ToDos.CommonLib.Models.DbModels;
using HomeWork_ToDos.DAL.DbContexts.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HomeWork_ToDos.DAL.DbContexts
{
    public class ToDoDbContext : DbContext
    {
        /// <summary>
        /// Default Constructor required to enable Migrations
        /// </summary>
        public ToDoDbContext()
        {

        }

        public ToDoDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<ToDoListDbModel> ToDoLists { get; set; }
        public DbSet<ToDoItemDbModel> ToDoItems { get; set; }
        public DbSet<LabelDbModel> Labels { get; set; }
        public DbSet<MapLabelsToListDbModel> MapLabelsToLists { get; set; }
        public DbSet<MapLabelsToItemDbModel> MapLabelsToItems { get; set; }

        /// <summary>
        /// This method configures the database (and other options) to be used for ToDoDbContext.
        /// This method is called for each instance of the context that is created.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                     .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                     .AddJsonFile($"appsettings.{envName}.json", optional: false)
                    .Build();
                string connectionString = configuration.GetConnectionString("ToDoDbConnString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        /// <summary>
        /// All DbSet entities are configured in this method.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ToDoListEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ToDoItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LabelEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MapLabelsToListEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MapLabelsToItemEntityTypeConfiguration());
        }
    }
}
