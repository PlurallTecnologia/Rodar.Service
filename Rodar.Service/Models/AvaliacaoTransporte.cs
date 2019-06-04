using Rodar.Business;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class AvaliacaoTransporte
    {
        public int idAvaliacaoTransporte { get; set; }

        public int idUsuarioAvaliador { get; set; }
        public Usuario UsuarioAvaliador { get; set; }

        public int idUsuarioAvaliado { get; set; }
        public Usuario UsuarioAvaliado { get; set; }

        public int idEventoTransporte { get; set; }
        //public EventoTransporte eventoTransporte {get;set;}

        public float Avaliacao { get; set; }

        public DateTime dataHoraAvaliacao { get; set; }

        public string Mensagem { get; set; }

        public static AvaliacaoTransporte EntityToModel(Domain.Entity.AvaliacaoTransporte avaliacaoTransporte)
        {
            if (avaliacaoTransporte == null)
                return null;

            var appEventoTransporte = new bllEventoTransporte(DBRepository.GetEventoTransporteRepository());
            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

            return new AvaliacaoTransporte()
            {
                Avaliacao = avaliacaoTransporte.Avaliacao,
                dataHoraAvaliacao = avaliacaoTransporte.dataHoraAvaliacao.Value,
                
                idAvaliacaoTransporte = avaliacaoTransporte.idAvaliacaoTransporte,

                idEventoTransporte = avaliacaoTransporte.idEventoTransporte,
                //eventoTransporte = EventoTransporte.EntityToModel(appEventoTransporte.Buscar(avaliacaoTransporte.idEventoTransporte)),

                idUsuarioAvaliado = avaliacaoTransporte.idUsuarioAvaliado,
                UsuarioAvaliado = Usuario.EntityToModel(appUsuario.BuscarPorId(avaliacaoTransporte.idUsuarioAvaliado)),
                
                idUsuarioAvaliador = avaliacaoTransporte.idUsuarioAvaliador,
                UsuarioAvaliador = Usuario.EntityToModel(appUsuario.BuscarPorId(avaliacaoTransporte.idUsuarioAvaliador)),

                Mensagem = avaliacaoTransporte.Mensagem
            };
        }

        public static Domain.Entity.AvaliacaoTransporte ModelToEntity(AvaliacaoTransporte avaliacaoTransporte)
        {
            return new Domain.Entity.AvaliacaoTransporte()
            {
                Avaliacao = avaliacaoTransporte.Avaliacao,
                dataHoraAvaliacao = avaliacaoTransporte.dataHoraAvaliacao,
                idAvaliacaoTransporte = avaliacaoTransporte.idAvaliacaoTransporte,
                idEventoTransporte = avaliacaoTransporte.idEventoTransporte,
                idUsuarioAvaliado = avaliacaoTransporte.idUsuarioAvaliado,
                idUsuarioAvaliador = avaliacaoTransporte.idUsuarioAvaliador,
                Mensagem = avaliacaoTransporte.Mensagem
            };
        }
    }
}