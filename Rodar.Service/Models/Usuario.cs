using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
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
        public bool Transportador { get; set; }
        public bool OrganizadorEvento { get; set; }

        public static Usuario EntityToModel(Domain.Entity.Usuario usuario)
        {
            return new Usuario()
            {
                CPF = usuario.CPF,
                dataNascimento = usuario.dataNascimento,
                Descricao = usuario.Descricao,
                Email = usuario.Email,
                facebookId = usuario.facebookId,
                Genero = usuario.Genero,
                googleId = usuario.googleId,
                idUsuario = usuario.idUsuario,
                Nome = usuario.Nome,
                numeroTelefone = usuario.numeroTelefone,
                RG = usuario.RG,
                Senha = usuario.Senha,
                Sobrenome = usuario.Sobrenome,
                urlImagemCPF = usuario.urlImagemCPF,
                urlImagemRGFrente = usuario.urlImagemRGFrente,
                urlImagemRGTras = usuario.urlImagemRGTras,
                urlImagemSelfie = usuario.urlImagemSelfie,
                OrganizadorEvento = usuario.OrganizadorEvento,
                Transportador = usuario.Transportador
            };
        }

        public static Domain.Entity.Usuario ModelToEntity(Usuario usuario)
        {
            return new Domain.Entity.Usuario()
            {
                CPF = usuario.CPF,
                dataNascimento = usuario.dataNascimento,
                Descricao = usuario.Descricao,
                Email = usuario.Email,
                facebookId = usuario.facebookId,
                Genero = usuario.Genero,
                googleId = usuario.googleId,
                idUsuario = usuario.idUsuario,
                Nome = usuario.Nome,
                numeroTelefone = usuario.numeroTelefone,
                RG = usuario.RG,
                Senha = usuario.Senha,
                Sobrenome = usuario.Sobrenome,
                urlImagemCPF = usuario.urlImagemCPF,
                urlImagemRGFrente = usuario.urlImagemRGFrente,
                urlImagemRGTras = usuario.urlImagemRGTras,
                urlImagemSelfie = usuario.urlImagemSelfie,
                OrganizadorEvento = usuario.OrganizadorEvento,
                Transportador = usuario.Transportador
            };
        }
    }
}