using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebUI.Controllers
{    
    public class ElectionController : Controller
    {
        private readonly IElectionService _electionService;

        public ElectionController(IElectionService electionService)
        {
            _electionService = electionService;
        }

        public IActionResult Index()
        {
            var elections = _electionService.GetAllElections(false);            
            return View(elections);
        }

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
            Election? election = _electionService.GetElectionById(id, true);
            return View(election);
        }
    }
}
