async function voteForCandidate(electionId, electionName, candidateId, candidateName, userId) {
    try {
         //Check MetaMask availability
        if (window.ethereum) {
            var web3 = new Web3(window.ethereum);
            // Request user permission to interact with their MetaMask account
            await window.ethereum.enable();

            // Get the selected Ethereum address from MetaMask
            const accounts = await web3.eth.getAccounts();
            const userAddress = accounts[0];

			const contractAddress = '0xCBF182146C06955Dd6CC0D82c3A76DF22f56e788';
			const contractAbi = [
				{
					"inputs": [
						{
							"internalType": "uint256",
							"name": "_electionId",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "_electionName",
							"type": "string"
						},
						{
							"internalType": "uint256",
							"name": "_candidateId",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "_candidateName",
							"type": "string"
						}
					],
					"name": "vote",
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
					"name": "electionResults",
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
							"name": "_electionId",
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
							"internalType": "uint256[]",
							"name": "",
							"type": "uint256[]"
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
						}
					],
					"name": "votes",
					"outputs": [
						{
							"internalType": "address",
							"name": "userAddress",
							"type": "address"
						},
						{
							"internalType": "uint256",
							"name": "electionId",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "electionName",
							"type": "string"
						},
						{
							"internalType": "uint256",
							"name": "candidateId",
							"type": "uint256"
						},
						{
							"internalType": "string",
							"name": "candidateName",
							"type": "string"
						}
					],
					"stateMutability": "view",
					"type": "function"
				}
			]; 
           
            const contract = new web3.eth.Contract(contractAbi, contractAddress);
			
			const transaction = contract.methods.vote(electionId, electionName, candidateId, candidateName);
            
            const result = await transaction.send({
                from: userAddress
            });

			
		} else {
			alert('MetaMask not detected. Please install MetaMask and connect to your Ethereum account.');
		}


		await fetch('/Vote/AddVote', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify({
				// Provide the necessary vote details
				electionId: electionId,
				userId: userId,
				voteDate: new Date().toISOString()
			}),
		});

		window.location.href = `/Election/Vote/${electionId}`;

    } catch (error) {
        console.error('Error:', error);
    }
}
