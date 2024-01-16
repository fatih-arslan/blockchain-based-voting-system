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
        public void AddVote(Vote vote)
        {
            Add(vote);
        }
    }
}
