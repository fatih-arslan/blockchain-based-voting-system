using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Static
{
    public class SmartConractConfig
    {
        public static string ContractAddress = "0xCBF182146C06955Dd6CC0D82c3A76DF22f56e788";
        public static string ContractAbi = @"
              [
				{
					""inputs"": [
						{
							""internalType"": ""uint256"",
							""name"": """",
							""type"": ""uint256""
						},
						{
							""internalType"": ""uint256"",
							""name"": """",
							""type"": ""uint256""
						}
					],
					""name"": ""electionResults"",
					""outputs"": [
						{
							""internalType"": ""uint256"",
							""name"": """",
							""type"": ""uint256""
						}
					],
					""stateMutability"": ""view"",
					""type"": ""function""
				},
				{
					""inputs"": [
						{
							""internalType"": ""uint256"",
							""name"": ""_electionId"",
							""type"": ""uint256""
						}
					],
					""name"": ""getElectionResults"",
					""outputs"": [
						{
							""internalType"": ""uint256[]"",
							""name"": """",
							""type"": ""uint256[]""
						},
						{
							""internalType"": ""uint256[]"",
							""name"": """",
							""type"": ""uint256[]""
						}
					],
					""stateMutability"": ""view"",
					""type"": ""function""
				},
				{
					""inputs"": [
						{
							""internalType"": ""uint256"",
							""name"": ""_electionId"",
							""type"": ""uint256""
						},
						{
							""internalType"": ""string"",
							""name"": ""_electionName"",
							""type"": ""string""
						},
						{
							""internalType"": ""uint256"",
							""name"": ""_candidateId"",
							""type"": ""uint256""
						},
						{
							""internalType"": ""string"",
							""name"": ""_candidateName"",
							""type"": ""string""
						}
					],
					""name"": ""vote"",
					""outputs"": [],
					""stateMutability"": ""nonpayable"",
					""type"": ""function""
				},
				{
					""inputs"": [
						{
							""internalType"": ""uint256"",
							""name"": """",
							""type"": ""uint256""
						}
					],
					""name"": ""votes"",
					""outputs"": [
						{
							""internalType"": ""address"",
							""name"": ""userAddress"",
							""type"": ""address""
						},
						{
							""internalType"": ""uint256"",
							""name"": ""electionId"",
							""type"": ""uint256""
						},
						{
							""internalType"": ""string"",
							""name"": ""electionName"",
							""type"": ""string""
						},
						{
							""internalType"": ""uint256"",
							""name"": ""candidateId"",
							""type"": ""uint256""
						},
						{
							""internalType"": ""string"",
							""name"": ""candidateName"",
							""type"": ""string""
						}
					],
					""stateMutability"": ""view"",
					""type"": ""function""
				}
			]";
    }
}
