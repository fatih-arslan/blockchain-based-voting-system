async function getResultsAndRedirect(electionId) {
    try {
        // Check MetaMask availability
        if (window.ethereum) {
            var web3 = new Web3(window.ethereum);
            // Request user permission to interact with their MetaMask account
            await window.ethereum.enable();

            // Get the selected Ethereum address from MetaMask
            const accounts = await web3.eth.getAccounts();
            const userAddress = accounts[0];

            // Replace 'YOUR_CONTRACT_ADDRESS' with your actual smart contract address
			const contractAddress = '0x7730dbdd9FC60Aa2a13Ed80A47817b8A90342Fe7';
			const contractAbi = [
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

            // Create a contract instance
            const contract = new web3.eth.Contract(contractAbi, contractAddress);

            // Replace 'getElectionResults' with the actual function in your smart contract for fetching results
            const results = await contract.methods.getElectionResults(electionId).call();

            // Redirect to the GetResults action with the results as a query parameter
            redirectToGetResults(electionId, results);
        } else {
            console.error('MetaMask not detected. Please install MetaMask and connect to your Ethereum account.');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

function redirectToGetResults(electionId, results) {
    // Assuming you are using the ASP.NET MVC route structure
    // Redirect to the GetResults action with the election ID and results as query parameters
    const resultsQueryParam = encodeURIComponent(JSON.stringify(results));
    window.location.href = `/Election/GetResults?electionId=${electionId}&results=${resultsQueryParam}`;
}

// Example usage:
// Call this function when the user clicks a button to see the results
document.getElementById("seeResultsBtn").addEventListener("click", function () {
    const electionId = 1; // Replace with the actual election ID
    getResultsAndRedirect(electionId);
});
