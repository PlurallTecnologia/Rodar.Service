using Rodar.Business;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class EventoCarona
    {
        public int idEventoCarona { get; set; }
        public int idEvento { get; set; }
        public int idUsuarioMotorista { get; set; }

        public string enderecoPartidaRua { get; set; }
        public string enderecoPartidaComplemento { get; set; }
        public string enderecoPartidaBairro { get; set; }
        public int enderecoPartidaNumero { get; set; }
        public string enderecoPartidaCEP { get; set; }
        public string enderecoPartidaCidade { get; set; }
        public string enderecoPartidaUF { get; set; }
        public decimal valorParticipacao { get; set; }
        public string Mensagem { get; set; }

        public int quantidadeVagas { get; set; }

        public int quantidadeVagasDisponiveis { get; set; }
        public int quantidadeVagasOcupadas { get; set; }

        public static EventoCarona EntityToModel(Domain.Entity.EventoCarona eventoCarona)
        {
            var appEventoCaronaPassageiro = new bllEventoCaronaPassageiro(DBRepository.GetEventoCaronaPassageiroRepository());
            var quantidadeVagasOcupadas = (appEventoCaronaPassageiro.BuscarTodos().Where(ecp => ecp.idEventoCarona == eventoCarona.idEventoCarona).Count() - eventoCarona.quantidadeVagas);

            return new EventoCarona()
            {
                enderecoPartidaBairro = eventoCarona.enderecoPartidaBairro,
                enderecoPartidaCEP = eventoCarona.enderecoPartidaCEP,
                enderecoPartidaCidade = eventoCarona.enderecoPartidaCidade,
                enderecoPartidaComplemento = eventoCarona.enderecoPartidaComplemento,
                enderecoPartidaNumero = eventoCarona.enderecoPartidaNumero,
                enderecoPartidaRua = eventoCarona.enderecoPartidaRua,
                enderecoPartidaUF = eventoCarona.enderecoPartidaUF,
                idEvento = eventoCarona.idEvento,
                idEventoCarona = eventoCarona.idEventoCarona,
                idUsuarioMotorista = eventoCarona.idUsuarioMotorista,
                Mensagem = eventoCarona.Mensagem,
                quantidadeVagas = eventoCarona.quantidadeVagas,
                valorParticipacao = eventoCarona.valorParticipacao,
                quantidadeVagasDisponiveis = eventoCarona.quantidadeVagas - quantidadeVagasOcupadas,
                quantidadeVagasOcupadas = quantidadeVagasOcupadas
            };
        }

        public static Domain.Entity.EventoCarona ModelToEntity(EventoCarona eventoCarona)
        {
            return new Domain.Entity.EventoCarona()
            {
                enderecoPartidaBairro = eventoCarona.enderecoPartidaBairro,
                enderecoPartidaCEP = eventoCarona.enderecoPartidaCEP,
                enderecoPartidaCidade = eventoCarona.enderecoPartidaCidade,
                enderecoPartidaComplemento = eventoCarona.enderecoPartidaComplemento,
                enderecoPartidaNumero = eventoCarona.enderecoPartidaNumero,
                enderecoPartidaRua = eventoCarona.enderecoPartidaRua,
                enderecoPartidaUF = eventoCarona.enderecoPartidaUF,
                idEvento = eventoCarona.idEvento,
                idEventoCarona = eventoCarona.idEventoCarona,
                idUsuarioMotorista = eventoCarona.idUsuarioMotorista,
                Mensagem = eventoCarona.Mensagem,
                quantidadeVagas = eventoCarona.quantidadeVagas,
                valorParticipacao = eventoCarona.valorParticipacao
            };
        }
    }
}