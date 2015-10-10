using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;

namespace Web.Controllers
{
    public class EstadoController : Controller
    {
        private EstadoAplicacao aplicacao;

        public EstadoController()
        {
            aplicacao = new EstadoAplicacao();
        }
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = aplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Estado estado)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Salvar(estado);
                return RedirectToAction("Index");
            }
            else
            {
                return View(estado);
            }
        }

        public ActionResult Editar(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Estado estado)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Salvar(estado);
                return RedirectToAction("Index");
            }
            else
            {
                return View(estado);
            }
        }

        public ActionResult Detalhes(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);//retorna a lista e mostra os dados dela
            }
        }

        public ActionResult Excluir(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirComando(int id)
        {
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}