using Rodar.Business;
using Rodar.Service.Providers;
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
        public EventoCarona eventoCarona { get; set; }

        public int idUsuarioPassageiro { get; set; }
        public Usuario usuarioPassageiro { get; set; }

        public static EventoCaronaPassageiro EntityToModel(Domain.Entity.EventoCaronaPassageiro eventoCaronaPassageiro)
        {
            if (eventoCaronaPassageiro == null)
                return null;

            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());
            var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());

            return new EventoCaronaPassageiro()
            {
                idEventoCaronaPassageiro = eventoCaronaPassageiro.idEventoCaronaPassageiro,

                idEventoCarona = eventoCaronaPassageiro.idEventoCarona,
                eventoCarona = EventoCarona.EntityToModel(appEventoCarona.Buscar(eventoCaronaPassageiro.idEventoCarona)),

                idUsuarioPassageiro = eventoCaronaPassageiro.idUsuarioPassageiro,
                usuarioPassageiro = Usuario.EntityToModel(appUsuario.BuscarPorId(eventoCaronaPassageiro.idUsuarioPassageiro))
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