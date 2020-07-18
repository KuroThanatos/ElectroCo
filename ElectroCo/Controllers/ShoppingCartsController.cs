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
    [Authorize(Roles ="cliente")]
    public class ShoppingCartsController : Controller
    {
        /// <summary>
        /// variável que identifica a BD 
        /// </summary>
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// recolher os dados de uma pessoa que está autenticada
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// Mostra a lista de Itens(Produtos) que um Cliente tem no seu Carrinho de Compras
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> Index()
        {
            var Cliente = await _context.Clientes
              .FirstOrDefaultAsync(m => m.UserId == _userManager.GetUserId(User));

            var applicationDbContext = _context.ShoppingCart.Include(e => e.Cliente).Include(s => s.Product).Where(m => m.ClientID == Cliente.ID);
            return View(await applicationDbContext.ToListAsync());

 
        }

        /// <summary>
        /// Função que :
        ///     -verifica se existem Itens(Produtos)no shoppingCart.
        /// Se houver, redireciona para a Função Create do Controller Encomendas
        /// Se não houver, manda listar os Produtos
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="cliente")]
        public async Task<IActionResult> Encomendar()
        {
            var Cliente = await _context.Clientes
               .FirstOrDefaultAsync(m => m.UserId == _userManager.GetUserId(User));
            var shoppingCart = await _context.ShoppingCart.FirstOrDefaultAsync(m => m.ClientID == Cliente.ID);
            if(shoppingCart != null)
            {
                return RedirectToAction("Create", "Encomendas");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ShoppingCarts/Details/5
        /* public async Task<IActionResult> Details(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var shoppingCart = await _context.ShoppingCart
                 .Include(s => s.Cliente)
                 .Include(s => s.Product)
                 .FirstOrDefaultAsync(m => m.ID == id);
             if (shoppingCart == null)
             {
                 return NotFound();
             }

             return View(shoppingCart);
         }

         // GET: ShoppingCarts/Create
         public IActionResult Create()
         {
             ViewData["ClientID"] = new SelectList(_context.Clientes, "ID", "Name");
             ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID");
             return View();
         }

         // POST: ShoppingCarts/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("ID,ClientID,ProdutoID")] ShoppingCart shoppingCart)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(shoppingCart);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["ClientID"] = new SelectList(_context.Clientes, "ID", "Name", shoppingCart.ClientID);
             ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID", shoppingCart.ProdutoID);
             return View(shoppingCart);
         }

         // GET: ShoppingCarts/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var shoppingCart = await _context.ShoppingCart.FindAsync(id);
             if (shoppingCart == null)
             {
                 return NotFound();
             }
             ViewData["ClientID"] = new SelectList(_context.Clientes, "ID", "Name", shoppingCart.ClientID);
             ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID", shoppingCart.ProdutoID);
             return View(shoppingCart);
         }

         // POST: ShoppingCarts/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("ID,ClientID,ProdutoID")] ShoppingCart shoppingCart)
         {
             if (id != shoppingCart.ID)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(shoppingCart);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!ShoppingCartExists(shoppingCart.ID))
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
             ViewData["ClientID"] = new SelectList(_context.Clientes, "ID", "Name", shoppingCart.ClientID);
             ViewData["ProdutoID"] = new SelectList(_context.Produtos, "ID", "ID", shoppingCart.ProdutoID);
             return View(shoppingCart);
         }
        */

        /// <summary>
        /// Função que verifica se existem itens no Carrinho de Compras, e chama a view de apagar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.Cliente)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        /// <summary>
        /// Função que apaga um Item(Produto) da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            _context.ShoppingCart.Remove(shoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCart.Any(e => e.ID == id);
        }
    }
}
