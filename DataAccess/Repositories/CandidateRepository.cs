using DataAccess.Data;
using DataAccess.Repositories.Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext context) : base(context)
        {
            
        }
        public async Task AddCandidateAsync(Candidate candidate)
        {
            await AddAsync(candidate);
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync(bool trackChanges)
        {
            return await GetAllAsync(trackChanges);
        }

        public async Task<Candidate?> GetCandidateByIdAsync(int id, bool trackChanges)
        {
            return await GetByConditionAsync(c => c.Id == id, trackChanges);
        }

        public async Task RemoveCandidateAsync(Candidate candidate)
        {
            await RemoveAsync(candidate);
        }

        public async Task UpdateCandidateAsync(Candidate candidate)
        {
            await UpdateAsync(candidate);
        }
    }
}
