using HolyQuran.Data.Models;
using HolyQuran.Data.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace HolyQuran.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Teacher>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Recording>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Evaluation>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Note>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<SpecificNote>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<GeneralNote>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<EmailLog>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Logging>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Notification>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<TajweedRule>().HasQueryFilter(x => !x.IsDelete);


            builder.Entity<Note>().HasIndex(x => x.EvaluationId).IsUnique();
            builder.Entity<Evaluation>().HasOne(x => x.Recording).WithOne().IsRequired();
            
            base.OnModelCreating(builder);
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Recording> Recordings { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<SpecificNote> SpecificNotes { get; set; }
        public DbSet<GeneralNote> GeneralNotes { get; set; }
        public DbSet<EmailLog> EmailLog { get; set; }
        public DbSet<Logging> Logging { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<TajweedRule> TajweedRules { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Chapter> Chapters { get; set; }


    }
}