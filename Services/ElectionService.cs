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

        public void AddElection(Election election)
        {
            string fileName = FileHelper.DefaultElectionFileName;
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
            string? filePath = election.ImagePath;
            if(filePath != null && filePath != FileHelper.DefaultElectionFilePath) 
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
                if(oldFilePath != null && oldFilePath != FileHelper.DefaultElectionFilePath)
                {                    
                    FileHelper.DeleteImage(oldFilePath);
                }
                string fileName = FileHelper.SaveImage(election.ImageFile);
                election.ImagePath = $"/images/{fileName}";
            }
            _electionRepository.UpdateElection(election);
        }

        public async Task<ElectionResultVM> GetElectionResult(int id)
        {
            // Ethereum web3 instance using MetaMask's injected Web3
            var web3 = new Web3("https://sepolia.infura.io/v3/51bd736f18d645e7b4832e8803d5a257");

            string contractAddress = SmartConractConfig.ContractAddress;
            string contractAbi = SmartConractConfig.ContractAbi;

            // Create contract instance
            var contract = web3.Eth.GetContract(contractAbi, contractAddress);

            // Replace with the electionId you want to query
            uint electionIdToQuery = (uint)id;

            // Query election results
            var getElectionResultsFunction = contract.GetFunction("getElectionResults");
            ElectionResultsDTO electionResults = await getElectionResultsFunction.CallDeserializingToObjectAsync<ElectionResultsDTO>(electionIdToQuery);

			var candidateIds = electionResults.CandidateIds;
			var voteCounts = electionResults.VoteCounts;
			Election? election = _electionRepository.GetElectionById(id, false);
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
