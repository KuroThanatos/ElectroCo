﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroCo.Data;
using ElectroCo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace ElectroCo.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EncomendasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Encomendas
        public async Task<IActionResult> Index()
        {
            var Funcionario = await _context.Funcionarios
               .FirstOrDefaultAsync(m => m.UserId == _userManager.GetUserId(User));

            var Cliente = await _context.Clientes
               .FirstOrDefaultAsync(m => m.UserId == _userManager.GetUserId(User));

            if (User.IsInRole("administrador"))
            {
                var applicationDbContex = _context.Encomendas.Include(e => e.Cliente).Include(e => e.Gestor);
                return View(await applicationDbContex.ToListAsync());
            }
            if (User.IsInRole("cliente"))
            {
                var enc = _context.Encomendas.Include(e => e.Cliente).Where(m => m.ClientID == Cliente.ID);
                return View(await enc.ToListAsync());
            }

            var applicationDbContext = _context.Encomendas.Include(e => e.Cliente).Where(m => m.GestorID == Funcionario.ID);
            return View(await applicationDbContext.ToListAsync());

        }

        public async Task<IActionResult> Concluido(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var encomendas = await _context.Encomendas.FindAsync(id);

            encomendas.EstadoEncomenda = "Concluido";

            _context.Update(encomendas);
            await _context.SaveChangesAsync();

            return View("Index");
        }

        // GET: Encomendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encomendas = await _context.Encomendas
                .Include(e => e.Cliente)
                .Include(e => e.Gestor)
                .FirstOrDefaultAsync(m => m.ID == id);

            var pro =  _context.DetalhesEncomendas.Where(m => m.EncomendaID == id);


            if (encomendas == null)
            {
                return NotFound();
            }
            if (User.IsInRole("administrador") ||
                encomendas.Cliente.UserId == _userManager.GetUserId(User)
                )
            {
                return View(await pro.ToListAsync());
            }
            return RedirectToAction("Index", "Clientes");
        }

        // GET: Encomendas/Create
        public  IActionResult Create()
        {
            var Cliente =  _context.Clientes.FirstOrDefault(m => m.UserId == _userManager.GetUserId(User));
            if (Cliente == null)
            {
                return NotFound();
            }
            var shopping = _context.ShoppingCart.Include(e => e.Cliente).Include(s => s.Product).Where(m => m.ClientID == Cliente.ID);
            if (shopping == null)
            {
                return NotFound();
            }

            ViewData["shopping"] = shopping.ToList();


            ViewData["ClientID"] = new SelectList(_context.Clientes, "ID", "Name");
            ViewData["GestorID"] = new SelectList(_context.Funcionarios, "ID", "Name");
            return View();
        }

        // POST: Encomendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MoradaEncomenda,MoradaFaturacao,")] Encomendas encomendas)
        {
            if (ModelState.IsValid)
            {
                var funcionario = new Funcionarios();
                Random r = new Random();
                do
                {
                    int id=r.Next(0, 100);
                    funcionario = await _context.Funcionarios
                   .FirstOrDefaultAsync(m => m.ID == id);

                } while (funcionario == null);

                encomendas.GestorID = funcionario.ID;
                var Cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.UserId == _userManager.GetUserId(User));

                if(encomendas.MoradaEncomenda == null)
                {
                    encomendas.MoradaEncomenda = Cliente.Morada +" "+ Cliente.CodigoPostal;
                }

                if (encomendas.MoradaFaturacao == null)
                {
                    encomendas.MoradaFaturacao = Cliente.Morada + " " + Cliente.CodigoPostal;
                }

                if (Cliente == null)
                {
                    return NotFound();
                }
                try
                {
                    DateTime date = DateTime.Now;
                    var week = date.DayOfWeek.ToString();
                    encomendas.ClientID = Cliente.ID;
                    encomendas.TrackID = "AdcWERewff12oo98";
                    encomendas.EstadoEncomenda = "Em processamento";
                    encomendas.DataEncomenda = date;
                    if(week == "Thursday" || week == "Friday")
                    {
                        encomendas.PrevisaoEntrega = date.AddDays(4);
                    }
                    else
                    {
                        encomendas.PrevisaoEntrega = date.AddDays(2);
                    }
                    
                    _context.Add(encomendas);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Create", "DetalhesEncomendas");
                }
                catch (Exception)
                {
                    return View(encomendas);
                }

            }
            return View();
        }

        // GET: Encomendas/Edit/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encomendas = await _context.Encomendas.FindAsync(id);
            if (encomendas == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clientes, "ID", "Name", encomendas.ClientID);
            ViewData["GestorID"] = new SelectList(_context.Funcionarios, "ID", "Name", encomendas.GestorID);
            return View(encomendas);
        }

        // POST: Encomendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EstadoEncomenda,DataEncomenda,MoradaEncomenda,MoradaFaturacao,PrevisaoEntrega,TrackID,ClientID,GestorID")] Encomendas encomendas)
        {
            if (id != encomendas.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encomendas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncomendasExists(encomendas.ID))
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
            ViewData["ClientID"] = new SelectList(_context.Clientes, "ID", "Name", encomendas.ClientID);
            ViewData["GestorID"] = new SelectList(_context.Funcionarios, "ID", "Name", encomendas.GestorID);
            return View(encomendas);
        }

        // GET: Encomendas/Delete/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encomendas = await _context.Encomendas
                .Include(e => e.Cliente)
                .Include(e => e.Gestor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (encomendas == null)
            {
                return NotFound();
            }

            return View(encomendas);
        }

        // POST: Encomendas/Delete/5
        [Authorize(Roles = "administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var encomendas = await _context.Encomendas.FindAsync(id);
            _context.Encomendas.Remove(encomendas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncomendasExists(int id)
        {
            return _context.Encomendas.Any(e => e.ID == id);
        }
    }
}
