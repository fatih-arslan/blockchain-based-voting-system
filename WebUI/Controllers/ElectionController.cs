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

        public async Task<IActionResult> Index()
        {
            var elections = await _electionService.GetAvailableElectionsAsync(false);            
            return View(elections);
        }

        public async Task<IActionResult> All()
        {
            var elections = await _electionService.GetAllElectionsAsync(true);
            return View(elections);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {            
            ViewData["Referer"] = Request.Headers["Referer"].ToString();
			return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Election election, string referer)
        {
            if(ModelState.IsValid)
            {                
                await _electionService.AddElectionAsync(election);
                return Redirect(referer);
            }            
            return View(election);
        }

        public async Task<IActionResult> Details(int id)
        {
            Election? election = await _electionService.GetElectionByIdAsync(id, false);
            if(election == null)
            {
                return View("NotFound", new NotFoundVM("Election"));
            }
            return View(election);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            Election? election = await _electionService.GetElectionByIdAsync(id, true);
            if (election == null)
            {
                return View("NotFound", new NotFoundVM("Election"));
            }
            ViewData["Referer"] = Request.Headers["Referer"].ToString();
            return View(election);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Election newElection, string referer)
        {
            if(ModelState.IsValid)
            {
                await _electionService.UpdateElectionAsync(newElection);
                return Redirect(referer);
            }            
            return View(newElection);
        }       

        public async Task<IActionResult> Delete(int id)
        {
            Election? election = await _electionService.GetElectionByIdAsync(id, true);
            if(election != null)
            {
                await _electionService.RemoveElectionAsync(election);
            }
            var referringUrl = Request.Headers["Referer"].ToString();
            return Redirect(string.IsNullOrEmpty(referringUrl) ? "/" : referringUrl);            
        }

        [Authorize(Roles = UserRoles.Voter)]
        public async Task<IActionResult> Vote(int id)
        {
            Election? election = await _electionService.GetElectionByIdAsync(id, false);
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
            ElectionResultVM result = await _electionService.GetElectionResultAsync(id);
            return View(result);
        }
    }
}
