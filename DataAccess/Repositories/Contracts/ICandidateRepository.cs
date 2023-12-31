using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contracts
{
    public interface ICandidateRepository
    {
        void AddCandidate(Candidate candidate);
        IEnumerable<Candidate> GetAllCandidates(bool trackChanges);
        Candidate? GetCandidateById(int id, bool trackChanges); 
        void UpdateCandidate(Candidate candidate);
        void RemoveCandidate(Candidate candidate);
    }
}
