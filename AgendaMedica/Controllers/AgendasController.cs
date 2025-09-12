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
    public class AgendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agendas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Agendas.Include(a => a.Medico).Include(a => a.Paciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Agendas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .Include(a => a.Medico)
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.AgendaId == id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // GET: Agendas/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome");
            return View();
        }

        // POST: Agendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgendaId,PacienteId,MedicoId,DataConsulta,Status")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                agenda.AgendaId = Guid.NewGuid();
                _context.Add(agenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", agenda.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", agenda.PacienteId);
            return View(agenda);
        }

        // GET: Agendas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", agenda.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", agenda.PacienteId);
            return View(agenda);
        }

        // POST: Agendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AgendaId,PacienteId,MedicoId,DataConsulta,Status")] Agenda agenda)
        {
            if (id != agenda.AgendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendaExists(agenda.AgendaId))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", agenda.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", agenda.PacienteId);
            return View(agenda);
        }

        // GET: Agendas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .Include(a => a.Medico)
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.AgendaId == id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // POST: Agendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda != null)
            {
                _context.Agendas.Remove(agenda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendaExists(Guid id)
        {
            return _context.Agendas.Any(e => e.AgendaId == id);
        }
    }
}
