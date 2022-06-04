using Domain.DTOs.Pessoa;
using Microsoft.AspNetCore.Mvc;
using WebDesafio.API;
using WebDesafio.Models;

namespace WebTesteCandidatoMotor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar(bool? excluido = null)
        {
            try
            {
                if (excluido != null)
                {
                    if (excluido.Value)
                    {
                        ViewBag.Class = "success";
                        ViewBag.Mensagem = "Dados excluido com sucesso!";
                    }
                    else if (!excluido.Value)
                    {
                        ViewBag.Class = "danger";
                        ViewBag.Mensagem = "Erro na hora de atualizar no banco!";
                    }
                }
                return View(APICore.ApiGetAll());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }
        public IActionResult Dados(int id , bool? atualizado = null)
        {
            try
            {
                if (atualizado != null)
                {
                    if (atualizado.Value)
                    {
                        ViewBag.Class = "success";
                        ViewBag.Mensagem = "Dados criado com sucesso!";
                    }
                    else if (!atualizado.Value)
                    {
                        ViewBag.Class = "danger";
                        ViewBag.Mensagem = "Erro na hora de atualizar no banco!";
                    }
                }

                return View(APICore.ApiGet(id).Data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Pessoa dados)
        {
            try
            {
                APICore.ApiPost(dados);
                ViewBag.Class = "success";
                ViewBag.Mensagem = "Dados atualizado com sucesso!";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Editar(Domain.DTOs.Pessoa.Pessoa dados)
        {
            try
            {
                bool atualiza;
#pragma warning disable CS8629 // O tipo de valor de nulidade pode ser nulo.
                if (APICore.ApiPostEdit(dados.Id.Value, dados))
                    atualiza = true;
                else 
                    atualiza = false;
#pragma warning restore CS8629 // O tipo de valor de nulidade pode ser nulo.

                return RedirectToAction("Dados", "Home", new { id = dados.Id , atualizado = atualiza });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult Excluir(int id)
        {
            try
            {
                bool excluido;
                if (APICore.ApiDelete(id))
                    excluido = true;
                else
                    excluido = false;

                return RedirectToAction("Listar", "Home", new { excluido = excluido });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }
    }
}
