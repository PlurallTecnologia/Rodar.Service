using Rodar.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodar.Domain.Repository
{
    public interface IChatUsuarioEventoTransporteRepository
    {
        int Cadastrar(ChatUsuarioEventoTransporte chatUsuarioEventoTransporte);
        List<ChatUsuarioEventoTransporte> BuscarTodos();
        List<ChatUsuarioEventoTransporte> BuscarCabecalhoMensagensPorUsuario(int idUsuario);
    }
}
