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
                    string cmd1 = $"select idusuario from usuario where Email = '{usuario.Email}'";
                    string cmd2 = $"insert into usuario (Nome, Email, Telefone, Password, Active, Admin) values ('{usuario.Nome}', '{usuario.Email}', '{usuario.Telefone}', '{usuario.Password}', 1, 0)";
                    var usu = sqlConnection.Query<UsuarioDto>(cmd1);
                    
                    if(usu.Count() != 0)
                    {
                        throw new Exception("Email já utilizado para cadastrar outro usuário");
                    }
                    else
                    {
                        sqlConnection.ExecuteScalar<UsuarioDto>(cmd2);
                        var usu2 = sqlConnection.Query<UsuarioDto>(cmd1);
                        var idUsu = usu2.First().IdUsuario;
                        string cmd3 = $"insert into papelusuario (idusuario, papelid) values ({idUsu}, 1)";
                        sqlConnection.ExecuteScalar<UsuarioDto>(cmd3);
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool Atualizar(UsuarioDto usuario)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
                {
                    string cmd1 = $"select idusuario from usuario where Email = '{usuario.Email}'";
                    string cmd2 = $"insert into usuario (Nome, Email, Telefone, Password, Active, Admin) values ('{usuario.Nome}', '{usuario.Email}', '{usuario.Telefone}', '{usuario.Password}', 1, 0)";
                    var usu = sqlConnection.Query<UsuarioDto>(cmd1);

                    if (usu.Count() != 0)
                    {
                        throw new Exception("Email já utilizado para cadastrar outro usuário");
                    }
                    else
                    {
                        sqlConnection.ExecuteScalar<UsuarioDto>(cmd2);
                        var usu2 = sqlConnection.Query<UsuarioDto>(cmd1);
                        var idUsu = usu2.First().IdUsuario;
                        string cmd3 = $"insert into papelusuario (idusuario, papelid) values ({idUsu}, 1)";
                        sqlConnection.ExecuteScalar<UsuarioDto>(cmd3);
                        return true;
                    }
                }
            }
            catch (Exception ex)
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
        public UsuarioDto LoginUsuario(LoginDto usuario)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
                {
                    string cmd = $"select * from usuario where Email = '{usuario.Email}' and Password = '{usuario.Password}'";
                    UsuarioDto usu = sqlConnection.Query<UsuarioDto>(cmd).FirstOrDefault();
                    if (usu == null)
                    {
                        throw new Exception("Usuário ou senha incorretos");
                    }
                    else
                    {
                        return usu;
                    }
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<PapelDto> ListarPapeis(int idUsuario)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(strConnection.StrConnection))
                {
                    string cmd1 = $"select p.PapelId, p.Nome from PapelUsuario pu " +
                        $"inner join Usuario u on u.IdUsuario = pu.IdUsuario " +
                        $"inner join Papel p on p.papelid = pu.papelid where pu.IdUsuario = {idUsuario}";
                    var usu = sqlConnection.Query<PapelDto>(cmd1);
                    if (usu.Count() == 0)
                    {
                        throw new Exception("Usuário não possui papeis");
                    }
                    else
                    {
                        return usu.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
