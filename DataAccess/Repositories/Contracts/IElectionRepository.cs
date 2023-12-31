using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contracts
{
    public interface IElectionRepository : IRepositoryBase<Election>
    {
        void AddElection(Election election);
        IEnumerable<Election> GetAllElections(bool trackChanges);
        Election? GetElectionById(int id, bool trackChanges);
        void UpdateElection(Election election);
        void RemoveElection(Election election);

    }
}
