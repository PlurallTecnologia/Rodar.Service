using Rodar.Business;
using Rodar.Service.Globals;
using Rodar.Service.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rodar.Service.Models
{
    public class Evento
    {
        public int idEvento { get; set; }
        public DateTime? dataCriacao { get; set; }

        public int idUsuarioCriacao { get; set; }
        public Usuario usuarioCriacao { get; set; }

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
        public string nomeEvento { get; set; }

        public bool Favorito { get; set; }

        public static Evento EntityToModel(Domain.Entity.Evento evento)
        {
            if (evento == null)
                return null;

            var appEventoFavorito = new bllEventoUsuarioFavorito(DBRepository.GetEventoUsuarioFavoritoRepository());
            var appUsuario = new bllUsuario(DBRepository.GetUsuarioRepository());

            return new Evento()
            {
                idUsuarioCriacao = evento.idUsuarioCriacao,
                usuarioCriacao = Usuario.EntityToModel(appUsuario.BuscarPorId(evento.idUsuarioCriacao)),

                dataCriacao = evento.DataCriacao,
                dataHoraInicio = evento.dataHoraInicio,
                dataHoraTermino = evento.dataHoraTermino,

                descricaoEvento = evento.descricaoEvento,
                nomeEvento = evento.nomeEvento,

                enderecoBairro = evento.enderecoBairro,
                enderecoCEP = evento.enderecoCEP,
                enderecoCidade = evento.enderecoCidade,
                enderecoComplemento = evento.enderecoComplemento,
                enderecoNumero = evento.enderecoNumero,
                enderecoRua = evento.enderecoRua,
                enderecoUF = evento.enderecoUF,

                idEvento = evento.idEvento,
                
                urlImagem1 = evento.urlImagem1,
                urlImagem2 = evento.urlImagem2,
                urlImagem3 = evento.urlImagem3,
                urlImagem4 = evento.urlImagem4,
                urlImagem5 = evento.urlImagem5,
                urlImagemCapa = evento.urlImagemCapa,

                Favorito = appEventoFavorito.ExisteFavorito(evento.idEvento, evento.idUsuarioCriacao)
            };
        }

        public static Domain.Entity.Evento ModelToEntity(Evento evento)
        {
            return new Domain.Entity.Evento()
            {
                DataCriacao = evento.dataCriacao,
                nomeEvento = evento.nomeEvento,
                dataHoraInicio = evento.dataHoraInicio,
                dataHoraTermino = evento.dataHoraTermino,
                descricaoEvento = evento.descricaoEvento,
                enderecoBairro = evento.enderecoBairro,
                enderecoCEP = evento.enderecoCEP,
                enderecoCidade = evento.enderecoCidade,
                enderecoComplemento = evento.enderecoComplemento,
                enderecoNumero = evento.enderecoNumero,
                enderecoRua = evento.enderecoRua,
                enderecoUF = evento.enderecoUF,
                idEvento = evento.idEvento,
                idUsuarioCriacao = evento.idUsuarioCriacao,
                urlImagem1 = evento.urlImagem1,
                urlImagem2 = evento.urlImagem2,
                urlImagem3 = evento.urlImagem3,
                urlImagem4 = evento.urlImagem4,
                urlImagem5 = evento.urlImagem5,
                urlImagemCapa = evento.urlImagemCapa
            };
        }
    }
}