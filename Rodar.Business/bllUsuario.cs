using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Business
{
    public class bllUsuario
    {
        IUsuarioRepository _usuarioRepository;

        public bllUsuario(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Cadastrar(Usuario usuario)
        {
            try
            {
                _usuarioRepository.Cadastrar(usuario);
            }
            catch
            {
                throw;
            }
        }

        public void Atualizar(Usuario usuario)
        {
            try
            {
                _usuarioRepository.Atualizar(usuario);
            }
            catch
            {
                throw;
            }
        }

        public Usuario BuscarPorEmail(string Email)
        {
            try
            {
                return _usuarioRepository.BuscarPorEmail(Email);
            }
            catch
            {
                throw;
            }
        }

        public Usuario BuscarPorId(int idUsuario)
        {
            try
            {
                return _usuarioRepository.BuscarPorId(idUsuario);
            }
            catch
            {
                throw;
            }
        }

        public void PromoverParaTransportador(int idUsuario)
        {
            try
            {
                var usuario = _usuarioRepository.BuscarPorId(idUsuario);

                if (usuario != null 
                    && !usuario.Transportador)
                {
                    usuario.Transportador = true;
                }

                _usuarioRepository.Atualizar(usuario);
            }
            catch
            {
                throw;
            }
        }

        public void PromoverParaOrganizadorEvento(int idUsuario)
        {
            try
            {
                var usuario = _usuarioRepository.BuscarPorId(idUsuario);

                if (usuario != null
                    && !usuario.OrganizadorEvento)
                {
                    usuario.OrganizadorEvento = true;
                }

                _usuarioRepository.Atualizar(usuario);
            }
            catch
            {
                throw;
            }
        }

        public void AtualizarTokenNotificacao(int idUsuario, string novoTokenNotificacao)
        {
            try
            {
                var usuario = _usuarioRepository.BuscarPorId(idUsuario);

                if (usuario != null && !string.IsNullOrWhiteSpace(novoTokenNotificacao))
                    usuario.tokenNotificacao = novoTokenNotificacao;

                _usuarioRepository.Atualizar(usuario);
            }
            catch
            {
                throw;
            }
        }
    }
}
