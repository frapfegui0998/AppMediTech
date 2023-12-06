using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMediTech.Datos;
using AppMediTech.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppMediTech.Controllers
{
        public class CitaController : Controller
        {
            private readonly ApplicationDbContext _context;

            public CitaController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Cita
            public async Task<IActionResult> Index()
            {
                var citas = await _context.Citas
                    .Include(c => c.Paciente)
                    .Include(c => c.Doctor)
                    .ToListAsync();

                return View(citas);
            }

            // GET: Cita/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var cita = await _context.Citas
                    .Include(c => c.Paciente)
                    .Include(c => c.Doctor)
                    .FirstOrDefaultAsync(m => m.CitaID == id);

                if (cita == null)
                {
                    return NotFound();
                }

                return View(cita);
            }

            // GET: Cita/Create
            public IActionResult Create()
            {
                ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "NombreCompleto");
                ViewData["DoctorID"] = new SelectList(_context.Doctores, "DoctorID", "NombreCompleto");
                return View();
            }

            // POST: Cita/Create
            [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("Fecha,Hora,Motivo,PacienteID,DoctorID,NotasDoctor")] Cita cita)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(cita);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.Pacientes = _context.Pacientes.ToList();
                    ViewBag.Doctores = _context.Doctores.ToList();
                    return View(cita);
                }

            // GET: Cita/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var cita = await _context.Citas.FindAsync(id);
                if (cita == null)
                {
                    return NotFound();
                }
                ViewBag.Pacientes = _context.Pacientes.ToList();
                ViewBag.Doctores = _context.Doctores.ToList();
                return View(cita);
            }

            // POST: Cita/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("CitaID,Fecha,Hora,Motivo,PacienteID,DoctorID,NotasDoctor")] Cita cita)
            {
                if (id != cita.CitaID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cita);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CitaExists(cita.CitaID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "NombreCompleto", cita.PacienteID);
                ViewData["DoctorID"] = new SelectList(_context.Doctores, "DoctorID", "NombreCompleto", cita.DoctorID);
                return View(cita);
            }


            private bool CitaExists(int id)
            {
                return _context.Citas.Any(e => e.CitaID == id);
            }


            // GET: Cita/Delete/5
            public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var cita = await _context.Citas
                        .Include(c => c.Paciente)
                        .Include(c => c.Doctor)
                        .FirstOrDefaultAsync(m => m.CitaID == id);
                    if (cita == null)
                    {
                        return NotFound();
                    }

                    return View(cita);
                }

            // POST: Cita/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var cita = await _context.Citas.FindAsync(id);
                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

    }
}

