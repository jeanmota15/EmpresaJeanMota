using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Repositorio;
using System.Data;
using System.Data.SqlClient;

namespace Aplicacao
{
    public class CidadeAplicacao
    {
        private Contexto contexto;

        private void Inserir(Cidade cidade)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" INSERT INTO CIDADE(NomeCidade) VALUES('{0}') ", cidade.NomeCidade);
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Cidade cidade)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " UPDATE CIDADE SET ";
                strQuery += string.Format(" NomeCidade = '{0}' ", cidade.NomeCidade);
                strQuery += string.Format(" WHERE IdCidade = {0} ", cidade.IdCidade);
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Cidade cidade)
        {
            if (cidade.IdCidade > 0)
            {
                Alterar(cidade);
            }
            else
            {
                Inserir(cidade);
            }
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" DELETE FROM CIDADE WHERE IdCidade = {0} ", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Cidade> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM CIDADE ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);
            }
        }

        public Cidade ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" SELECT * FROM CIDADE WHERE IdCidade = {0} ", id);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        private List<Cidade> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var cidades = new List<Cidade>();

            while (reader.Read())
            {
                Cidade cidade = new Cidade();
                cidade.IdCidade = int.Parse(reader["IdCidade"].ToString());
                cidade.NomeCidade = reader["NomeCidade"].ToString();

                cidades.Add(cidade);
            }
            reader.Close();
            return cidades;
        }
    }
}
