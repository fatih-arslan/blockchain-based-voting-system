using AutoMapper;
using DataAccess.Static;
using Entities.DTOs;
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
        private readonly IMapper _mapper;

        public ElectionController(IElectionService electionService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _electionService = electionService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var elections = await _electionService.GetAvailableElectionsAsync(false);            
            return View(elections);
        }

        [Authorize(Roles = UserRoles.Admin)]
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
            ElectionUpdateDTO updateDTO = _mapper.Map<ElectionUpdateDTO>(election);             
            ViewData["Referer"] = Request.Headers["Referer"].ToString();
            return View(updateDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ElectionUpdateDTO newElection, string referer)
        {
            if(ModelState.IsValid)
            {
                Election election = _mapper.Map<Election>(newElection);
                await _electionService.UpdateElectionAsync(election);
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

    /*
     * 
     * 
    
    [Authorize(Roles = UserRoles.Admin)]
public async Task<IActionResult> Edit(int id)
{
    Election? election = await _electionService.GetElectionByIdAsync(id, includeCandidates: true);
    if (election == null)
    {
        return View("NotFound", new NotFoundVM("Election"));
    }

    // Map Election to ElectionUpdateDto
    ElectionUpdateDto electionUpdateDto = _mapper.Map<ElectionUpdateDto>(election); // Assuming you're using AutoMapper or similar

    ViewData["Referer"] = Request.Headers["Referer"].ToString();
    return View(electionUpdateDto); // Pass ElectionUpdateDto to the view instead of Election
}



     
     [HttpPost]
public async Task<IActionResult> Edit(ElectionUpdateDto electionUpdateDto, string referer)
{
    if(ModelState.IsValid)
    {
        // Fetch the current election from the database
        Election? currentElection = await _electionService.GetElectionByIdAsync(electionUpdateDto.Id, includeCandidates: true);

        if(currentElection == null)
        {
            return View("NotFound", new NotFoundVM("Election"));
        }

        // Map ElectionUpdateDto to the current Election entity
        _mapper.Map(electionUpdateDto, currentElection); // AutoMapper handles the mapping

        // Update the election
        await _electionService.UpdateElectionAsync(currentElection);

        return Redirect(referer);
    }            
    return View(electionUpdateDto); // Return DTO to the view if validation fails
}

     
     
     */
}
