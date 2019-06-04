using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System.Collections.Generic;

namespace Rodar.Business
{
    public class bllChatUsuarioEventoTransporte
    {
        IChatUsuarioEventoTransporteRepository _chatUsuarioEventoTransporteRepository;

        public bllChatUsuarioEventoTransporte(IChatUsuarioEventoTransporteRepository chatUsuarioEventoTransporteRepository)
        {
            _chatUsuarioEventoTransporteRepository = chatUsuarioEventoTransporteRepository;
        }

        public int Cadastrar(ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            try
            {
                return _chatUsuarioEventoTransporteRepository.Cadastrar(chatUsuarioEventoTransporte);
            }
            catch
            {
                throw;
            }
        }

        public List<ChatUsuarioEventoTransporte> BuscarTodos()
        {
            try
            {
                return _chatUsuarioEventoTransporteRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }
    }
}
