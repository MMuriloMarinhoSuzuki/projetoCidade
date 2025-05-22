using Microsoft.AspNetCore.Mvc;
using ProjetoEcommerce.Repositorio;

namespace ProjetoEcommerce.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(string email, string senha)
        {


            var usuario = _usuarioRepositorio.ObterUsuario(email);
            if (usuario != null && usuario.Senha == senha)
            {

                return RedirectToAction("CadastrarCliente", "Cliente");
            }


            ModelState.AddModelError("", "Email ou senha inválidos.");
            //retorna view Login 
            return View();
        }
    }
}
