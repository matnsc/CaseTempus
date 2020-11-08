using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CaseTempus.Models;
using CaseTempus.Repository;

namespace CaseTempus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<Cliente> _logger;
        private readonly ClienteContext _context;
        private readonly ClienteRepository _repository;

        public HomeController(ILogger<Cliente> logger, ClienteContext context)
        {
            _logger = logger;
            _context = context;
            _repository = new ClienteRepository(_context, _logger);
        }

        public IActionResult Index()
        {
            Cliente mv = new Cliente();

            mv.Clientes = _repository.Listar();

            return View(mv);
        }

        public IActionResult Detalhes(Guid id)
        {
            Cliente mv = _repository.Buscar(id);

            return View(mv);
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

            obj.DataCadastro = DateTime.Now;

            if (!_repository.Cadastrar(obj))
            {
                ViewData["Sucesso"] = "N";
                ViewData["Mensagem"] = "Não foi possível cadastrar cliente!";
                return View(obj);
            }

            ViewData["Sucesso"] = "S";

            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
