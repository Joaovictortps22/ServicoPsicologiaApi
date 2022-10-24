using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using servico_atendimento_psicologia.Data.Connection;
using servico_atendimento_psicologia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servico_atendimento_psicologia.Data
{
    public class UsuarioData
    {
        private Conexao strConnection = new Conexao();
        public List<UsuarioDto> GetUsuarios()
        {
            using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
            {
                const string cmd = "select IdUsuario, Nome, Email,Telefone, Papel from Usuario where Deleted = 0";
                var result = sqlConnection.Query<UsuarioDto>(cmd);
                return result.ToList();
            }
        }
        public UsuarioDto GetUsuario(int idUsuario)
        {
            using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
            {
                string cmd = $"select IdUsuario, Nome, Email, Telefone, Papel from Usuario where IdUsuario = {idUsuario}";
                var result = sqlConnection.QuerySingle<UsuarioDto>(cmd);
                return result;
            }
        }
        public bool Cadastrar(UsuarioDto usuario)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
                {
                    string cmd1 = $"select 1 from usuario where Email = '{usuario.Email}'";
                    string cmd2 = $"insert into usuario (Nome, Email, Telefone, Password, PapelId, Deleted) values ('{usuario.Nome}', '{usuario.Email}', '{usuario.Telefone}', '{usuario.Password}', {usuario.Papel}, 0)";
                    var usu = sqlConnection.Query<UsuarioDto>(cmd1);
                    if(usu.Count() != 0)
                    {
                        throw new Exception("Email já utilizado para cadastrar outro usuário");
                    }
                    else
                    {
                        sqlConnection.ExecuteScalar<UsuarioDto>(cmd2);
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool Deletar(string email)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
                {
                    string cmd1 = $"select 1 from usuario where Email = '{email}'";
                    string cmd2 = $"delete from usuario where Email = '{email}'";
                    var usu = sqlConnection.Query<UsuarioDto>(cmd1);
                    if (usu.Count() == 0)
                    {
                        throw new Exception("Não é possível deletar um usuário que não exista");
                    }
                    else
                    {
                        sqlConnection.ExecuteScalar<UsuarioDto>(cmd2);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool LoginUsuario(UsuarioDto usuario)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
                {
                    string cmd = $"select 1 from usuario where Email = '{usuario.Email}' and Password = '{usuario.Password}'";
                    var usu = sqlConnection.Query<UsuarioDto>(cmd);
                    if (usu.Count() == 0)
                    {
                        throw new Exception("Usuário ou senha incorretos");
                    }
                    else
                    {
                        return true;
                    }
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
