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
    public class ElectionRepository : RepositoryBase<Election>, IElectionRepository
    {
        public ElectionRepository(AppDbContext context) : base(context)
        {
            
        }
        public async Task AddElectionAsync(Election election)
        {
            await AddAsync(election);
        }

        public async Task<IEnumerable<Election>> GetAllElectionsAsync(bool trackChanges)
        {
            return await GetAllAsync(trackChanges);
        }

        public async Task<Election?> GetElectionByIdAsync(int id, bool trackChanges)
        {
            return await GetByConditionAsync(e => e.Id == id, trackChanges, includeProperties:"Candidates");
        }

        public async Task RemoveElectionAsync(Election election)
        {
            await RemoveAsync(election);   
        }

        public async Task UpdateElectionAsync(Election election)
        {
            await UpdateAsync(election);
        }
    }
}
