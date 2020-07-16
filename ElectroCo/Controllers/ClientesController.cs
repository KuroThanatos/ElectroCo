using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroCo.Data;
using ElectroCo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ElectroCo.Controllers
{

    [Authorize(Roles = "administrador,cliente")]
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// recolher os dados de uma pessoa que está autenticada
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;


        public ClientesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clientes
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        [Authorize(Roles = "administrador,cliente")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                TempData["error"] = "Não existe informação";
                return LocalRedirect("~/");
                // return RedirectToAction("Index", "Clientes");
            }

            // select *
            // from clientes
            // where UserId = id
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UserId == id);

            if (cliente == null)
            {
                TempData["error"] = "Não existe informação";
                return LocalRedirect("~/");
            }

            // o Cliente foi encontrado
            // será que a pessoa q está a tentar aceder a estes dados tem autorização de acesso?
            // Quem tem acesso?
            //   - pessoas com role 'Administrador'
            //   - o próprio
            if (User.IsInRole("administrador") ||
                cliente.UserId == _userManager.GetUserId(User)
                )
            {
                return View(cliente);
            }

            // se cheguei aqui, é pq não tenho acesso aos dados
            return RedirectToAction("Index", "Home");
        }

        
        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Não existe informação";
                return LocalRedirect("~/");
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                TempData["error"] = "Não existe informação";
                return LocalRedirect("~/");
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Email,Telefone,NIF,Morada,CodigoPostal,UserId")] Clientes cliente)
        {
            if (id != cliente.ID)
            {
                TempData["error"] = "Não existe informação";
                return LocalRedirect("~/");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(cliente.ID))
                    {
                        TempData["error"] = "Não existe informação";
                        return LocalRedirect("~/");
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("administrador"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    return RedirectToAction(nameof(Details), new {id = cliente.UserId });
                }
               
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }


        // POST: Clientes/Delete/5
        [Authorize(Roles = "administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.ID == id);
        }
    }
}
