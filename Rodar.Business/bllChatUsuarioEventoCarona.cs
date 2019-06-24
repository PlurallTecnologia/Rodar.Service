using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllChatUsuarioEventoCarona
    {
        IChatUsuarioEventoCaronaRepository _chatUsuarioEventoCaronaRepository;

        public bllChatUsuarioEventoCarona(IChatUsuarioEventoCaronaRepository chatUsuarioEventoCaronaRepository)
        {
            _chatUsuarioEventoCaronaRepository = chatUsuarioEventoCaronaRepository;
        }

        public int Cadastrar(ChatUsuarioEventoCarona chatUsuarioEventoCarona)
        {
            try
            {
                return _chatUsuarioEventoCaronaRepository.Cadastrar(chatUsuarioEventoCarona);
            }
            catch
            {
                throw;
            }
        }

        public List<ChatUsuarioEventoCarona> BuscarTodos()
        {
            try
            {
                return _chatUsuarioEventoCaronaRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }

        public List<ChatUsuarioEventoCarona> BuscarCabecalhoMensagensPorUsuario(int idUsuario)
        {
            try
            {
                return _chatUsuarioEventoCaronaRepository.BuscarCabecalhoMensagensPorUsuario(idUsuario);
            }
            catch
            {
                throw;
            }
        }
    }
}
