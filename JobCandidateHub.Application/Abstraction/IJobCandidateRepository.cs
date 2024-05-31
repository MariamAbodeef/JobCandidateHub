using JobCandidateHub.Domain.Entities;


namespace JobCandidateHub.Application.Abstraction
{
    public interface IJobCandidateRepository
    {
        IQueryable<JobCandidate> GetAllCandidatesAsQueryable(string? email);
        Task<List<JobCandidate>> GetAllCandidatesAsListAsync(string? email);
        Task<bool> UpdateCandidateAsync(JobCandidate candidate);
        Task<bool> AddCandidateAsync(JobCandidate candidate);


    }
}
