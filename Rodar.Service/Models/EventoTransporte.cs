using Rodar.Business;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class EventoTransporte
    {
        public int idEventoTransporte { get; set; }

        public int idEvento { get; set; }
        public Evento Evento { get; set; }

        public string nomeTransporte { get; set; }

        public int idUsuarioTransportador { get; set; }
        public Usuario usuarioTransportador { get; set; }
        
        public int quantidadeVagas { get; set; }
        public string enderecoPartidaRua { get; set; }
        public string enderecoPartidaComplemento { get; set; }
        public string enderecoPartidaBairro { get; set; }
        public int enderecoPartidaNumero { get; set; }
        public string enderecoPartidaCEP { get; set; }
        public string enderecoPartidaCidade { get; set; }
        public string enderecoPartidaUF { get; set; }

        public decimal valorParticipacao { get; set; }
        public string Mensagem { get; set; }

        public DateTime? dataHoraPartida { get; set; }
        public DateTime? dataHoraPrevisaoChegada { get; set; }

        public int quantidadeVagasDisponiveis { get; set; }
        public int quantidadeVagasOcupadas { get; set; }

        public List<Usuario> Passageiros { get; set; }

        public AvaliacaoTransporte avaliacaoTransporte { get; set; }
            
        public static EventoTransporte EntityToModel(Domain.Entity.EventoTransporte eventoTransporte)
        {
            if (eventoTransporte == null)
                return null;

            var appEventoTransportePassageiro = new bllEventoTransportePassageiro(DBRepository.GetEventoTransportePassageiroRepository());
            var quantidadeVagasOcupadas = (appEventoTransportePassageiro.BuscarTodos()?.Where(ecp => ecp.idEventoTransporte == eventoTransporte.idEventoTransporte).Count());

            var appEvento = new bllEvento(DBRepository.GetEventoRepository());
            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());
            var appUsuarioTransportePassageiros = new bllEventoTransportePassageiro(DBRepository.GetEventoTransportePassageiroRepository());
            var appAvaliacaoTransporte = new bllAvaliacaoTransporte(DBRepository.GetAvaliacaoTransporteRepository());

            return new EventoTransporte()
            {
                enderecoPartidaBairro = eventoTransporte.enderecoPartidaBairro,
                enderecoPartidaCEP = eventoTransporte.enderecoPartidaCEP,
                enderecoPartidaCidade = eventoTransporte.enderecoPartidaCidade,
                enderecoPartidaComplemento = eventoTransporte.enderecoPartidaComplemento,
                enderecoPartidaNumero = eventoTransporte.enderecoPartidaNumero,
                enderecoPartidaRua = eventoTransporte.enderecoPartidaRua,
                enderecoPartidaUF = eventoTransporte.enderecoPartidaUF,

                idEvento = eventoTransporte.idEvento,
                Evento = Evento.EntityToModel(appEvento.Buscar(eventoTransporte.idEvento)),

                idEventoTransporte = eventoTransporte.idEventoTransporte,

                idUsuarioTransportador = eventoTransporte.idUsuarioTransportador,
                usuarioTransportador = Usuario.EntityToModel(appUsuario.BuscarPorId(eventoTransporte.idUsuarioTransportador)),

                Mensagem = eventoTransporte.Mensagem,
                valorParticipacao = eventoTransporte.valorParticipacao,

                dataHoraPartida = eventoTransporte.dataHoraPartida,
                dataHoraPrevisaoChegada = eventoTransporte.dataHoraPrevisaoChegada,

                quantidadeVagas = eventoTransporte.quantidadeVagas,
                quantidadeVagasDisponiveis = eventoTransporte.quantidadeVagas - (quantidadeVagasOcupadas != null ? quantidadeVagasOcupadas.Value : 0),
                quantidadeVagasOcupadas = (quantidadeVagasOcupadas != null ? quantidadeVagasOcupadas.Value : 0),

                Passageiros = appUsuarioTransportePassageiros.BuscarPorTransporte(eventoTransporte.idEventoTransporte)?.Select(ecp => Usuario.EntityToModel(appUsuario.BuscarPorId(ecp.idUsuarioPassageiro))).ToList(),

                avaliacaoTransporte = AvaliacaoTransporte.EntityToModel(appAvaliacaoTransporte.BuscarTodos()?.Where(at => at.idEventoTransporte == eventoTransporte.idEventoTransporte).FirstOrDefault())
            };
        }

        public static Domain.Entity.EventoTransporte ModelToEntity(EventoTransporte eventoTransporte)
        {
            return new Domain.Entity.EventoTransporte()
            {
                enderecoPartidaBairro = eventoTransporte.enderecoPartidaBairro,
                enderecoPartidaCEP = eventoTransporte.enderecoPartidaCEP,
                enderecoPartidaCidade = eventoTransporte.enderecoPartidaCidade,
                enderecoPartidaComplemento = eventoTransporte.enderecoPartidaComplemento,
                enderecoPartidaNumero = eventoTransporte.enderecoPartidaNumero,
                enderecoPartidaRua = eventoTransporte.enderecoPartidaRua,
                enderecoPartidaUF = eventoTransporte.enderecoPartidaUF,
                idEvento = eventoTransporte.idEvento,
                idEventoTransporte = eventoTransporte.idEventoTransporte,
                idUsuarioTransportador = eventoTransporte.idUsuarioTransportador,
                Mensagem = eventoTransporte.Mensagem,
                quantidadeVagas = eventoTransporte.quantidadeVagas,
                valorParticipacao = eventoTransporte.valorParticipacao,
                dataHoraPartida = eventoTransporte.dataHoraPartida,
                dataHoraPrevisaoChegada = eventoTransporte.dataHoraPrevisaoChegada,
            };
        }
    }
}