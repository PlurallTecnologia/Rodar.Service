using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class Evento
    {
        public int idEvento { get; set; }
        public DateTime? dataCriacao { get; set; }
        public int idUsuarioCriacao { get; set; }
        public string enderecoRua { get; set; }
        public string enderecoComplemento { get; set; }
        public string enderecoBairro { get; set; }
        public int enderecoNumero { get; set; }
        public string enderecoCEP { get; set; }
        public string enderecoCidade { get; set; }
        public string enderecoUF { get; set; }
        public string urlImagemCapa { get; set; }
        public string urlImagem1 { get; set; }
        public string urlImagem2 { get; set; }
        public string urlImagem3 { get; set; }
        public string urlImagem4 { get; set; }
        public string urlImagem5 { get; set; }
        public DateTime? dataHoraInicio { get; set; }
        public DateTime? dataHoraTermino { get; set; }
        public string descricaoEvento { get; set; }
    }
}
