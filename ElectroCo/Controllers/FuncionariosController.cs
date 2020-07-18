using System;
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

namespace ElectroCo.Controllers
{
    [Authorize(Roles = "administrador,gestorArmazem")]
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FuncionariosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Funcionarios
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.ToListAsync());
        }

        // GET: Funcionarios/Details/GUID
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // GET: Funcionarios/Create
        [Authorize(Roles = "administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Create([Bind("ID,Nome,Email,Telefone,password,NumFuncionario,TipoFuncionario")] Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = funcionarios.Email, Email = funcionarios.Email};
                var result = await _userManager.CreateAsync(user,funcionarios.password);
                if (result.Succeeded)
                {
                    var claim = new System.Security.Claims.Claim("Nome", funcionarios.Nome);
                    await _userManager.AddClaimAsync(user, claim);
                    var result1 = await _userManager.AddToRoleAsync(user, funcionarios.TipoFuncionario);
                    funcionarios.password = null;
                    funcionarios.UserId = user.Id;
                    try
                    {
                        _context.Add(funcionarios);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        await _userManager.RemoveFromRoleAsync(user, funcionarios.TipoFuncionario);
                        await _userManager.RemoveClaimAsync(user, claim);
                        await _userManager.DeleteAsync(user);

                        return View(funcionarios);
                    }
                }
              

            }
            return View(funcionarios);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var funcionarios = new Funcionarios();
            funcionarios = null;

            if (id == null)
            {
                return NotFound();
            }

            if (User.IsInRole("gestorArmazem"))
                funcionarios = await _context.Funcionarios.FirstOrDefaultAsync(f => f.UserId == _userManager.GetUserId(User));
            else
                funcionarios = await _context.Funcionarios.FindAsync(id);

            if (funcionarios == null)
            {
                return NotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Email,Telefone,NumFuncionario,TipoFuncionario")] Funcionarios funcionarios)
        {
            if (id != funcionarios.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionariosExists(funcionarios.ID))
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
            return View(funcionarios);
        }

        //// GET: Funcionarios/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var funcionarios = await _context.Funcionarios
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (funcionarios == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(funcionarios);
        //}

        //// POST: Funcionarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var funcionarios = await _context.Funcionarios.FindAsync(id);
        //    _context.Funcionarios.Remove(funcionarios);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool FuncionariosExists(int id)
        {
            return _context.Funcionarios.Any(e => e.ID == id);
        }
    }
}
