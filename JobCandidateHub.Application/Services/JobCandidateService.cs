using AutoMapper;
using JobCandidateHub.Application.Abstraction;
using JobCandidateHub.Domain.Dto;
using JobCandidateHub.Domain.Entities;

namespace JobCandidateHub.Application.Services
{
    public class JobCandidateService : IJobCandidateService
    {
        private readonly IJobCandidateRepository _jobCandidateRepository;
        private readonly IMapper _mapper;
        public JobCandidateService(IJobCandidateRepository jobCandidateRepository, IMapper mapper)
        {
            _jobCandidateRepository = jobCandidateRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrUpdateCandidate(JobCandidateDto candidate)
        {
            var existCandidate = _jobCandidateRepository.GetAllCandidatesAsQueryable(candidate.Email);
            if( existCandidate.Any() )
            {
                return await _jobCandidateRepository.UpdateCandidateAsync(_mapper.Map<JobCandidate>(candidate));
            }else
            {
                return await _jobCandidateRepository.AddCandidateAsync(_mapper.Map<JobCandidate>(candidate));
            }
        }

        public async Task<List<JobCandidateDto>> GetCandidates(string? email)
        {
            return  _mapper.Map<List<JobCandidateDto>>( await _jobCandidateRepository.GetAllCandidatesAsListAsync(email));
        }

        

        
    }
}
