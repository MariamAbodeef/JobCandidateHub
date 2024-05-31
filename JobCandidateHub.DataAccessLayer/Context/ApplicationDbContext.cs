using JobCandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JobCandidateHub.DataAccessLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobCandidate>()
            .HasKey(c => c.Email);
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<JobCandidate> JobCandidates { get; set; }

    }
}
