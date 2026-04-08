using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ClienteBL
    {
        public static List<ClientesDto> ListarClientes()
        {
            return ClienteDAL.ListarClientes();
        }

        public static bool InsertarCliente(ClientesDto cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre del cliente es requerido.");
            if (string.IsNullOrWhiteSpace(cliente.TipoCliente))
                throw new ArgumentException("El tipo de cliente es requerido.");

            return ClienteDAL.InsertarCliente(cliente);
        }

        public static bool ActualizarCliente(ClientesDto cliente)
        {
            if (cliente.IdCliente <= 0)
                throw new ArgumentException("El ID del cliente es inválido.");
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre del cliente es requerido.");
            if (string.IsNullOrWhiteSpace(cliente.TipoCliente))
                throw new ArgumentException("El tipo de cliente es requerido.");

            return ClienteDAL.ActualizarCliente(cliente);
        }

        public static bool EliminarCliente(int idCliente)
        {
            if (idCliente <= 0)
                throw new ArgumentException("El ID del cliente es inválido.");

            return ClienteDAL.EliminarCliente(idCliente);
        }
    }
}
