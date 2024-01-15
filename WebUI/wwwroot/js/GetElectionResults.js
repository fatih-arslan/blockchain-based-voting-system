async function getElectionResults(electionId) {
    try {
        // Check MetaMask availability
        if (window.ethereum) {
            var web3 = new Web3(window.ethereum);
            // Request user permission to interact with their MetaMask account
            await window.ethereum.enable();

            // Replace 'YOUR_CONTRACT_ADDRESS' with your actual smart contract address
            const contractAddress = '0xD7ACd2a9FD159E69Bb102A1ca21C9a3e3A5F771B';
			const contractAbi = [
				{
					"anonymous": false,
					"inputs": [
						{
							"indexed": true,
							"internalType": "address",
							"name": "voter",
							"type": "address"
						},
						{
							"indexed": false,
							"internalType": "string",
							"name": "electionName",
							"type": "string"
						},
						{
							"indexed": false,
							"internalType": "string",
							"name": "candidateName",
							"type": "string"
						}
					],
					"name": "Voted",
					"type": "event"
				},
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "electionId",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "candidateName",
							"type": "string"
						}
					],
					"name": "addCandidate",
					"outputs": [],
					"stateMutability": "nonpayable",
					"type": "function"
				},
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "",
							"type": "uint256"
						},
						{
							"internalType": "uint256",
							"name": "",
							"type": "uint256"
						}
					],
					"name": "candidates",
					"outputs": [
						{
							"internalType": "uint256",
							"name": "id",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "name",
							"type": "string"
						},
						{
							"internalType": "uint256",
							"name": "voteCount",
							"type": "uint256"
						}
					],
					"stateMutability": "view",
					"type": "function"
				},
				{
					"inputs": [
						{
							"internalType": "string",
							"name": "electionName",
							"type": "string"
						}
					],
					"name": "createElection",
					"outputs": [],
					"stateMutability": "nonpayable",
					"type": "function"
				},
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "",
							"type": "uint256"
						}
					],
					"name": "electionNames",
					"outputs": [
						{
							"internalType": "string",
							"name": "",
							"type": "string"
						}
					],
					"stateMutability": "view",
					"type": "function"
				},
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "electionId",
							"type": "uint256"
						}
					],
					"name": "getElectionResults",
					"outputs": [
						{
							"internalType": "uint256[]",
							"name": "",
							"type": "uint256[]"
						},
						{
							"internalType": "string",
							"name": "",
							"type": "string"
						}
					],
					"stateMutability": "view",
					"type": "function"
				},
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "electionId",
							"type": "uint256"
						},
						{
							"internalType": "uint256",
							"name": "candidateId",
							"type": "uint256"
						}
					],
					"name": "getVoteCount",
					"outputs": [
						{
							"internalType": "uint256",
							"name": "",
							"type": "uint256"
						}
					],
					"stateMutability": "view",
					"type": "function"
				},
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "",
							"type": "uint256"
						},
						{
							"internalType": "address",
							"name": "",
							"type": "address"
						}
					],
					"name": "hasVoted",
					"outputs": [
						{
							"internalType": "bool",
							"name": "",
							"type": "bool"
						}
					],
					"stateMutability": "view",
					"type": "function"
				},
				{
					"inputs": [],
					"name": "totalElections",
					"outputs": [
						{
							"internalType": "uint256",
							"name": "",
							"type": "uint256"
						}
					],
					"stateMutability": "view",
					"type": "function"
				},
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "electionId",
							"type": "uint256"
						},
						{
							"internalType": "uint256",
							"name": "candidateId",
							"type": "uint256"
						}
					],
					"name": "vote",
					"outputs": [],
					"stateMutability": "nonpayable",
					"type": "function"
				}
			]; // Replace with your smart contract ABI

            // Create a contract instance
            const contract = new web3.eth.Contract(contractAbi, contractAddress);
            console.log('Election ID:', electionId);

            // Replace 'getElectionResults' with the actual function in your smart contract for getting results
            const results = await contract.methods.getElectionResults(electionId).call();

            console.log('Election Name:', results[1]);
            console.log('Vote Counts:', results[0]);
        } else {
            console.error('MetaMask not detected. Please install MetaMask and connect to your Ethereum account.');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}
