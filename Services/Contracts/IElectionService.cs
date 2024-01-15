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
        void AddElection(Election election);
        Election? GetElectionById(int id, bool trackChanges);
        IEnumerable<Election> GetAllElections(bool trackChanges);
        void UpdateElection(Election election);
        void RemoveElection(Election election);
        Task<ElectionResultVM> GetElectionResult(int id);

    }
}
