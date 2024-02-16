using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IElectionService
    {
        Task AddElectionAsync(Election election);
        Task<Election?> GetElectionByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Election>> GetAllElectionsAsync(bool trackChanges);
        Task<IEnumerable<Election>> GetAvailableElectionsAsync(bool trackChanges);
        Task UpdateElectionAsync(Election election);
        Task RemoveElectionAsync(Election election);
        Task<ElectionResultVM> GetElectionResultAsync(int id);
        Task<(Election? election, bool alreadyVoted)> GetElectionVotingDetailsAsync(int electionId, string userId);


	}
}
