using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class EventoCaronaPassageiro
    {
        public int idEventoCaronaPassageiro { get; set; }
        public int idEventoCarona { get; set; }
        public int idUsuarioPassageiro { get; set; }

        public static EventoCaronaPassageiro EntityToModel(Domain.Entity.EventoCaronaPassageiro eventoCaronaPassageiro)
        {
            return new EventoCaronaPassageiro()
            {
                idEventoCarona = eventoCaronaPassageiro.idEventoCarona,
                idEventoCaronaPassageiro = eventoCaronaPassageiro.idEventoCaronaPassageiro,
                idUsuarioPassageiro = eventoCaronaPassageiro.idUsuarioPassageiro
            };
        }

        public static Domain.Entity.EventoCaronaPassageiro ModelToEntity(EventoCaronaPassageiro eventoCaronaPassageiro)
        {
            return new Domain.Entity.EventoCaronaPassageiro()
            {
                idEventoCarona = eventoCaronaPassageiro.idEventoCarona,
                idEventoCaronaPassageiro = eventoCaronaPassageiro.idEventoCaronaPassageiro,
                idUsuarioPassageiro = eventoCaronaPassageiro.idUsuarioPassageiro
            };
        }
    }
}