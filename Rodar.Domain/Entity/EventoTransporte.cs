using System;

namespace Rodar.Domain.Entity
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
        public DateTime? dataHoraPartida { get; set; }
        public DateTime? dataHoraPrevisaoChegada { get; set; }
        public string Mensagem { get; set; }
    }
}