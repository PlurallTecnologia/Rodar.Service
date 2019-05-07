using Rodar.Domain.Entity;
using Rodar.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Business
{
    public class bllEvento
    {
        IEventoRepository _eventoRepository;

        public bllEvento(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public int Cadastrar(Evento evento)
        {
            try
            {
                return _eventoRepository.Cadastrar(evento);
            }
            catch
            {
                throw;
            }
        }

        public void Atualizar(Evento evento)
        {
            try
            {
                _eventoRepository.Atualizar(evento);
            }
            catch
            {
                throw;
            }
        }

        public Evento Buscar(int idEvento)
        {
            try
            {
                return _eventoRepository.Buscar(idEvento);
            }
            catch
            {
                throw;
            }
        }

        public int Excluir(int idEvento)
        {
            try
            {
                return _eventoRepository.Excluir(idEvento);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Evento> BuscarPorUsuario(int idUsuario = 0)
        {
            try
            {
                return _eventoRepository.BuscarPorUsuario(idUsuario);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Evento> BuscarTodos()
        {
            try
            {
                return _eventoRepository.BuscarTodos();
            }
            catch
            {
                throw;
            }
        }
    }
}
