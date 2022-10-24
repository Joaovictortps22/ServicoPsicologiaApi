using servico_atendimento_psicologia.Data;
using servico_atendimento_psicologia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servico_atendimento_psicologia.Manager
{
    public class UsuarioManager
    {
        private readonly UsuarioData _usuarioData = new UsuarioData();
        public List<UsuarioDto> GetUsuarios()
        {
            try
            {
                List<UsuarioDto> lst = _usuarioData.GetUsuarios();

                if (lst.Count() == 0)
                    throw new Exception("Nao encontramos nada");

                return lst;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UsuarioDto GetUsuario(int IdUsuario)
        {
            try
            {
                UsuarioDto usu = _usuarioData.GetUsuario(IdUsuario);

                if (usu == null)
                    throw new Exception("Nao encontramos nenhum usuario");

                return usu;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Cadastrar(UsuarioDto usuario)
        {
            try
            {
                if (!_usuarioData.Cadastrar(usuario))
                    throw new Exception("Usuário não pode ser cadastrado");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Deletar(string email)
        {
            try
            {
                if (!_usuarioData.Deletar(email))
                    throw new Exception("Usuario não pode ser deletado");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public JwtDto LoginUsuario(UsuarioDto usuario)
        {
            try
            {

                if (_usuarioData.LoginUsuario(usuario))
                {
                    ServiceTokenManager _serviceToken = new ServiceTokenManager();
                    return _serviceToken.GerarToken(usuario);
                }
                else
                {
                    throw new Exception("Problemas com autenticação");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
