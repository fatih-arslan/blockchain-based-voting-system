using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
            builder.Entity<Election>().HasData(
                new Election
                {
                    Title = "2023 Cumhurbaşkanlığı Seçimi",
                    Id = 1,
                    Description = "2023 Cumhurbaşkanlığı Seçimi",
                    StartDate = DateTime.SpecifyKind(new DateTime(2023, 12, 27), DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc),
                    ImagePath = "images/fors.png",
                }
                );
        }

        public DbSet<Election> Elections { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
    }
}
