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
    public class EstadoAplicacao
    {
        private Contexto contexto;

        private void Inserir(Estado estado)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" INSERT INTO ESTADO(NomeEstado) VALUES('{0}') ", estado.NomeEstado);
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Estado estado)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " UPDATE ESTADO SET ";
                strQuery += string.Format(" NomeEstado = '{0}' ", estado.NomeEstado);
                strQuery += string.Format(" WHERE IdEstado = {0} ", estado.IdEstado);
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Estado estado)
        {
            if (estado.IdEstado > 0)
            {
                Alterar(estado);
            }
            else
            {
                Inserir(estado);
            }
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" DELETE FROM ESTADO WHERE IdEstado = {0} ", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Estado> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM ESTADO ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);
            }
        }

        public Estado ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" SELECT * FROM ESTADO WHERE IdEstado = {0} ", id);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        private List<Estado> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var estados = new List<Estado>();

            while (reader.Read())
            {
                Estado estado = new Estado();
                estado.IdEstado = int.Parse(reader["IdEstado"].ToString());
                estado.NomeEstado = reader["NomeEstado"].ToString();

                estados.Add(estado);
            }
            reader.Close();
            return estados;
        }
    }
}
