using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;
using System.Web.Security;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private LoginAplicacao aplicacao;

        public LoginController()
        {
            aplicacao = new LoginAplicacao();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string senha)
        {
            var acesso = aplicacao.Logar(usuario, senha);

            if (acesso != null)
            {
                FormsAuthentication.SetAuthCookie(usuario, false);
                Session["Usuario"] = acesso;
                return RedirectToAction("Index","Funcionario");
            }
            //if (acesso != null && acesso.Funcionario.Cargo.ToUpper().Contains("SUPORTE"))
            //{
            //    FormsAuthentication.SetAuthCookie(usuario, false);
            //    Session["Usuario"] = acesso;
            //    return RedirectToAction("Index", "Cidade");
            //    && acesso.Funcionario.Cargo.ToUpper().Contains("PROGRAMADOR")
            //}
            //else
            //{
                ViewBag.Mensagem = "Usuário e/ou Senha Inválidos!";
                return View();//retorna a view vazia
            //}  
        }
    }
}