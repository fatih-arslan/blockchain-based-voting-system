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
        Task AddCandidateAsync(Candidate candidate);
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync(bool trackChanges);
        Task<Candidate?> GetCandidateByIdAsync(int id, bool trackChanges);
        Task UpdateCandidateAsync(Candidate candidate);
        Task RemoveCandidateAsync(Candidate candidate);
    }
}
