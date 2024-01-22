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
        Task AddElectionAsync(Election election);
        Task<IEnumerable<Election>> GetAllElectionsAsync(bool trackChanges);
        Task<Election?> GetElectionByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Election>> GetAvailableElectionsAsync(bool trackChanges);
        Task UpdateElectionAsync(Election election);
        Task RemoveElectionAsync(Election election);

    }
}
