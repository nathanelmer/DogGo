using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using System.Collections.Generic;
using DogGo.Models;
using DogGo.Models.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace DogGo.Controllers
{

    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IOwnerRepository ownerRepository)
        {
            _walkerRepo = walkerRepository;
            _ownerRepo = ownerRepository;
        }
        // GET: WalkersController
        // GET: Walkers
        public ActionResult Index()
        {
            List<Walker> walkers = _walkerRepo.GetAllWalkers();

            int ownerId = GetCurrentUserId();
            if (ownerId != 0)
            {
                Owner owner = _ownerRepo.GetOwnerById(ownerId);
                List<Walker> matchedByNeighborhood = walkers.Where(w => w.NeighborhoodId == owner.NeighborhoodId).ToList();
                return View(matchedByNeighborhood);
            }
            else
            {
                return View(walkers);
            }
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walkerRepo.GetWalksForWalker(walker.Id);

            WalkerFormModel wm = new WalkerFormModel
            {
                Walker = walker,
                Walks = walks
            };

            if (walker == null)
            {
                return NotFound();
            }

            return View(wm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(id);
            }
        }
    }
}
