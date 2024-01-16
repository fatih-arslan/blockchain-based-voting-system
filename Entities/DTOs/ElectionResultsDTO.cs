using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    [FunctionOutput]
    public class ElectionResultsDTO
    {
        [Parameter("uint256[]", "_candidateIds", 1)]
        public List<uint> CandidateIds { get; set; }

        [Parameter("uint256[]", "_voteCounts", 2)]
        public List<uint> VoteCounts { get; set; }
    }
}
