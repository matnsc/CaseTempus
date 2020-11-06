using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CaseTempus.Models;

namespace CaseTempus.Controllers
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

        public IActionResult Cadastrar()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult Cadastrar(Cliente obj)
        {
            if (!obj.ValidarCPF())
            {
                ViewData["Sucesso"] = "N";
                ViewData["Mensagem"] = "CPF inválido!";
                return View(obj);
            }

            if (!obj.ValidarDataNascimento())
            {
                ViewData["Sucesso"] = "N";
                ViewData["Mensagem"] = "Data de nascimento deve ser menor do que a data atual!";
                return View(obj);
            }

            if (!obj.ValidarRendaFamiliar())
            {
                ViewData["Sucesso"] = "N";
                ViewData["Mensagem"] = "Renda familiar não pode ser menor que zero!";
                return View(obj);
            }

            ViewData["Sucesso"] = "S";

            // to do - persistência

            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
