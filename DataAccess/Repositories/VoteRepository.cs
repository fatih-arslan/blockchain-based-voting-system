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
    public class VoteRepository : RepositoryBase<Vote>, IVoteRepository
    {
        public VoteRepository(AppDbContext context) : base(context)
        {
                    
        }
        public async Task AddVoteAsync(Vote vote)
        {
            await AddAsync(vote);
        }

		public async Task<bool> UserAlreadyVotedAsync(string userId, int electionId)
		{
			var vote = await GetByConditionAsync(v => v.UserId == userId && v.ElectionId == electionId, trackChanges: false);
			return vote != null;
		}
	}
}
