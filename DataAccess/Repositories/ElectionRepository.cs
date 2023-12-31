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
    public class ElectionRepository : RepositoryBase<Election>, IElectionRepository
    {
        public ElectionRepository(AppDbContext context) : base(context)
        {
            
        }
        public void AddElection(Election election)
        {
            Add(election);
        }

        public IEnumerable<Election> GetAllElections(bool trackChanges)
        {
            return GetAll(trackChanges);
        }

        public Election? GetElectionById(int id, bool trackChanges)
        {
            return GetByCondition(e => e.Id == id, trackChanges, includeProperties:"Candidates");
        }

        public void RemoveElection(Election election)
        {
            Remove(election);   
        }

        public void UpdateElection(Election election)
        {
            UpdateElection(election);
        }
    }
}
