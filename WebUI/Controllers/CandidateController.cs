using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;

namespace WebUI.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public IActionResult Create(int electionId)
        {
            return View(new Candidate {ElectionId = electionId});
        }

        [HttpPost]
        public IActionResult Create(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                _candidateService.AddCandidate(candidate);
                return RedirectToAction("Edit", "Election", new {id = candidate.ElectionId});
            }     
            
            return View();
        }
    }
}
