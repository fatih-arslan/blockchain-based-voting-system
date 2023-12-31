using DataAccess.Repositories;
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
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public void AddCandidate(Candidate candidate)
        {
            var fileName = candidate.ImageFile.FileName;
            var imagePath = Path.Combine("wwwroot/images", fileName);
            using(var stream = new FileStream(imagePath, FileMode.Create))
            {
                candidate.ImageFile.CopyTo(stream);
            }
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
            _candidateRepository.UpdateCandidate(candidate);
        }
    }
}
