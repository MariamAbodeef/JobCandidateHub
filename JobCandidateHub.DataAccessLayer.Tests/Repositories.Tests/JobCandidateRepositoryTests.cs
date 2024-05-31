using JobCandidateHub.DataAccessLayer.Context;
using JobCandidateHub.DataAccessLayer.Repositories;
using JobCandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using FluentAssertions;
using AutoFixture;
using System.Data.Common;

namespace JobCandidateHub.DataAccessLayer.Tests.Repositories.Tests
{
    public class JobCandidateRepositoryTests
    {
        [Fact]
        public async Task AddCandidateAsync_ShouldAddCandidate()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RepositoryTestsDB")
                .Options;

            var mockContext = new ApplicationDbContext(options);
            var repository = new JobCandidateRepository(mockContext);

            var candidate = new JobCandidate { Email = "tst@me.me" , FirstName="tst", LastName="tst" };

            // Act
            var result = await repository.AddCandidateAsync(candidate);

            // Assert
            result.Should().BeTrue();
            mockContext.JobCandidates.Should().Contain(candidate);
        }

        [Fact]
        public void GetAllCandidatesAsQueryable_ShouldReturnFilteredCandidates()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RepositoryTestsDB")
                .Options;

            var candidates = new List<JobCandidate>
            {
                new JobCandidate { Email = "tst1@me.me", FirstName="tst1", LastName= "tst1" },
                new JobCandidate { Email = "tst2@me.me", FirstName="tst2", LastName= "tst2"},
                new JobCandidate { Email = "tst3@me.me", FirstName="tst3", LastName= "tst3"}
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.JobCandidates.AddRange(candidates);
                context.SaveChanges();
            }

            var mockContext = new ApplicationDbContext(options);
            var repository = new JobCandidateRepository(mockContext);

            // Act
            var result = repository.GetAllCandidatesAsQueryable("tst2@me.me").ToList();

            // Assert
            result.Should().HaveCount(1);
            result[0].Email.Should().Be("tst2@me.me");
        }

        

    }

}
