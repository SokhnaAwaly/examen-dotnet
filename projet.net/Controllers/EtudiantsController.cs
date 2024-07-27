using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projet.net.Models;
using projet.net.request;

namespace projet.net.Controllers
{
    [Authorize]
    public class EtudiantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EtudiantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Etudiants
        public async Task<IActionResult> Index()
        {
            var etudiants = await _context.Etudiants.ToListAsync();
            return View(etudiants);
        }

        // GET: Etudiants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // GET: Etudiants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etudiants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,datenaissance,lieudenaissance,adresse")] CreateOrUpdateEtudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                etudiant.Id = Guid.NewGuid();
                var creer = new Etudiant
                {
                    Id = etudiant.Id,
                    FirstName = etudiant.FirstName,
                    LastName = etudiant.LastName,
                    datenaissance = etudiant.datenaissance,
                    lieudenaissance = etudiant.lieudenaissance,
                    adresse = etudiant.adresse
                };

                _context.Add(creer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etudiant);
        }

        // GET: Etudiants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null)
            {
                return NotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,datenaissance,lieudenaissance,adresse")] CreateOrUpdateEtudiant etudiant)
        {
            if (id != etudiant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var update = new Etudiant

                    {
                        Id = etudiant.Id,


                        FirstName = etudiant.FirstName,
                        LastName = etudiant.LastName,
                        datenaissance = etudiant.datenaissance,
                        lieudenaissance = etudiant.lieudenaissance,
                        adresse = etudiant.adresse,
                    };
                    _context.Update(update);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtudiantExists(etudiant.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(etudiant);
        }

        // GET: Etudiants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant != null)
            {
                _context.Etudiants.Remove(etudiant);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EtudiantExists(Guid id)
        {
            return _context.Etudiants.Any(e => e.Id == id);
        }
    }
}
