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
using System.Diagnostics;

namespace ElectroCo.Controllers
{
    public class DetalhesEncomendasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetalhesEncomendasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DetalhesEncomendas
        /*
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DetalhesEncomendas.Include(d => d.Order).Include(d => d.Product);
            return View(await applicationDbContext.ToListAsync());
        }
        */


        /// <summary>
        /// Função que retorna os dados de uma encomenda, ou seja:
        ///         -Os dados guardados na tabela encomenda de uma encomenda (Estado da encomenda, Morada de Entrega, Morada de Faturação, ...)
        ///         -Os produtos guardados na tabela Detalhesencomenda
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = _context.DetalhesEncomendas.Include(o => o.Order).Include(o => o.Product).Where(m => m.EncomendaID == id);


            var detalhesEncomenda = await _context.DetalhesEncomendas
                .Include(d => d.Order)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.EncomendaID == id);

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.ID == detalhesEncomenda.Order.ClientID);
            if (detalhesEncomenda == null || cliente == null)
            {
                return NotFound();
            }
            if (User.IsInRole("administrador, gestorArmazem") ||
               cliente.UserId == _userManager.GetUserId(User)
               )
            {
                return View(await produtos.ToListAsync());
            }
            return RedirectToAction("Index", "Clientes");
        }

        /// <summary>
        /// Função que cria os Detalhes de uma encomenda.
        /// Começa por descobrir o id da encomenda
        /// verifica se  Carrinho esta vazio
        /// Se estiver Remove a encomenda
        /// Se nao estiver itera os produtos que estao dentro do carrinho e adiciona os a BD
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            var Cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.UserId == _userManager.GetUserId(User));
            if (Cliente == null)
            {
                return NotFound();
            }
            var idEnc = _context.Encomendas.Max(m => m.ID);
            var applicationDbContext = _context.ShoppingCart.Include(e => e.Cliente).Include(s => s.Product).Where(m => m.ClientID == Cliente.ID);
            if (applicationDbContext == null)
            {
                var encomenda = _context.Encomendas.FirstOrDefaultAsync(m => m.ID == idEnc);
                _context.Remove(encomenda);

                return RedirectToAction("Index", "Produtos");
            }
            if (VerificaEncomenda(applicationDbContext)) 
            {
                try
                {
                    foreach (var item in applicationDbContext)
                    {
                        var detalhes = new DetalhesEncomenda
                        {
                            EncomendaID = idEnc,
                            Quantidade = item.Quantidade,
                            PrecoProduto = item.Product.Preco,
                            ProdutoID = item.Product.ID
                        };
                        var produtoAtualizado = _context.Produtos.Find(detalhes.ProdutoID);
                        produtoAtualizado = AtualizarProduto(produtoAtualizado, item.Quantidade);
                        _context.Add(detalhes);
                        _context.Remove(item);
                        _context.Update(produtoAtualizado);
                    }

                    await _context.SaveChangesAsync();


                    return RedirectToAction("Index", "Encomendas");
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["error"] = "Não é possível satisfazer a encomenda devido a insuficiência de stock";
            return RedirectToAction("Index", "ShoppingCarts");
           
        }

        private Produtos AtualizarProduto(Produtos produtos, int quantidade)
        {
            produtos.Stock -= quantidade;
            if (produtos.Stock == 0) {
                produtos.EstadoProduto = "Indisponível";
            }
            return produtos;
        }

        private bool VerificaEncomenda(IQueryable<ShoppingCart> applicationDbContext)
        {
            foreach (var item in applicationDbContext) {
                var produto = _context.Produtos.Find(item.ProdutoID);
                if (produto.Stock < item.Quantidade)
                    return false;
            }
            return true;
        }

        /*
        // GET: DetalhesEncomendas/Create
        public IActionResult Create2()
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
        public async Task<IActionResult> Create2([Bind("ID,Quantidade,PrecoProduto,EncomendaID,ProdutoID")] DetalhesEncomenda detalhesEncomenda)
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
        [Authorize(Roles = "administrador")]
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
        [Authorize(Roles = "administrador")]
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
        */

        /// <summary>
        /// Função para apagar um detalhe de uma encomenda 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "administrador")]
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

        /// <summary>
        /// Função para apagar um detalhe de uma encomenda 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "administrador")]
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
