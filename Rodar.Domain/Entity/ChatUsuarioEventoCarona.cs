using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Entity
{
    public class ChatUsuarioEventoCarona
    {
        public int idChatUsuarioEventoCarona { get; set; }
        public int idEventoCarona { get; set; }
        public DateTime? dataHoraInclusaoMensagem { get; set; }
        public int idUsuarioOrigem { get; set; }
        public int idUsuarioDestino { get; set; }
        public string Mensagem { get; set; }
    }
}
