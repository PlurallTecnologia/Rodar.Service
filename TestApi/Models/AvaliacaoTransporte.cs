using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class AvaliacaoTransporte
    {
        public int idAvaliacaoTransporte { get; set; }
        public int idUsuarioAvaliador { get; set; }
        public int idUsuarioAvaliado { get; set; }
        public int idEventoTransporte { get; set; }
        public int Avaliacao { get; set; }  
        public string Mensagem { get; set; }
    }
}
