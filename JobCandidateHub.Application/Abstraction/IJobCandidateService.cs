using JobCandidateHub.Domain.Dto;
using JobCandidateHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Abstraction
{
    public interface IJobCandidateService
    {
        Task<bool> AddOrUpdateCandidate(JobCandidateDto candidate);
        Task<List<JobCandidateDto>> GetCandidates(string? email);
    }
}
