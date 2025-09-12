using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaMedica.Data;
using AgendaMedica.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgendaMedica.Controllers
{
    [Authorize]
    public class MedicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Medicos.Include(m => m.Especialidade);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Especialidade)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "Nome");
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId,Nome,CRM,Celular,EspecialidadeId")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                medico.MedicoId = Guid.NewGuid();
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MedicoId,Nome,CRM,Celular,EspecialidadeId")] Medico medico)
        {
            if (id != medico.MedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.MedicoId))
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
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "EspecialidadeId", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Especialidade)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(Guid id)
        {
            return _context.Medicos.Any(e => e.MedicoId == id);
        }
    }
}
