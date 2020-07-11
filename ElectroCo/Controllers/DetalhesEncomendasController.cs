using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroCo.Data;
using ElectroCo.Models;

namespace ElectroCo.Controllers
{
    public class DetalhesEncomendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalhesEncomendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalhesEncomendas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DetalhesEncomendas.Include(d => d.Order).Include(d => d.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DetalhesEncomendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalhesEncomenda = await _context.DetalhesEncomendas
                .Include(d => d.Order)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detalhesEncomenda == null)
            {
                return NotFound();
            }

            return View(detalhesEncomenda);
        }

        // GET: DetalhesEncomendas/Create
        public IActionResult Create()
        {
            ViewData["EncomendaID"] = new SelectList(_context.Encomendas, "ID", "ID");
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID");
            return View();
        }

        // POST: DetalhesEncomendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Quantidade,PrecoProduto,EncomendaID,ProdutoID")] DetalhesEncomenda detalhesEncomenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalhesEncomenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EncomendaID"] = new SelectList(_context.Encomendas, "ID", "ID", detalhesEncomenda.EncomendaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID", detalhesEncomenda.ProdutoID);
            return View(detalhesEncomenda);
        }

        // GET: DetalhesEncomendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalhesEncomenda = await _context.DetalhesEncomendas.FindAsync(id);
            if (detalhesEncomenda == null)
            {
                return NotFound();
            }
            ViewData["EncomendaID"] = new SelectList(_context.Encomendas, "ID", "ID", detalhesEncomenda.EncomendaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID", detalhesEncomenda.ProdutoID);
            return View(detalhesEncomenda);
        }

        // POST: DetalhesEncomendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantidade,PrecoProduto,EncomendaID,ProdutoID")] DetalhesEncomenda detalhesEncomenda)
        {
            if (id != detalhesEncomenda.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalhesEncomenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalhesEncomendaExists(detalhesEncomenda.ID))
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
            ViewData["EncomendaID"] = new SelectList(_context.Encomendas, "ID", "ID", detalhesEncomenda.EncomendaID);
            ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID", detalhesEncomenda.ProdutoID);
            return View(detalhesEncomenda);
        }

        // GET: DetalhesEncomendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalhesEncomenda = await _context.DetalhesEncomendas
                .Include(d => d.Order)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detalhesEncomenda == null)
            {
                return NotFound();
            }

            return View(detalhesEncomenda);
        }

        // POST: DetalhesEncomendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalhesEncomenda = await _context.DetalhesEncomendas.FindAsync(id);
            _context.DetalhesEncomendas.Remove(detalhesEncomenda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalhesEncomendaExists(int id)
        {
            return _context.DetalhesEncomendas.Any(e => e.ID == id);
        }
    }
}
