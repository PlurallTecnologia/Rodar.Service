using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Entity
{
    public class AvaliacaoCarona
    {
        public int idAvaliacaoCarona { get; set; }
        public int idUsuarioAvaliador { get; set; }
        public int idUsuarioAvaliado { get; set; }
        public int idEventoCarona { get; set; }
        public int Avaliacao { get; set; }
        public DateTime? dataHoraAvaliacao { get; set; }
    }
}