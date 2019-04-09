using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string urlImagemSelfie { get; set; }
        public string Genero { get; set; }
        public string Descricao { get; set; }
        public DateTime? dataNascimento { get; set; }
        public string Email { get; set; }
        public string numeroTelefone { get; set; }
        public string Senha { get; set; }
        public string facebookId { get; set; }
        public string googleId { get; set; }
        public string urlImagemRGFrente { get; set; }
        public string urlImagemRGTras { get; set; }
        public string urlImagemCPF { get; set; }
    }
}
