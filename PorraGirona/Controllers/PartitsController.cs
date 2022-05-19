using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PorraGirona.Models.Entity;

namespace PorraGirona.Controllers
{
    public class PartitsController : Controller
    {
        private readonly PostDbContext _context;

        public PartitsController(PostDbContext context)
        {
            _context = context;
        }

        // GET: Partits
        public async Task<IActionResult> Index()
        {
            var postDbContext = _context.Partits.Include(p => p.IdequiplocalNavigation).Include(p => p.IdequipvisitantNavigation);
            return View(await postDbContext.ToListAsync());
        }

        // GET: Partits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partit = await _context.Partits
                .Include(p => p.IdequiplocalNavigation)
                .Include(p => p.IdequipvisitantNavigation)
                .FirstOrDefaultAsync(m => m.Idpartit == id);
            if (partit == null)
            {
                return NotFound();
            }

            return View(partit);
        }

        // GET: Partits/Create
        public IActionResult Create()
        {
            ViewData["Idequiplocal"] = new SelectList(_context.Equips, "Idequip", "Idequip");
            ViewData["Idequipvisitant"] = new SelectList(_context.Equips, "Idequip", "Idequip");
            return View();
        }

        // POST: Partits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpartit,Idequiplocal,Idequipvisitant,Datainici,Jornada,Golslocal,Golsvisitant,Finalitzat,Temporada,Idsjugadorslocal,Idsjugadorsvisitant")] Partit partit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idequiplocal"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequiplocal);
            ViewData["Idequipvisitant"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequipvisitant);
            return View(partit);
        }

        // GET: Partits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partit = await _context.Partits.FindAsync(id);
            if (partit == null)
            {
                return NotFound();
            }
            ViewData["Idequiplocal"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequiplocal);
            ViewData["Idequipvisitant"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequipvisitant);
            return View(partit);
        }

        // POST: Partits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idpartit,Idequiplocal,Idequipvisitant,Datainici,Jornada,Golslocal,Golsvisitant,Finalitzat,Temporada,Idsjugadorslocal,Idsjugadorsvisitant")] Partit partit)
        {
            if (id != partit.Idpartit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partit);
                    await _context.SaveChangesAsync();

                    // Calcular puntuacions
                    List<Porre> porres = _context.Porres.FromSqlRaw("SELECT * FROM porres WHERE idpartit = " + partit.Idpartit).ToList();

                    foreach (Porre porra in porres)
                    {
                        Puntuacion puntuacio = (Puntuacion)_context.Puntuacions.FromSqlRaw("SELECT * FROM puntuacions WHERE idpenyista = " + porra.Idpenyista);
                        puntuacio.Puntuacio += CalculaPuntuacioUtilitzantEntitatsAmbAlies(porra, partit);

                        _context.Puntuacions.Update(puntuacio);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartitExists(partit.Idpartit))
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

            ViewData["Idequiplocal"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequiplocal);
            ViewData["Idequipvisitant"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequipvisitant);
            return View(partit);
        }

        // GET: Partits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partit = await _context.Partits
                .Include(p => p.IdequiplocalNavigation)
                .Include(p => p.IdequipvisitantNavigation)
                .FirstOrDefaultAsync(m => m.Idpartit == id);
            if (partit == null)
            {
                return NotFound();
            }

            return View(partit);
        }

        // POST: Partits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partit = await _context.Partits.FindAsync(id);
            _context.Partits.Remove(partit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartitExists(int id)
        {
            return _context.Partits.Any(e => e.Idpartit == id);
        }
        
        public static int CalculaPuntuacioUtilitzantEntitatsAmbAlies(Porre porra, Partit partit)
        {
            int puntuacio, guanyador, prediccioGuanyador;

            bool empat = partit.Golsvisitant == partit.Golslocal;

            // 0 per equip local, 1 per visitant
            guanyador = partit.Golslocal > partit.Golsvisitant ? 0 : 1;
            prediccioGuanyador = porra.Golslocal > porra.Golsvisitant ? 0 : 1;


            if (porra.Golslocal == partit.Golslocal && porra.Golsvisitant == partit.Golsvisitant) puntuacio = 5;

            else if (porra.Golslocal == partit.Golslocal || porra.Golsvisitant == partit.Golsvisitant) puntuacio = 4;

            else if (empat && porra.Golslocal == porra.Golsvisitant) puntuacio = 3;
            
            else if (guanyador == prediccioGuanyador) puntuacio = 3;

            else if (Math.Abs((double)(porra.Golslocal - partit.Golslocal)) <= 1 && Math.Abs((double)(porra.Golsvisitant - partit.Golsvisitant)) <= 1) puntuacio = 2;

            else puntuacio = 1;

            return puntuacio;
        }
    }
}