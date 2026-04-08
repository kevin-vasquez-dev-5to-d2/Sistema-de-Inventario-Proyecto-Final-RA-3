using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class RolesDto
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public class Roles : RolesDto
        {
        }
    }
}