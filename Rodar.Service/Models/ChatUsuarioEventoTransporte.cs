using Rodar.Business;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class ChatUsuarioEventoTransporte
    {
        public int idChatUsuarioEventoTransporte { get; set; }

        public int idEventoTransporte { get; set; }
        public EventoTransporte eventoTransporte { get; set; }

        public DateTime? dataHoraInclusaoMensagem { get; set; }

        public int idUsuarioOrigem { get; set; }
        public Usuario usuarioOrigem { get; set; }

        public int idUsuarioDestino { get; set; }
        public Usuario usuarioDestino { get; set; }

        public string Mensagem { get; set; }

        public static ChatUsuarioEventoTransporte EntityToModel(Domain.Entity.ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            if (chatUsuarioEventoTransporte == null)
                return null;

            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());
            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

            return new ChatUsuarioEventoTransporte()
            {
                dataHoraInclusaoMensagem = chatUsuarioEventoTransporte.dataHoraInclusaoMensagem,
                idChatUsuarioEventoTransporte = chatUsuarioEventoTransporte.idChatUsuarioEventoTransporte,

                idUsuarioDestino = chatUsuarioEventoTransporte.idUsuarioDestino,
                usuarioDestino = Usuario.EntityToModel(appUsuario.BuscarPorId(chatUsuarioEventoTransporte.idUsuarioDestino)),

                idUsuarioOrigem = chatUsuarioEventoTransporte.idUsuarioOrigem,
                usuarioOrigem = Usuario.EntityToModel(appUsuario.BuscarPorId(chatUsuarioEventoTransporte.idUsuarioOrigem)),

                idEventoTransporte = chatUsuarioEventoTransporte.idEventoTransporte,
                eventoTransporte = EventoTransporte.EntityToModel(appEventoTransporte.Buscar(chatUsuarioEventoTransporte.idEventoTransporte)),

                Mensagem = chatUsuarioEventoTransporte.Mensagem
            };
        }

        public static Domain.Entity.ChatUsuarioEventoTransporte ModelToEntity(ChatUsuarioEventoTransporte chatUsuarioEventoTransporte)
        {
            return new Domain.Entity.ChatUsuarioEventoTransporte()
            {
                dataHoraInclusaoMensagem = chatUsuarioEventoTransporte.dataHoraInclusaoMensagem,
                idChatUsuarioEventoTransporte = chatUsuarioEventoTransporte.idChatUsuarioEventoTransporte,
                idUsuarioDestino = chatUsuarioEventoTransporte.idUsuarioDestino,
                idUsuarioOrigem = chatUsuarioEventoTransporte.idUsuarioOrigem,
                idEventoTransporte = chatUsuarioEventoTransporte.idEventoTransporte,
                Mensagem = chatUsuarioEventoTransporte.Mensagem
            };
        }
    }
}