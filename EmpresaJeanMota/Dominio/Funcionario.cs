using System;

namespace Dominio
{
    public class Funcionario
    {
        public int IdFuncionario { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string CPF { get; set; }

        public string Cargo { get; set; }

        public Decimal Salario { get; set; }

        public int IdCidade { get; set; }

        public Cidade Cidade { get; set; }

        public int IdEstado { get; set; }

        public Estado Estado { get; set; }

        public Status Status { get; set; }
    }
}
