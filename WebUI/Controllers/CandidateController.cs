using DataAccess.Static;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create(int electionId)
        {
			return View(new Candidate {ElectionId = electionId});
        }

        [HttpPost]
        public async Task<IActionResult> Create(Candidate candidate)
        {
			if (ModelState.IsValid)
            {
                await _candidateService.AddCandidateAsync(candidate);
                return RedirectToAction("Edit", "Election", new {id = candidate.ElectionId, changeReferer = false});
            }     
            
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            Candidate? candidate = await _candidateService.GetCandidateByIdAsync(id, false);
            if(candidate == null)
            {
                return View("NotFound", new NotFoundVM("Candidate"));
            }
            return View(candidate);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            Candidate? candidate = await _candidateService.GetCandidateByIdAsync(id, false);
            if (candidate == null)
            {
                return View("NotFound", new NotFoundVM("Candidate"));
            }
            return View(candidate);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Candidate newCandidate) 
        {
            if (ModelState.IsValid)
            {
                await _candidateService.UpdateCandidateAsync(newCandidate);
                return RedirectToAction("Edit", "Election", new { id = newCandidate.ElectionId, changeReferer = false });

            }            
            return View(newCandidate);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            Candidate? candidate = await _candidateService.GetCandidateByIdAsync(id, true);            
            if(candidate != null)
            {
                int electionId = candidate.ElectionId;
                await _candidateService.RemoveCandidateAsync(candidate);
				return RedirectToAction("Edit", "Election", new { id = electionId, changeReferer = false });
			}
            return View("NotFound", new NotFoundVM("Candidate"));
        }
    }
}
