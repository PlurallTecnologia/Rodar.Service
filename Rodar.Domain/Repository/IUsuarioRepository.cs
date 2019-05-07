using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario Usuario);

        void Atualizar(Usuario Usuario);

        Usuario BuscarPorEmail(string Email);

        Usuario BuscarPorId(int idUsuario);
    }
}
