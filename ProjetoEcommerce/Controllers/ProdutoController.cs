using Microsoft.AspNetCore.Mvc;
using ProjetoEcommerce.Models;
using ProjetoEcommerce.Repositorio;



namespace ProjetoEcommerce.Controllers
{
    public class ProdutoController : Controller
    {
        // Variável somente leitura do tipo ProdutoRepositorio (injeção de dependência)
        private readonly ProdutoRepositorio _produtoRepositorio;

        // Construtor recebe o repositório por injeção
        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        // Lista todos os produtos
        public IActionResult Produto()
        {
            return View(_produtoRepositorio.TodosProdutos());
        }

        // Exibe o formulário de cadastro de produto
        public IActionResult CadastrarProduto()
        {
            return View();
        }

        // Processa os dados do formulário de cadastro (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastrarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepositorio.Cadastrar(produto);
                return RedirectToAction(nameof(Produto));
            }

            // Retorna a View com os erros de validação, se houver
            return View(produto);
        }

        // Exibe o formulário de edição de produto
        public IActionResult EditarProduto(int id)
        {
            var produto = _produtoRepositorio.ObterProduto(id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // Processa a edição do produto (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(int id, [Bind("IdPRod,Nome,Descricao,Preco,quantidade")] Produto produto)
        {
            if (id != produto.IdPRod)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRepositorio.Atualizar(produto))
                    {
                        return RedirectToAction(nameof(Produto));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Erro ao editar o produto.");
                    return View(produto);
                }
            }

            return View(produto);
        }

        // Exibe confirmação de exclusão
        [HttpGet]
        public IActionResult ExcluirProduto(int id)
        {
            var produto = _produtoRepositorio.ObterProduto(id);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // Confirma a exclusão do produto
        [HttpPost, ActionName("ExcluirProduto")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            _produtoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Produto));
        }
    }
}

