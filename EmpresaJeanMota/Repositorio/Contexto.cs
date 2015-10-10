using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Repositorio
{
    public class Contexto:IDisposable
    {
        private SqlConnection minhaConexao;

        public Contexto()
        {
            minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["EmpresaJeanMota"].ConnectionString);
            minhaConexao.Open();
        }

        public void ExecutaComando(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery, minhaConexao);
            cmd.ExecuteNonQuery();
        }

        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery, minhaConexao);
            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            if (minhaConexao.State == ConnectionState.Open)
            {
                minhaConexao.Close();
            }
        }
    }
}
