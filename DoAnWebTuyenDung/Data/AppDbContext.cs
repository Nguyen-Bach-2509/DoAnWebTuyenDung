using DoAnWebTuyenDung.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace DoAnWebTuyenDung.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DoAnWebEntities")
        { }
            public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobCategoryMapping> JobCategoryMappings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobCategoryMapping>()
                .HasRequired(e => e.Job)
                .WithMany(c => c.JobCategoryMappings) 
                .HasForeignKey(e => e.CategoryID); //
        }
    }
}