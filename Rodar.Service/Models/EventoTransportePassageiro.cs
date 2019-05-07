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
        public int idUsuarioPassageiro { get; set; }

        public static EventoTransportePassageiro EntityToModel(Domain.Entity.EventoTransportePassageiro eventoTransportePassageiro)
        {
            return new EventoTransportePassageiro()
            {
                idEventoTransporte = eventoTransportePassageiro.idEventoTransporte,
                idEventoTransportePassageiro = eventoTransportePassageiro.idEventoTransportePassageiro,
                idUsuarioPassageiro = eventoTransportePassageiro.idUsuarioPassageiro
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