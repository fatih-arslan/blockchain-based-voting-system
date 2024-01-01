using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using System.ComponentModel.Design;

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

        public IActionResult Details(int id)
        {
            Candidate? candidate = _candidateService.GetCandidateById(id, false);
            return View(candidate);
        }

        public IActionResult Edit(int id)
        {
            Candidate? candidate = _candidateService.GetCandidateById(id, false);
            return View(candidate);
        }

        [HttpPost]
        public IActionResult Edit(Candidate newCandidate) 
        {
            if (ModelState.IsValid)
            {
                _candidateService.UpdateCandidate(newCandidate);
                return RedirectToAction("Edit", "Election", new { id = newCandidate.ElectionId });

            }            
            return View(newCandidate);
        }

        public IActionResult Delete(int id)
        {
            Candidate? candidate = _candidateService.GetCandidateById(id, true);            
            if(candidate != null)
            {
                int electionId = candidate.ElectionId;
                _candidateService.RemoveCandidate(candidate);
				return RedirectToAction("Edit", "Election", new { id = electionId });
			}
            return View("NotFound");
		}
    }
}
