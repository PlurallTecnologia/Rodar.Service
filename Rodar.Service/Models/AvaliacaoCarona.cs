using Rodar.Business;
using Rodar.Service.Providers;
using System;

namespace Rodar.Service.Models
{
    public class AvaliacaoCarona
    {
        public int idAvaliacaoCarona { get; set; }

        public int idUsuarioAvaliador { get; set; }
        public Usuario UsuarioAvaliador { get; set; }

        public int idUsuarioAvaliado { get; set; }
        public Usuario UsuarioAvaliado { get; set; }

        public int idEventoCarona { get; set; }
        //public EventoCarona eventoCarona { get; set; }

        public float Avaliacao { get; set; }

        public DateTime dataHoraAvaliacao { get; set; }

        public string Mensagem { get; set; }

        public static AvaliacaoCarona EntityToModel(Domain.Entity.AvaliacaoCarona avaliacaoCarona)
        {
            if (avaliacaoCarona == null)
                return null;

            var appEventoCarona = new bllEventoCarona(DBRepository.GetEventoCaronaRepository());
            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

            return new AvaliacaoCarona()
            {
                Avaliacao = avaliacaoCarona.Avaliacao,
                dataHoraAvaliacao = avaliacaoCarona.dataHoraAvaliacao.Value,

                idAvaliacaoCarona = avaliacaoCarona.idAvaliacaoCarona,

                idEventoCarona = avaliacaoCarona.idEventoCarona,
                //eventoCarona = EventoCarona.EntityToModel(appEventoCarona.Buscar(avaliacaoCarona.idEventoCarona)),

                idUsuarioAvaliado = avaliacaoCarona.idUsuarioAvaliado,
                UsuarioAvaliado = Usuario.EntityToModel(appUsuario.BuscarPorId(avaliacaoCarona.idUsuarioAvaliado)),

                idUsuarioAvaliador = avaliacaoCarona.idUsuarioAvaliador,
                UsuarioAvaliador = Usuario.EntityToModel(appUsuario.BuscarPorId(avaliacaoCarona.idUsuarioAvaliador)),

                Mensagem = avaliacaoCarona.Mensagem
            };
        }

        public static Domain.Entity.AvaliacaoCarona ModelToEntity(AvaliacaoCarona avaliacaoCarona)
        {
            return new Domain.Entity.AvaliacaoCarona()
            {
                Avaliacao = avaliacaoCarona.Avaliacao,
                dataHoraAvaliacao = avaliacaoCarona.dataHoraAvaliacao,
                idAvaliacaoCarona = avaliacaoCarona.idAvaliacaoCarona,
                idEventoCarona = avaliacaoCarona.idEventoCarona,
                idUsuarioAvaliado = avaliacaoCarona.idUsuarioAvaliado,
                idUsuarioAvaliador = avaliacaoCarona.idUsuarioAvaliador,
                Mensagem = avaliacaoCarona.Mensagem
            };
        }
    }
}