using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Entity
{
    public class EventoTransportePassageiro
    {
        public int idEventoTransportePassageiro { get; set; }
        public int idEventoTransporte { get; set; }
        public int idUsuarioPassageiro { get; set; }
    }
}
