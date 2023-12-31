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
        public void AddCandidate(Candidate candidate)
        {
            Add(candidate);
        }

        public IEnumerable<Candidate> GetAllCandidates(bool trackChanges)
        {
            return GetAll(trackChanges);
        }

        public Candidate? GetCandidateById(int id, bool trackChanges)
        {
            return GetByCondition(c => c.Id == id, trackChanges);
        }

        public void RemoveCandidate(Candidate candidate)
        {
            Remove(candidate);
        }

        public void UpdateCandidate(Candidate candidate)
        {
            Update(candidate);
        }
    }
}
