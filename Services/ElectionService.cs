using DataAccess.Repositories.Contracts;
using Entities.Models;
using Services.CommonUtilities;
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
            string fileName = FileHelper.DefaultFileName;
            if(election.ImageFile != null)
            {
                fileName = FileHelper.SaveImage(election.ImageFile);

            }
            election.ImagePath = $"/images/{fileName}";
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
            string filePath = election.ImagePath;
            if(filePath != null && filePath != FileHelper.DefaultFilePath) 
            {
				FileHelper.DeleteImage(filePath);
			}
            _electionRepository.RemoveElection(election);
        }

        public void UpdateElection(Election election)
        {
            if(election.ImageFile != null)
            {
                string? oldFilePath = election.ImagePath;
                if(oldFilePath != null && oldFilePath != FileHelper.DefaultFilePath)
                {                    
                    FileHelper.DeleteImage(oldFilePath);
                }
                string fileName = FileHelper.SaveImage(election.ImageFile);
                election.ImagePath = $"/images/{fileName}";
            }
            _electionRepository.UpdateElection(election);
        }
    }
}
