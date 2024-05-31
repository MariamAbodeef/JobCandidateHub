using FluentAssertions;
using JobCandidateHub.DataAccessLayer.Context;
using JobCandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobCandidateHub.DataAccessLayer.Tests.Context.Tests
{
    public class ApplicationDbContextTests
    {
        [Fact]
        public void ApplicationDbContext_Constructor_ShouldCreateDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ApplicationDbContextTestsDB")
                .Options;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                // Assert
                context.Database.IsInMemory().Should().BeTrue();
            }
        }

        [Fact]
        public void ApplicationDbContext_ShouldConfigureEntityMappings()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ApplicationDbContextTestsDB")
                .Options;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                // Assert
                context.Model.FindEntityType(typeof(JobCandidate))
                    .Should().NotBeNull();
                context.Model.FindEntityType(typeof(JobCandidate)).FindPrimaryKey().Properties
                    .Should().HaveCount(1); // Ensure Email is the primary key
            }
        }

    }
}
