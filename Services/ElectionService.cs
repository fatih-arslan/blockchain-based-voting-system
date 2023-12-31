using DataAccess.Repositories.Contracts;
using Entities.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ElectionService : IElectionService
    {
        private readonly IElectionRepository _electionRepository;

        public ElectionService(IElectionRepository electionRepository)
        {
            _electionRepository = electionRepository;
        }

        public void AddElection(Election election)
        {
            var fileName = election.ImageFile.FileName;
            var imagePath = Path.Combine("wwwroot/images", fileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                election.ImageFile.CopyTo(stream);
            }
            election.ImagePath = $"images/{fileName}";
            _electionRepository.AddElection(election);
        }

        public IEnumerable<Election> GetAllElections(bool trackChanges)
        {
            return _electionRepository.GetAllElections(trackChanges).OrderBy(e => e.Id).ToList();
        }

        public Election? GetElectionById(int id, bool trackChanges)
        {
            return _electionRepository.GetElectionById(id, trackChanges);
        }

        public void RemoveElection(Election election)
        {
            _electionRepository.RemoveElection(election);
        }

        public void UpdateElection(Election election)
        {
            _electionRepository.UpdateElection(election);
        }
    }
}
