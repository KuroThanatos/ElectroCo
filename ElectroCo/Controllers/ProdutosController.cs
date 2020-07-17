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
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProdutosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        public async Task<IActionResult> AdicionarCarrinho(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Cliente = await _context.Clientes
               .FirstOrDefaultAsync(m => m.UserId == _userManager.GetUserId(User));

            if(Cliente == null)
            {
                return NotFound();
            }

            var ProdutoExiste = await _context.Produtos.FirstOrDefaultAsync(m => m.ID == id);

            if (ProdutoExiste == null)
            {
                return NotFound();
            }

            var shopppingCart = new ShoppingCart();
            var shoppingProduto = await _context.ShoppingCart
               .FirstOrDefaultAsync(m => m.ProdutoID.Equals(id) && m.ClientID.Equals(Cliente.ID));

            try
            {
                if(shoppingProduto != null){
                    shoppingProduto.Quantidade = shoppingProduto.Quantidade + 1;
                    _context.Update(shoppingProduto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                shopppingCart.ClientID = Cliente.ID;
                shopppingCart.ProdutoID = (int)id;
                shopppingCart.Quantidade = 1;

                _context.Add(shopppingCart);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: Produtos/Create
        [Authorize(Roles = "administrador")]
        public IActionResult Create()
        {
            ViewBag.product_types = new SelectList (this.TipoProdutos());
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Tipo,Preco,Stock,EstadoProduto,Imagem")] Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtos);
        }

        // GET: Produtos/Edit/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.FindAsync(id);
            if (produtos == null)
            {
                return NotFound();
            }
            ViewBag.product_types = new SelectList(this.TipoProdutos());
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Tipo,Preco,Stock,EstadoProduto,Imagem")] Produtos produtos)
        {
            if (id != produtos.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutosExists(produtos.ID))
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
            return View(produtos);
        }

        // GET: Produtos/Delete/5
        [Authorize(Roles = "administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // POST: Produtos/Delete/5
        [Authorize(Roles = "administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Dictionary<string,string[]> TipoProdutos() {
            var product_types = new Dictionary<string, string[]>();
            product_types.Add("armazenamento", new string[] {
            "Adaptadores de Armazenamento", "Armazenamento Externo", "Armazenamento Interno", "Armazenamento em Cloud", "Caixas Externas e Docking",
            "Cartões de Memórias", "Leitores de Cartões", "Pen Drives",
            });
                    product_types.Add("componentes", new string[] {
                "Caixas de Computador", "Coolers CPU", "Drives Ópticas", "Fonte de Alimentação", "Memórias RAM", "Motherboard", "Placa Gráfica",
                "Placas de Expansão", "Placas de Som", "Processadores", "Ventoinhas",
            });
                    product_types.Add("computadores", new string[] {
                "Acessórios Portáteis","Barebones","Desktops","Mini PCS","Notebooks","Servidores",
            });
                    product_types.Add("imagem e som", new string[] {
                "Câmara","Entretenimento e Streaming","Home Audio", "Monitores", "Projeção de Imagem", "Suportes", "Televisores",
            });
                    product_types.Add("mobilidade", new string[] {
            "Acessórios","EBooks","Mobile Audio","Smartphones","Tablets","Wearables",
            });
                    product_types.Add("periféricos", new string[] {
            "Conversação Web", "Design Gráfico", "Distribuição de Energia", "Gravação e Controlo de Produção", "Impressão e Consumíveis",
            "PC Audio", "Ratos / Teclados", "Simulação e Controladores Gaming",
            });
                    product_types.Add("redes/comunicação", new string[] {
            "Access Points / Repetidores", "Antenas", "Bluetooth", "Placas Rede", "Powerlines", "Routers / Modems", "Switch",
            });

            return product_types;
        }
        private bool ProdutosExists(int id)
        {
            return _context.Produtos.Any(e => e.ID == id);
        }
    }
}
