using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RolesBL
    {
        public static bool InsertarRol(RolesDto rol)
        {
            if (string.IsNullOrWhiteSpace(rol.NombreRol))
                throw new ArgumentException("El nombre del rol es requerido.");

            return RolesDAL.InsertarRol(rol);
        }

        public static List<RolesDto> ListarRoles()
        {
            return RolesDAL.ListarRoles();
        }

        public static bool ActualizarRol(RolesDto rol)
        {
            if (rol.IdRol <= 0)
                throw new ArgumentException("El ID del rol es inválido.");
            if (string.IsNullOrWhiteSpace(rol.NombreRol))
                throw new ArgumentException("El nombre del rol es requerido.");

            return RolesDAL.ActualizarRol(rol);
        }

        public static bool EliminarRol(int idRol)
        {
            if (idRol <= 0)
                throw new ArgumentException("El ID del rol es inválido.");

            return RolesDAL.EliminarRol(idRol);
        }
    }
}