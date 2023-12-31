using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICandidateService
    {
        void AddCandidate(Candidate candidate);
        Candidate? GetCandidateById(int id, bool trackChanges);
        IEnumerable<Candidate> GetAllCandidates(bool trackChanges);
        void UpdateCandidate(Candidate candidate);
        void RemoveCandidate(Candidate candidate);
    }
}
