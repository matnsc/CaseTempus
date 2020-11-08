using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using CaseTempus.Models;


namespace CaseTempus.Repository
{
    public class ClienteRepository
    {
        private readonly ClienteContext _context;
        private readonly ILogger<Cliente> _logger;

        public ClienteRepository(ClienteContext context, ILogger<Cliente> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Cliente> Listar()
        {
            return _context.Clientes.ToList();
        }

        public List<Cliente> Listar(string nome)
        {
            return _context.Clientes.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();
        }

        public Cliente Buscar(Guid id)
        {
            return _context.Clientes.Single(x => x.Id == id);
        }

        public bool Cadastrar(Cliente obj)
        {
            try 
            {
                _context.Clientes.Add(obj);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return false;
            }
        }

        public bool Editar(Cliente obj)
        {
            try 
            {
                var registro = _context.Clientes.Single(x => x.Id == obj.Id);

                _context.Entry<Cliente>(registro).CurrentValues.SetValues(obj);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return false;
            }
        }

        public bool Excluir(Cliente obj)
        {
            try 
            {
                var cliente = _context.Clientes.Single(x => x.Id == obj.Id);

                _context.Clientes.Remove(cliente);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                return false;
            }
        }
    }
}