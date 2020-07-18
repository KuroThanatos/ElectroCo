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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace ElectroCo.Controllers
{
    public class ProdutosController : Controller
    {
        /// <summary>
        /// variável que identifica a BD 
        /// </summary>
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// recolher os dados de uma pessoa que está autenticada
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;
        /// <summary>
        /// variável que contém os dados do 'ambiente' do servidor. 
        /// Em particular, onde estão os ficheiros guardados, no disco rígido do servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        public ProdutosController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment caminho)
        {
            _context = context;
            _userManager = userManager;
            _caminho = caminho;
        }

        /// <summary>
        /// Lista os Produtos
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.ToListAsync());
        }

       /// <summary>
       /// Mostra os detalhes de um Produto, se este existir.
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
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
        /// <summary>
        /// Função que adiciona um Produto ao Carrinho de Compras.
        ///     -Verifica se o produto existe;
        ///     -verifica se o produto ja esta no carrinho:
        ///         -Se sim, adiciona +1 a quantidade.
        ///         -Se não, adiciona o produto ao Carrinho de compras
        /// No final redireciona para a Lista de Produtos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "cliente")]
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
            //verificar se o produto existe no Carrinho de compras
            var shopppingCart = new ShoppingCart();
            var shoppingProduto = await _context.ShoppingCart
               .FirstOrDefaultAsync(m => m.ProdutoID.Equals(id) && m.ClientID.Equals(Cliente.ID));

            try
            {
                
                //Se ja esta adicionar +1  a quantidade e retirar ao stock
                if (shoppingProduto != null) { 
                    shoppingProduto.Quantidade += 1;
                    if (shoppingProduto.Quantidade <= ProdutoExiste.Stock)
                    {
                        _context.Update(shoppingProduto);
                        await _context.SaveChangesAsync();
                    }
                    else {
                        TempData["error"] = "Não pode adicionar mais do que o Stock existente";
                    }
                    return RedirectToAction("Index","Home");
                }
                //Se nao existe, adiciona o Produto ao carrinho.
                shopppingCart.ClientID = Cliente.ID;
                shopppingCart.ProdutoID = (int)id;
                shopppingCart.Quantidade = 1;

                _context.Add(shopppingCart);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index","Home");
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }

        }

        /// <summary>
        /// Retorna a View create com uma ViewBag com os Tipos de Produtos.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "administrador")]
        public IActionResult Create()
        {
            ViewBag.product_types = new SelectList (this.TipoProdutos());
            return View();
        }

        /// <summary>
        /// Funçao que cria um produto.
        /// Começa por verificar, se a imagem existe e se esta correta.
        /// Se esta tiver bem, e se todas as caracteristicas do produto tiverem corretas, este adiciona o produto a base de dados.
        /// </summary>
        /// <param name="produtos"></param>
        /// <param name="proImg"></param>
        /// <returns></returns>
        [Authorize(Roles = "administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Tipo,Preco,Stock,EstadoProduto,Imagem")] Produtos produtos, IFormFile proImg)
        {
            string caminhoCompleto = "";

            bool img = false;

            //verifica se a imagem existe
            if (proImg == null)
            {
                produtos.Imagem = "no.png";

            }
            else
            {
                //verifica se o tipo da imagem e o correto
                if (proImg.ContentType == "image/jpeg" || proImg.ContentType == "image/png")
                {
                    // cria o um novo id unico para nome da imagem
                    Guid g;
                    g = Guid.NewGuid();

                    string extensao = Path.GetExtension(proImg.FileName).ToLower();
                    string nome = g.ToString() + extensao;

                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens\\produtos", nome);

                    produtos.Imagem = nome;

                    img = true;
                }
                else
                {
                    produtos.Imagem = "no.png";
                }
            }


            if (ModelState.IsValid)
            {
                try {

                    _context.Add(produtos);
                    await _context.SaveChangesAsync();
                    if (img)
                    {
                        //guardar a imagem na Pasta Imagens/Produtos
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await proImg.CopyToAsync(stream);
                    }

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception)
                {
                    return View(produtos);
                }
                
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

        /// <summary>
        /// Funçao para editar um produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produtos"></param>
        /// <returns></returns>
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
        /*
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
        */


    /// <summary>
    /// Função que cria um dicionario, para as categorias de um produto.
    /// </summary>
    /// <returns></returns>
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
