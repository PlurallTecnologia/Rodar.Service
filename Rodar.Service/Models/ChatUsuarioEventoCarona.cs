using Rodar.Business;
using Rodar.Service.Providers;
using System;


namespace Rodar.Service.Models
{
    public class ChatUsuarioEventoCarona
    {
        public int idChatUsuarioEventoCarona { get; set; }

        public int idEventoCarona { get; set; }
        public EventoCarona eventoCarona { get; set; }

        public DateTime? dataHoraInclusaoMensagem { get; set; }

        public int idUsuarioOrigem { get; set; }
        public Usuario usuarioOrigem { get; set; }
        
        public int idUsuarioDestino { get; set; }
        public Usuario usuarioDestino { get; set; }

        public string Mensagem { get; set; }

        public static ChatUsuarioEventoCarona EntityToModel(Domain.Entity.ChatUsuarioEventoCarona chatUsuarioEventoCarona)
        {
            if (chatUsuarioEventoCarona == null)
                return null;

            var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());
            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

            return new ChatUsuarioEventoCarona()
            {
                dataHoraInclusaoMensagem = chatUsuarioEventoCarona.dataHoraInclusaoMensagem,
                idChatUsuarioEventoCarona = chatUsuarioEventoCarona.idChatUsuarioEventoCarona,

                idUsuarioDestino = chatUsuarioEventoCarona.idUsuarioDestino,
                usuarioDestino = Usuario.EntityToModel(appUsuario.BuscarPorId(chatUsuarioEventoCarona.idUsuarioDestino)),

                idUsuarioOrigem = chatUsuarioEventoCarona.idUsuarioOrigem,
                usuarioOrigem = Usuario.EntityToModel(appUsuario.BuscarPorId(chatUsuarioEventoCarona.idUsuarioOrigem)),

                idEventoCarona = chatUsuarioEventoCarona.idEventoCarona,
                eventoCarona = EventoCarona.EntityToModel(appEventoCarona.Buscar(chatUsuarioEventoCarona.idEventoCarona)),

                Mensagem = chatUsuarioEventoCarona.Mensagem
            };
        }

        public static Domain.Entity.ChatUsuarioEventoCarona ModelToEntity(ChatUsuarioEventoCarona chatUsuarioEventoCarona)
        {
            return new Domain.Entity.ChatUsuarioEventoCarona()
            {
                dataHoraInclusaoMensagem = chatUsuarioEventoCarona.dataHoraInclusaoMensagem,
                idChatUsuarioEventoCarona = chatUsuarioEventoCarona.idChatUsuarioEventoCarona,
                idUsuarioDestino = chatUsuarioEventoCarona.idUsuarioDestino,
                idUsuarioOrigem = chatUsuarioEventoCarona.idUsuarioOrigem,
                idEventoCarona = chatUsuarioEventoCarona.idEventoCarona,
                Mensagem = chatUsuarioEventoCarona.Mensagem
            };
        }
    }
}