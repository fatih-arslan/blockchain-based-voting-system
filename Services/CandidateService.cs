using DataAccess.Repositories;
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
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public void AddCandidate(Candidate candidate)
        {
            string fileName = FileHelper.SaveImage(candidate.ImageFile);
            candidate.ImagePath = $"images/{fileName}";
            _candidateRepository.AddCandidate(candidate);
        }

        public IEnumerable<Candidate> GetAllCandidates(bool trackChanges)
        {
            return _candidateRepository.GetAllCandidates(trackChanges);
        }

        public Candidate? GetCandidateById(int id, bool trackChanges)
        {
            return _candidateRepository.GetCandidateById(id, trackChanges);
        }

        public void RemoveCandidate(Candidate candidate)
        {
            _candidateRepository.RemoveCandidate(candidate);
        }

        public void UpdateCandidate(Candidate candidate)
        {
            if(candidate.ImageFile != null)
            {
                string fileName = FileHelper.SaveImage(candidate.ImageFile);
                candidate.ImagePath = $"images/{fileName}";
            }
            _candidateRepository.UpdateCandidate(candidate);
        }
    }
}
