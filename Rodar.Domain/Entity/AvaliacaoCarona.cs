using System;

namespace Rodar.Domain.Entity
{
    public class AvaliacaoCarona
    {
        public int idAvaliacaoCarona { get; set; }
        public int idUsuarioAvaliador { get; set; }
        public int idUsuarioAvaliado { get; set; }
        public int idEventoCarona { get; set; }
        public float Avaliacao { get; set; }
        public DateTime? dataHoraAvaliacao { get; set; }
        public string Mensagem { get; set; }
    }
}