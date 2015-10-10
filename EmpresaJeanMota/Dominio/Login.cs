using System;

namespace Dominio
{
    public class Login
    {
        public int IdLogin { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public Status Status { get; set; }

        public Funcionario Funcionario { get; set; }
    }
}
