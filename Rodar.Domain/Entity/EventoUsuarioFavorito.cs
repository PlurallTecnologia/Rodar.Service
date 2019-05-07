using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Entity
{
    public class EventoUsuarioFavorito
    {
        public int idEvento { get; set; }
        public int idUsuario { get; set; }
        public DateTime? dataHoraFavoritado { get; set; }
    }
}
