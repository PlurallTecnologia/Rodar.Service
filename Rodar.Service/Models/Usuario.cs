using Rodar.Business;
using Rodar.Service.Providers;
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
        public float Avaliacao { get; set; }

        public static Usuario EntityToModel(Domain.Entity.Usuario usuario)
        {
            var appAvaliacaoCarona = new bllAvaliacaoCarona(DBRepository.GetAvaliacaoCaronaRepository());
            var appAvaliacaoTransporte = new bllAvaliacaoTransporte(DBRepository.GetAvaliacaoTransporteRepository());

            var listaAvaliacoesCarona = appAvaliacaoCarona.BuscarTodos();
            var listaAvaliacoesTransporte = appAvaliacaoTransporte.BuscarTodos();
            
            var somaAvaliacoesCarona = 0;
            var quantidadeAvaliacoesCarona = 0;

            var somaAvaliacoesTransporte =  0;
            var quantidadeAvaliacoesTransporte = 0;

            if (listaAvaliacoesCarona != null)
            {
                somaAvaliacoesCarona = listaAvaliacoesCarona.Where(ac => ac.idUsuarioAvaliado == usuario.idUsuario).Sum(x => x.Avaliacao);
                quantidadeAvaliacoesCarona = listaAvaliacoesCarona.Where(ac => ac.idUsuarioAvaliado == usuario.idUsuario).Count();
            }

            if (listaAvaliacoesTransporte != null)
            {
                somaAvaliacoesTransporte = listaAvaliacoesTransporte.Where(ac => ac.idUsuarioAvaliado == usuario.idUsuario).Sum(x => x.Avaliacao);
                quantidadeAvaliacoesTransporte = listaAvaliacoesTransporte.Where(ac => ac.idUsuarioAvaliado == usuario.idUsuario).Count();
            }

            var totalSomaAvaliacoes = somaAvaliacoesCarona + somaAvaliacoesTransporte;
            var totalQuantidadeAvaliacoes = quantidadeAvaliacoesCarona + quantidadeAvaliacoesTransporte;

            var mediaAvaliacoes = Convert.ToSingle(totalSomaAvaliacoes) / Convert.ToSingle(totalQuantidadeAvaliacoes);

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
                Transportador = usuario.Transportador,
                Avaliacao = mediaAvaliacoes
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