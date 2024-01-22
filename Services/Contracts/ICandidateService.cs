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
        Task AddCandidateAsync(Candidate candidate);
        Task<Candidate?> GetCandidateByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync(bool trackChanges);
        Task UpdateCandidateAsync(Candidate candidate);
        Task RemoveCandidateAsync(Candidate candidate);
    }
}
