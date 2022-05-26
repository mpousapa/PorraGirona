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
    public class PronPorresController : Controller
    {
        /*
        private readonly PostDbContext _context;

        public PronPorresController(PostDbContext context)
        {
            _context = context;
        }
        */
        PostDbContext _context;
        public PronPorresController()
        {
            _context = new PostDbContext();
        }

        // GET: PronPorres
        public async Task<IActionResult> Index(int id)
        {
            // return View(await _context.PronPorres.ToListAsync());
            // int id = 10;
            string consulta = @"
                SELECT por.idporra as id, par.jornada, par.datainici, loc.nom as local, vis.nom as visitant,
                    por.golslocal as predlocal, por.golsvisitant as predvisitant, par.golslocal, par.golsvisitant,
                    loc.imatge as escutlocal, vis.imatge as escutvisitant
                FROM porres por
                  JOIN partits par ON (por.idpartit = par.idpartit)
                  JOIN equips loc ON (par.idequiplocal = loc.idequip)
                  JOIN equips vis ON (par.idequipvisitant = vis.idequip)
                WHERE idpenyista = " + id;

            List<PronPorres> pronostics = _context.PronPorres.FromSqlRaw(consulta).ToList();
            var pronostics_task = await Task.Run(() => pronostics);

            return View(pronostics_task);
        }

        // GET: PronPorres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pronPorres = await _context.PronPorres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pronPorres == null)
            {
                return NotFound();
            }

            return View(pronPorres);
        }

        // GET: PronPorres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PronPorres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jornada,Datainici,Local,Visitant,Predlocal,Predvisitant,Golslocal,Golsvisitant")] PronPorres pronPorres)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pronPorres);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pronPorres);
        }

        // GET: PronPorres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pronPorres = await _context.PronPorres.FindAsync(id);
            if (pronPorres == null)
            {
                return NotFound();
            }
            return View(pronPorres);
        }

        // POST: PronPorres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Jornada,Datainici,Local,Visitant,Predlocal,Predvisitant,Golslocal,Golsvisitant")] PronPorres pronPorres)
        {
            if (id != pronPorres.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pronPorres);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PronPorresExists(pronPorres.Id))
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
            return View(pronPorres);
        }

        // GET: PronPorres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pronPorres = await _context.PronPorres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pronPorres == null)
            {
                return NotFound();
            }

            return View(pronPorres);
        }

        // POST: PronPorres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pronPorres = await _context.PronPorres.FindAsync(id);
            _context.PronPorres.Remove(pronPorres);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PronPorresExists(int id)
        {
            return _context.PronPorres.Any(e => e.Id == id);
        }
    }
}
