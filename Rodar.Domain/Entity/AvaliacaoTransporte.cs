using System;

namespace Rodar.Domain.Entity
{
    public class AvaliacaoTransporte
    {
        public int idAvaliacaoTransporte { get; set; }
        public int idUsuarioAvaliador { get; set; }
        public int idUsuarioAvaliado { get; set; }
        public int idEventoTransporte { get; set; }
        public float Avaliacao { get; set; }
        public DateTime? dataHoraAvaliacao { get; set; }
        public string Mensagem { get; set; }
    }
}