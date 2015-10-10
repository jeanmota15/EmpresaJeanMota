using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;

namespace Web.Controllers
{
    public class FuncionarioController : Controller
    {
        private FuncionarioAplicacao appFuncionario;
        private CidadeAplicacao appCidade;
        private EstadoAplicacao appEstado;

        public FuncionarioController()
        {
            appFuncionario = new FuncionarioAplicacao();
            appCidade = new CidadeAplicacao();
            appEstado = new EstadoAplicacao();
        }
        public ActionResult Index(string ordem, string pesquisa)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = appFuncionario.ListarTodos();

            ViewBag.Nome = string.IsNullOrEmpty(ordem) ? "Nome_Desc" : "Nome";
            ViewBag.Data = ordem == "Date" ? "Data_Desc" : "Date";

            if (!string.IsNullOrEmpty(pesquisa))
            {
                lista = lista.Where(x => x.Nome.ToUpper().Contains(pesquisa.ToUpper()) 
                    || x.Cargo.ToUpper().Contains(pesquisa.ToUpper())).ToList();
            }

            switch (ordem)
            {
                case "Nome_Desc":
                    lista = lista.OrderByDescending(x => x.Nome).ToList();
                    break;
                case "Nome":
                    lista = lista.OrderBy(x => x.Nome).ToList();
                    break;
                case "Data_Desc":
                    lista = lista.OrderByDescending(x => x.DataNascimento).ToList();
                    break;
                case "Date":
                    lista = lista.OrderBy(x=> x.DataNascimento).ToList();
                    break;
                default:
                    lista = lista.OrderByDescending(x => x.DataNascimento).ToList();
                    break;
            }
            return View(lista);
        }

        public ActionResult Index2(string nome)//pesquisa pelo nome, passa esse parâmetro
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = appFuncionario.ListarTodos();

            ViewBag.Pesquisa = (from funcionario in lista
                                    select funcionario.Nome).Distinct();//pesquisa pelo nome

            var model = from funcionario in lista
                        orderby funcionario.Nome
                        where funcionario.Nome == nome
                        select funcionario;

            return View(model);//passa essa pesquisa
        }

        public ActionResult Cadastrar()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Cidade = new SelectList(appCidade.ListarTodos(), "IdCidade", "NomeCidade");
            ViewBag.Estado = new SelectList(appEstado.ListarTodos(), "IdEstado", "NomeEstado");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                appFuncionario.Salvar(funcionario);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Cidade = new SelectList(appCidade.ListarTodos(), "IdCidade", "NomeCidade");
                ViewBag.Estado = new SelectList(appEstado.ListarTodos(), "IdEstado", "NomeEstado");
                return View(funcionario);
            }
        }

        public ActionResult Editar(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = appFuncionario.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Cidade = new SelectList(appCidade.ListarTodos(), "IdCidade", "NomeCidade");
                ViewBag.Estado = new SelectList(appEstado.ListarTodos(), "IdEstado", "NomeEstado");
                return View(lista);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                appFuncionario.Salvar(funcionario);
                return RedirectToAction("Index");
            }
            else
            {
                return View(funcionario);
            }
        }

        public ActionResult Detalhes(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lista = appFuncionario.ListarPorId(id);

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

            var lista = appFuncionario.ListarPorId(id);

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
            appFuncionario.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}