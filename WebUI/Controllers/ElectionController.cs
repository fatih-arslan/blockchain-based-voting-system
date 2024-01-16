using DataAccess.Static;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;

namespace WebUI.Controllers
{    
    public class ElectionController : Controller
    {
        private readonly IElectionService _electionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ElectionController(IElectionService electionService, UserManager<ApplicationUser> userManager)
        {
            _electionService = electionService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var elections = _electionService.GetAllElections(false);            
            return View(elections);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Election election)
        {
            if(ModelState.IsValid)
            {                
                _electionService.AddElection(election);
                return RedirectToAction("Index");
            }            
            return View(election);
        }

        public IActionResult Details(int id)
        {
            Election? election = _electionService.GetElectionById(id, false);
            if(election == null)
            {
                return View("NotFound", new NotFoundVM("Election"));
            }
            return View(election);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Edit(int id)
        {
            Election? election = _electionService.GetElectionById(id, true);
            if (election == null)
            {
                return View("NotFound", new NotFoundVM("Election"));
            }
            return View(election);
        }

        [HttpPost]
        public IActionResult Edit(Election newElection)
        {
            if(ModelState.IsValid)
            {
                _electionService.UpdateElection(newElection);
                return RedirectToAction("Index");
            }            
            return View(newElection);
        }       

        public IActionResult Delete(int id)
        {
            Election? election = _electionService.GetElectionById(id, true);
            if(election != null)
            {
                _electionService.RemoveElection(election);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = UserRoles.Voter)]
        public async Task<IActionResult> Vote(int id)
        {
            Election? election = _electionService.GetElectionById(id, false);
            ApplicationUser currentUser =  await _userManager.GetUserAsync(User);
            currentUser = _userManager.Users.Include(u => u.Votes).FirstOrDefault(u => u.Id == currentUser.Id);
            bool alreadyVoted = currentUser.Votes.Any(vo => vo.ElectionId == id);
            VoteVM voteVM = new VoteVM { UserAlreadyVoted = alreadyVoted, Election = election };
            if (election == null)
            {
                return View("NotFound", new NotFoundVM("Election"));
            }
            return View(voteVM);
        }

        public async Task<IActionResult> Results(int id)
        {
            ElectionResultVM result = await _electionService.GetElectionResult(id);
            return View(result);
        }
    }
}
