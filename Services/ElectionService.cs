using DataAccess.Repositories.Contracts;
using Entities.Models;
using Entities.ViewModels;
using Services.Utilities;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Entities.DTOs;
using DataAccess.Static;

namespace Services
{

    public class ElectionService : IElectionService
    {
        private readonly IElectionRepository _electionRepository;

        public ElectionService(IElectionRepository electionRepository)
        {
            _electionRepository = electionRepository;
        }

        public async Task AddElectionAsync(Election election)
        {
            string fileName = FileHelper.DefaultElectionFileName;
            if(election.ImageFile != null)
            {
                fileName = FileHelper.SaveImage(election.ImageFile);

            }
            election.ImagePath = $"/images/{fileName}";
            await _electionRepository.AddElectionAsync(election);
        }

        public async Task<IEnumerable<Election>> GetAllElectionsAsync(bool trackChanges)
        {
            return await _electionRepository.GetAllElectionsAsync(trackChanges);
        }

        public async Task<Election?> GetElectionByIdAsync(int id, bool trackChanges)
        {
            return await _electionRepository.GetElectionByIdAsync(id, trackChanges);
        }

        public async Task RemoveElectionAsync(Election election)
        {
            string? filePath = election.ImagePath;
            if(filePath != null && filePath != FileHelper.DefaultElectionFilePath) 
            {
				FileHelper.DeleteImage(filePath);
			}
            foreach(Candidate c in election.Candidates)
            {
                if(c.ImagePath != null)
                {
                    FileHelper.DeleteImage(c.ImagePath);
                }
            }
            await _electionRepository.RemoveElectionAsync(election);
        }

        public async Task UpdateElectionAsync(Election election)
        {
            if(election.ImageFile != null)
            {
                string? oldFilePath = election.ImagePath;
                if(oldFilePath != null && oldFilePath != FileHelper.DefaultElectionFilePath)
                {                    
                    FileHelper.DeleteImage(oldFilePath);
                }
                string fileName = FileHelper.SaveImage(election.ImageFile);
                election.ImagePath = $"/images/{fileName}";
            }
            await _electionRepository.UpdateElectionAsync(election);
        }

        public async Task<ElectionResultVM> GetElectionResultAsync(int id)
        {
            string apiKey = APIKeys.InfuraSepoliaAPI;
            var web3 = new Web3($"https://sepolia.infura.io/v3/{apiKey}");

            string contractAddress = SmartConractConfig.ContractAddress;
            string contractAbi = SmartConractConfig.ContractAbi;

            var contract = web3.Eth.GetContract(contractAbi, contractAddress);

            uint electionIdToQuery = (uint)id;

            var getElectionResultsFunction = contract.GetFunction("getElectionResults");
            ElectionResultsDTO electionResults = await getElectionResultsFunction.CallDeserializingToObjectAsync<ElectionResultsDTO>(electionIdToQuery);

			var candidateIds = electionResults.CandidateIds;
			var voteCounts = electionResults.VoteCounts;
			Election? election = await _electionRepository.GetElectionByIdAsync(id, false);
			if (election == null)
			{
				return null;
			}

			List<Candidate> candidates = new List<Candidate>();
            int idCount = 0;
            for (int i = 0; i < election.Candidates.Count; i++)
			{				
				var candidate = election.Candidates[i];
				if (candidateIds.Contains((uint)candidate.Id))
				{
					candidate.VoteCount = (int)voteCounts[idCount++];
                    candidate.VotePercent = (float)(candidate.VoteCount * 1.0) / voteCounts.Sum(x => (int)x) * 100;
					candidates.Add(candidate);
				}
				else
				{
					candidate.VoteCount = 0;
					candidates.Add(candidate);
				}
			}

			return new ElectionResultVM { ElectionName = election.Title, Candidates = candidates };
		}
    }
	
}
