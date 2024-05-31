using JobCandidateHub.Application.Abstraction;
using JobCandidateHub.DataAccessLayer.Context;
using JobCandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace JobCandidateHub.DataAccessLayer.Repositories
{
    public class JobCandidateRepository : IJobCandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public JobCandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateCandidateAsync(JobCandidate candidate)
        {
            try
            {

                candidate.ModifiedOn = DateTime.Now;
                _context.Update(candidate);
                var saved = await _context.SaveChangesAsync();
                return saved > 0;
            }
            catch (DbException ex)
            {
                throw new Exception("Error updating candidate. See inner exception for details.", ex);
            }
        }
        public async Task<bool> AddCandidateAsync(JobCandidate candidate)
        {
            
            try
            {
                candidate.CreatedOn = DateTime.Now;
                await _context.JobCandidates.AddAsync(candidate);
                var saved = await _context.SaveChangesAsync();
                return saved > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding candidate. See inner exception for details.", ex);
            }
        }

        public IQueryable<JobCandidate> GetAllCandidatesAsQueryable(string? email)
        {
            return _context.JobCandidates.AsQueryable().Where(c=> String.IsNullOrWhiteSpace(email) || c.Email == email );
        }
        
        public async Task<List<JobCandidate>> GetAllCandidatesAsListAsync(string? email)
        {
            return await _context.JobCandidates.Where(c=> String.IsNullOrWhiteSpace(email) || c.Email == email ).OrderByDescending(c=>c.CreatedOn).ToListAsync();
        }
    }
}
