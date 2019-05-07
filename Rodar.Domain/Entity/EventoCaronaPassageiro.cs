using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Entity
{
    public class EventoCaronaPassageiro
    {
        public int idEventoCaronaPassageiro { get; set; }
        public int idEventoCarona { get; set; }
        public int idUsuarioPassageiro { get; set; }
    }
}
