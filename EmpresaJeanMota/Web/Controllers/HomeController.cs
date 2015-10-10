using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.ViewModel;
using Aplicacao;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private FuncionarioAplicacao aplicacao;

        public HomeController()
        {
            aplicacao = new FuncionarioAplicacao();
        }
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = aplicacao.ListarTodos();

            var model = from funcionario in lista
                        group funcionario by funcionario.DataNascimento
                            into grupo
                            select new FuncionarioEstatistica()
                            {
                                Data = grupo.Key,
                                Contador = grupo.Count()
                            };
            return View(model);
        }

        public ActionResult Contact()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}