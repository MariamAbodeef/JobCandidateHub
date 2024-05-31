using AutoFixture;
using AutoMapper;
using FluentAssertions;
using JobCandidateHub.Application.Abstraction;
using JobCandidateHub.Application.Services;
using JobCandidateHub.Domain.Dto;
using JobCandidateHub.Domain.Entities;
using Moq;
using Xunit;

namespace JobCandidateHub.Application.Tests.Services.Tests
{
    public class JobCandidateServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IJobCandidateRepository> _repositoryMock;

        public JobCandidateServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IJobCandidateRepository>();
        }

        [Fact]
        public async Task AddOrUpdateCandidate_WhenCandidateExists_ShouldUpdateCandidate()
        {
            // Arrange
            var existingCandidate = _fixture.Create<JobCandidateDto>();
            var updatedCandidate = _fixture.Create<JobCandidateDto>();
            updatedCandidate.Email = existingCandidate.Email;

            var repositoryMock = new Mock<IJobCandidateRepository>();
            repositoryMock.Setup(repo => repo.GetAllCandidatesAsQueryable(existingCandidate.Email)).Returns(_fixture.CreateMany<JobCandidate>().AsQueryable());
            repositoryMock.Setup(repo => repo.UpdateCandidateAsync(It.IsAny<JobCandidate>())).ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<JobCandidate>(updatedCandidate)).Returns(_fixture.Create<JobCandidate>());

            var service = new JobCandidateService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.AddOrUpdateCandidate(updatedCandidate);

            // Assert
            result.Should().BeTrue();
            repositoryMock.Verify(repo => repo.UpdateCandidateAsync(It.IsAny<JobCandidate>()), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_WhenCandidateDoesNotExist_ShouldAddCandidate()
        {
            // Arrange
            var newCandidate = _fixture.Create<JobCandidateDto>();

            _repositoryMock.Setup(repo => repo.GetAllCandidatesAsQueryable(newCandidate.Email)).Returns(Enumerable.Empty<JobCandidate>().AsQueryable());
            _repositoryMock.Setup(repo => repo.AddCandidateAsync(It.IsAny<JobCandidate>())).ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<JobCandidate>(newCandidate)).Returns(_fixture.Create<JobCandidate>());

            var service = new JobCandidateService(_repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.AddOrUpdateCandidate(newCandidate);

            // Assert
            result.Should().BeTrue();
            _repositoryMock.Verify(repo => repo.AddCandidateAsync(It.IsAny<JobCandidate>()), Times.Once);
        }

        [Fact]
        public async Task GetCandidates_WhenEmailIsNull_ShouldReturnAllCandidates()
        {
            // Arrange
            var candidates = _fixture.CreateMany<JobCandidate>().ToList();
            var expectedCandidates = _fixture.CreateMany<JobCandidateDto>().ToList();

            _repositoryMock.Setup(repo => repo.GetAllCandidatesAsListAsync(null)).ReturnsAsync(candidates);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<JobCandidateDto>>(candidates)).Returns(expectedCandidates);

            var service = new JobCandidateService(_repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.GetCandidates(null);

            // Assert
            result.Should().BeEquivalentTo(expectedCandidates);
        }

        [Fact]
        public async Task GetCandidates_WhenEmailIsProvided_ShouldReturnFilteredCandidates()
        {
            // Arrange
            var email = _fixture.Create<string>();
            var candidates = _fixture.CreateMany<JobCandidate>().ToList();
            var expectedCandidates = _fixture.CreateMany<JobCandidateDto>().ToList();

            _repositoryMock.Setup(repo => repo.GetAllCandidatesAsListAsync(email)).ReturnsAsync(candidates);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<JobCandidateDto>>(candidates)).Returns(expectedCandidates);

            var service = new JobCandidateService(_repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.GetCandidates(email);

            // Assert
            result.Should().BeEquivalentTo(expectedCandidates);
        }
    }
}
