using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio;
using Repositorio;

namespace Aplicacao
{
    public class FuncionarioAplicacao
    {
        private Contexto contexto;

        private void Inserir(Funcionario funcionario)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " INSERT INTO FUNCIONARIO(Nome, DataNascimento, CPF, Cargo, Salario, IdCidade, IdEstado) ";
                strQuery += string.Format(" VALUES('{0}', '{1}', '{2}','{3}', {4}, {5}, {6}) ", funcionario.Nome, funcionario.DataNascimento,
                    funcionario.CPF, funcionario.Cargo, funcionario.Salario, funcionario.IdCidade, funcionario.IdEstado);
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Funcionario funcionario)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " UPDATE FUNCIONARIO SET ";
                strQuery += string.Format(" Nome = '{0}', ", funcionario.Nome);
                strQuery += string.Format(" DataNascimento = '{0}', ", funcionario.DataNascimento);
                strQuery += string.Format(" CPF = '{0}', ", funcionario.CPF);
                strQuery += string.Format(" Cargo = '{0}', ", funcionario.Cargo);
                strQuery += string.Format(" Salario = {0}, ", funcionario.Salario);
                strQuery += string.Format(" IdCidade = {0}, ", funcionario.IdCidade);
                strQuery += string.Format(" IdEstado = {0} ", funcionario.IdEstado);
                strQuery += string.Format(" WHERE IdFuncionario = {0} ", funcionario.IdFuncionario);
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Funcionario funcionario)
        {
            if (funcionario.IdFuncionario > 0)
            {
                Alterar(funcionario);
            }
            else
            {
                Inserir(funcionario);
            }
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" DELETE FROM FUNCIONARIO WHERE IdFuncionario = {0} ", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Funcionario> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM FUNCIONARIO AS F ";
                strQuery += " LEFT JOIN CIDADE AS C ON(F.IdFuncionario = C.IdCidade) ";
                strQuery += " LEFT JOIN ESTADO AS E ON(F.IdFuncionario = E.IdEstado) ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);
            }
        }

        public Funcionario ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM FUNCIONARIO AS F ";
                strQuery += " LEFT JOIN CIDADE AS C ON(F.IdFuncionario = C.IdCidade) ";
                strQuery += " LEFT JOIN ESTADO AS E ON(F.IdFuncionario = E.IdEstado) ";
                strQuery += string.Format(" WHERE F.IdFuncionario = {0} ", id);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        private List<Funcionario> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var funcionarios = new List<Funcionario>();

            while (reader.Read())
            {
                Funcionario funcionario = new Funcionario();
                funcionario.IdFuncionario = int.Parse(reader["IdFuncionario"].ToString());
                funcionario.Nome = reader["Nome"].ToString();
                funcionario.DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString());
                funcionario.CPF = reader["CPF"].ToString();
                funcionario.Cargo = reader["Cargo"].ToString();
                funcionario.Salario = Decimal.Parse(reader["Salario"].ToString());
                funcionario.IdCidade = int.Parse(reader["IdCidade"].ToString());
                funcionario.IdEstado = int.Parse(reader["IdEstado"].ToString());

                funcionario.Cidade = new Cidade();
                funcionario.Cidade.NomeCidade = reader["NomeCidade"].ToString();

                funcionario.Estado = new Estado();
                funcionario.Estado.NomeEstado = reader["NomeEstado"].ToString();

                funcionarios.Add(funcionario);
            }
            reader.Close();
            return funcionarios;
        }
    }
}
