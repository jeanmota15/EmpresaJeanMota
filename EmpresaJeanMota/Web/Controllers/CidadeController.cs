using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;

namespace Web.Controllers
{
    public class CidadeController : Controller
    {
        private CidadeAplicacao aplicacao;

        public CidadeController()
        {
            aplicacao = new CidadeAplicacao();
        }
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index","Login");
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
        public ActionResult Cadastrar(Cidade cidade)
        {

            if (ModelState.IsValid)
            {
                aplicacao.Salvar(cidade);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cidade);
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
                return View(lista);//retorna ele msm
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Salvar(cidade);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cidade);
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
                return View(lista);
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