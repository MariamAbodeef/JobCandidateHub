using JobCandidateHub.Application.Abstraction;
using JobCandidateHub.Domain.Dto;
using Microsoft.AspNetCore.Mvc;


namespace JobCandidateHub.API.Controllers
{
    [Route("api/job-candidate")]
    [ApiController]
    public class JobCandidateController : ControllerBase
    {
        private readonly IJobCandidateService _jobCandidateService;

        public JobCandidateController(IJobCandidateService jobCandidateService)
        {
            _jobCandidateService = jobCandidateService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<JobCandidateDto>>> GetAllCandidates([FromQuery] string email)
        {
            var candidates = await _jobCandidateService.GetCandidates(email);
            if(candidates is null) { return NotFound(); }
            return Ok(candidates);
        }

        
        
        [HttpPost]
        public async Task<ActionResult<JobCandidateDto>> AddOrUpdateCandidate([FromBody] JobCandidateDto input)
        {
            if(input is null)
            {
                return BadRequest();
            }
            var success = await _jobCandidateService.AddOrUpdateCandidate(input);
            return Ok(success);
        }
    }
}
