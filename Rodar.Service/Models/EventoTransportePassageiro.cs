using Rodar.Business;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class EventoTransportePassageiro
    {
        public int idEventoTransportePassageiro { get; set; }

        public int idEventoTransporte { get; set; }
        public EventoTransporte eventoTransporte { get; set; }

        public int idUsuarioPassageiro { get; set; }
        public Usuario usuarioPassageiro { get; set; }

        public static EventoTransportePassageiro EntityToModel(Domain.Entity.EventoTransportePassageiro eventoTransportePassageiro)
        {
            if (eventoTransportePassageiro == null)
                return null;

            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());
            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());

            return new EventoTransportePassageiro()
            {
                idEventoTransporte = eventoTransportePassageiro.idEventoTransporte,

                idEventoTransportePassageiro = eventoTransportePassageiro.idEventoTransportePassageiro,
                eventoTransporte = EventoTransporte.EntityToModel(appEventoTransporte.Buscar(eventoTransportePassageiro.idEventoTransporte)),

                idUsuarioPassageiro = eventoTransportePassageiro.idUsuarioPassageiro,
                usuarioPassageiro = Usuario.EntityToModel(appUsuario.BuscarPorId(eventoTransportePassageiro.idUsuarioPassageiro))
            };
        }

        public static Domain.Entity.EventoTransportePassageiro ModelToEntity(EventoTransportePassageiro eventoTransportePassageiro)
        {
            return new Domain.Entity.EventoTransportePassageiro()
            {
                idEventoTransporte = eventoTransportePassageiro.idEventoTransporte,
                idEventoTransportePassageiro = eventoTransportePassageiro.idEventoTransportePassageiro,
                idUsuarioPassageiro = eventoTransportePassageiro.idUsuarioPassageiro
            };
        }
    }
}