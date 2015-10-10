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
    public class LoginAplicacao
    {
        private Contexto contexto;

        public Login Logar(string usuario, string senha)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM LOGIN AS L ";//pega tds os atributos das classes
                //strQuery += " LEFT JOIN STATUS AS S ON(L.IdLogin = S.IdStatus) ";//usa p/ diferente de demitido
                //strQuery += " LEFT JOIN FUNCIONARIO AS F ON(L.IdLogin = F.IdFuncionario) ";//usa p/ pegar cargo
                //strQuery += string.Format(" WHERE UPPER(S.Descricao) <> 'DEMITIDO' AND L.Usuario = '{0}' AND L.Senha = '{1}' ", 
                strQuery += string.Format(" WHERE L.Usuario = '{0}' AND L.Senha = '{1}' ",
                    //strQuery += string.Format(" WHERE UPPER(S.Descricao) <> 'DEMITIDO' AND L.Usuario = '{0}' AND L.Senha = '{1}' ", 
                    usuario, senha);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        public List<Login> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM LOGIN AS L ";
                //strQuery += " LEFT JOIN STATUS AS S ON(L.IdLogin = S.IdStatus) ";
                //strQuery += " LEFT JOIN FUNCIONARIO AS F ON(L.IdLogin = F.IdFuncionario) ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);
            }
        }


        private List<Login> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var logins = new List<Login>();

            while (reader.Read())
            {
                Login login = new Login();//new instânciando
                login.IdLogin = int.Parse(reader["IdLogin"].ToString());
                login.Usuario = reader["Usuario"].ToString();
                login.Senha = reader["Senha"].ToString();

                //login.Funcionario = new Funcionario();//tem q passar os dados na aplicação
                //login.Funcionario.Cargo = reader["Cargo"].ToString();//instânciar p/ poder existir

                //login.Status = new Status();
                //login.Status.Descricao = reader["Descricao"].ToString();

                logins.Add(login);
            }
            reader.Close();
            return logins;
        }
    }
}
