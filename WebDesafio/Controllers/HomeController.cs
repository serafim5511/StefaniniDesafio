using Domain.DTOs.Pessoa;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public IActionResult Listar()
        {
            return View(APICore.ApiGetAll());
        }
        public IActionResult DadosMotorista(int id)
        {
            return View(APICore.ApiGet(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Pessoa dados)
        {
            try
            {
                APICore.ApiPost(dados);
                ViewBag.Class = "success";
                ViewBag.Mensagem = "Dados criado com sucesso!";

                return View(dados);
            }
            catch (Exception ex)
            {
                ViewBag.Class = "danger";
                ViewBag.Mensagem = "Erro cadastrar o endereço. Erro :" + ex;
                return View(dados);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EntitiesDesafio.Pessoa dados)
        {
            try
            {
                if (APICore.ApiPostEdit(dados))
                {
                    ViewBag.Class = "success";
                    ViewBag.Mensagem = "Dados criado com sucesso!";
                }
                else {
                    ViewBag.Class = "danger";
                    ViewBag.Mensagem = "Erro na hora de atualizar no banco!";
                }
                return RedirectToAction("Dados", "Home", new { id = dados.Id });
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpGet]
        public async Task<ActionResult> Excluir(int  id)
        {
            try
            {

                if (APICore.ApiDelete(APICore.ApiGet(id)))
                {
                    ViewBag.Class = "success";
                    ViewBag.Mensagem = "Dados criado com sucesso!";
                }
                else
                {
                    ViewBag.Class = "danger";
                    ViewBag.Mensagem = "Erro na hora de atualizar no banco!";
                }
                return RedirectToAction("Lista", "Home");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
