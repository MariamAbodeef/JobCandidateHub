using AutoFixture;
using FluentAssertions;
using JobCandidateHub.API.Controllers;
using JobCandidateHub.Application.Abstraction;
using JobCandidateHub.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JobCandidateHub.API.Tests.Controllers.Tests
{
    public class JobCandidateControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IJobCandidateService> _mockService;
        private readonly JobCandidateController _controller;

        public JobCandidateControllerTests()
        {
            _fixture = new Fixture();
            _mockService = new Mock<IJobCandidateService>();
            _controller = new JobCandidateController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllCandidates_ShouldReturnOk()
        {
            // Arrange

            var email = _fixture.Create<string>();
            var candidates = _fixture.Create<List<JobCandidateDto>>();
            _mockService.Setup(service => service.GetCandidates(email)).ReturnsAsync(candidates);


            // Act
            var result = await _controller.GetAllCandidates(email);


            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<List<JobCandidateDto>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(candidates);
            _mockService.Verify(x => x.GetCandidates(email), Times.Once());
        }

        [Fact]
        public async Task GetAllCandidates_WithNullEmail_ShouldReturnOk()
        {
            // Arrange
            string email = null;
            var candidates = _fixture.Create<List<JobCandidateDto>>();
            _mockService.Setup(service => service.GetCandidates(email)).ReturnsAsync(candidates);

            // Act
            var result = await _controller.GetAllCandidates(email);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<List<JobCandidateDto>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(candidates);
            _mockService.Verify(x => x.GetCandidates(email), Times.Once());

        }
        
        
        [Fact]
        public async Task GetAllCandidates_WithEmptyEmail_ShouldReturnOk()
        {
            // Arrange
            string email = String.Empty;
            var candidates = _fixture.Create<List<JobCandidateDto>>();
            _mockService.Setup(service => service.GetCandidates(email)).ReturnsAsync(candidates);

            // Act
            var result = await _controller.GetAllCandidates(email);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<List<JobCandidateDto>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(candidates);
            _mockService.Verify(x => x.GetCandidates(email), Times.Once());
        }

        [Fact]
        public async Task GetAllCandidates_WithNotExistEmail_ShouldReturnNotFound()
        {
            // Arrange
            string email = _fixture.Create<string>(); ;
            List<JobCandidateDto> candidates = null;
            _mockService.Setup(service => service.GetCandidates(email)).ReturnsAsync(candidates);

            // Act
            var result = await _controller.GetAllCandidates(email);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NotFoundResult>();
            _mockService.Verify(x => x.GetCandidates(email), Times.Once());

        }
        [Fact]
        public async Task AddOrUpdateCandidate_WithNullInput_ShouldReturnBadRequest()
        {
            // Arrange
            JobCandidateDto candidate = null;

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidate);

            // Assert
            result.Result.Should().BeAssignableTo<BadRequestResult>();

        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldReturnOk()
        {
            // Arrange
            var candidate = new JobCandidateDto();
            _mockService.Setup(service => service.AddOrUpdateCandidate(candidate)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidate);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().Be(true);
        }
    }
}
