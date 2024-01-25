using DataAccess.Repositories;
using DataAccess.Repositories.Contracts;
using Entities.Models;
using Services.Utilities;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly DefaultImagePaths _defaultImagePaths;

        public CandidateService(ICandidateRepository candidateRepository, DefaultImagePaths defaultImagePaths)
        {
            _candidateRepository = candidateRepository;
            _defaultImagePaths = defaultImagePaths;
        }

        public async Task AddCandidateAsync(Candidate candidate)
        {
            if(candidate.ImageFile != null)
            {
                string fileName = FileHelper.SaveImage(candidate.ImageFile);
                candidate.ImagePath = $"/images/{fileName}";
            }
            else
            {
                candidate.ImagePath = _defaultImagePaths.Candidate;
            }
            await _candidateRepository.AddCandidateAsync(candidate);
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync(bool trackChanges)
        {
            return await _candidateRepository.GetAllCandidatesAsync(trackChanges);
        }

        public async Task<Candidate?> GetCandidateByIdAsync(int id, bool trackChanges)
        {
            return await _candidateRepository.GetCandidateByIdAsync(id, trackChanges);
        }

        public async Task RemoveCandidateAsync(Candidate candidate)
        {
            string? filePath = candidate.ImagePath;
            if (filePath != null && filePath != _defaultImagePaths.Candidate)
            {
                FileHelper.DeleteImage(filePath);
            }
            await _candidateRepository.RemoveCandidateAsync(candidate);
        }

        public async Task UpdateCandidateAsync(Candidate candidate)
        {
            if(candidate.ImageFile != null)
            {
                string? oldFilePath = candidate.ImagePath;
                if (oldFilePath != null && oldFilePath != _defaultImagePaths.Candidate)
                {
                    FileHelper.DeleteImage(oldFilePath);
                }
                string fileName = FileHelper.SaveImage(candidate.ImageFile);
                candidate.ImagePath = $"/images/{fileName}";
            }
            await _candidateRepository.UpdateCandidateAsync(candidate);
        }
    }
}
