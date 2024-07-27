using System;
using System.Collections.Generic;
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

    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Notes.Include(n => n.Module);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.Module)
                .FirstOrDefaultAsync(m => m.id == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id");
            ViewData["EtudiantId"] = new SelectList(_context.Modules, "Id", "FirstName");

            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Notedevoir,Noteexamen,EtudiantId,ModuleId,Moyenne")] CreateOrUpdateNotes notes)
        {
            if (ModelState.IsValid)
            {
                notes.id = Guid.NewGuid();
                _context.Add(notes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", notes.ModuleId);
            ViewData["EtudiantId"] = new SelectList(_context.Modules, "Id", "FirstName", notes.ModuleId);

            return View(notes);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", notes.ModuleId);
            ViewData["EtudiantId"] = new SelectList(_context.Modules, "Id", "FirstName", notes.ModuleId);

            return View(notes);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Notedevoir,Noteexamen,EtudiantId,ModuleId,Moyenne")] CreateOrUpdateNotes notes)
        {
            if (id != notes.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesExists(notes.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", notes.ModuleId);
            ViewData["EtudiantId"] = new SelectList(_context.Modules, "Id", "FirstName", notes.ModuleId);

            return View(notes);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.Module)
                .FirstOrDefaultAsync(m => m.id == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notes = await _context.Notes.FindAsync(id);
            if (notes != null)
            {
                _context.Notes.Remove(notes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesExists(Guid id)
        {
            return _context.Notes.Any(e => e.id == id);
        }
    }
}
