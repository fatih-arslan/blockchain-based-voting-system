using DataAccess.Repositories.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVoteRepository _voteRepository;

        public VoteController(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddVote([FromBody] Vote vote)
        {
            await _voteRepository.AddVoteAsync(vote);
            return Ok(); 
        }
    }
}
