using System;
using System.ComponentModel;
using CpfLibrary;

namespace CaseTempus.Models
{
    public class Cliente 
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime DataCadastro { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [DisplayName("Renda Familiar")]
        public decimal RendaFamiliar { get; set; }

        public bool ValidarCPF()
        {
            return Cpf.Check(CPF);
        }

        public bool ValidarDataNascimento()
        {
            return DataNascimento.Date < DateTime.Now.Date;
        }

        public bool ValidarRendaFamiliar()
        {
            return RendaFamiliar >= 0;
        }
    }
}