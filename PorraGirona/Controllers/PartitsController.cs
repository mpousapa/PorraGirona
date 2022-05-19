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
        //private readonly PostDbContext _context;

        //public PartitsController(PostDbContext context)
        //{
        //    _context = context;
        //}
        PostDbContext _context;
        public PartitsController()
        {
            _context = new PostDbContext();
        }
        //Nivell Accés
        private int NivellAcces()
        {
            int nivell = 10;

            String rol;
            byte[] valor = null;
            bool existeix = HttpContext.Session.TryGetValue("rol", out valor);
            if (valor != null)
            {
                rol = System.Text.Encoding.UTF8.GetString(valor);
                if (rol == "admin")
                    nivell = 0;
                if (rol == "soci")
                    nivell = 5;
            }
            return nivell;
        }

        // GET: Partits
        public async Task<IActionResult> Index()
        {
            if (NivellAcces() != 0) return RedirectToAction("Index","Login");
            else
            {
                //var postDbContext = _context.Partits.Include(p => p.IdequiplocalNavigation).Include(p => p.IdequipvisitantNavigation);
                //return View(await postDbContext.ToListAsync());
                return View(await _context.Partits.OrderBy(p => p.Jornada).ToListAsync());
            }

        }

        // GET: Partits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (NivellAcces() != 0) return RedirectToAction("Index", "Login");
            else
            {
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
        }

        // GET: Partits/Create
        public IActionResult Create()
        {
            if (NivellAcces() != 0) return RedirectToAction("Index", "Login");
            else
            {
                ViewData["Idequiplocal"] = new SelectList(_context.Equips, "Idequip", "Idequip");
                ViewData["Idequipvisitant"] = new SelectList(_context.Equips, "Idequip", "Idequip");
                return View();
            }
        }

        // POST: Partits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpartit,Idequiplocal,Idequipvisitant,Datainici,Jornada,Golslocal,Golsvisitant,Finalitzat,Temporada,Idsjugadorslocal,Idsjugadorsvisitant")] Partit partit)
        {
            if (NivellAcces() != 0) return RedirectToAction("Index", "Login");
            else
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
        }

        // GET: Partits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (NivellAcces() != 0) return RedirectToAction("Index", "Login");
            else
            {
                var partit = await _context.Partits.FindAsync(id);
                if (partit == null)
                {
                    return NotFound();
                }
                ViewData["Idequiplocal"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequiplocal);
                ViewData["Idequipvisitant"] = new SelectList(_context.Equips, "Idequip", "Idequip", partit.Idequipvisitant);
                return View(partit);
            }
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
            if (NivellAcces() != 0) return RedirectToAction("Index", "Login");
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(partit);
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
        }

        // GET: Partits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (NivellAcces() != 0) return RedirectToAction("Index", "Login");
            else
            {
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
        }

        // POST: Partits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (NivellAcces() != 0) return RedirectToAction("Index", "Login");
            else
            {
                var partit = await _context.Partits.FindAsync(id);
                _context.Partits.Remove(partit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool PartitExists(int id)
        {
            return _context.Partits.Any(e => e.Idpartit == id);
        }
    }
}
