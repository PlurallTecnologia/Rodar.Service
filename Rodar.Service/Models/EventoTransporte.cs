﻿using Rodar.Business;
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
        public string nomeTransporte { get; set; }
        public int idUsuarioTransportador { get; set; }
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

        public int quantidadeVagasDisponiveis { get; set; }
        public int quantidadeVagasOcupadas { get; set; }

        public static EventoTransporte EntityToModel(Domain.Entity.EventoTransporte eventoTransporte)
        {
            var appEventoTransportePassageiro = new bllEventoTransportePassageiro(DBRepository.GetEventoTransportePassageiroRepository());
            var quantidadeVagasOcupadas = (appEventoTransportePassageiro.BuscarTodos().Where(ecp => ecp.idEventoTransporte == eventoTransporte.idEventoTransporte).Count() - eventoTransporte.quantidadeVagas);

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
                idEventoTransporte = eventoTransporte.idEventoTransporte,
                idUsuarioTransportador = eventoTransporte.idUsuarioTransportador,
                Mensagem = eventoTransporte.Mensagem,
                quantidadeVagas = eventoTransporte.quantidadeVagas,
                valorParticipacao = eventoTransporte.valorParticipacao,
                quantidadeVagasDisponiveis = eventoTransporte.quantidadeVagas - quantidadeVagasOcupadas,
                quantidadeVagasOcupadas = quantidadeVagasOcupadas
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
                valorParticipacao = eventoTransporte.valorParticipacao
            };
        }
    }
}