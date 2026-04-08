using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CategoriaBL
    {
        public static bool InsertarCategoria(CategoriaDto categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new ArgumentException("El nombre de la categoría es requerido.");

            return CategoriaDAL.InsertarCategoria(categoria);
        }

        public static List<CategoriaDto> ListarCategorias()
        {
            return CategoriaDAL.ListarCategorias();
        }

        public static bool ActualizarCategoria(CategoriaDto categoria)
        {
            if (categoria.IdCategoria <= 0)
                throw new ArgumentException("El ID de la categoría es inválido.");
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new ArgumentException("El nombre de la categoría es requerido.");

            return CategoriaDAL.ActualizarCategoria(categoria);
        }

        public static bool EliminarCategoria(int idCategoria)
        {
            if (idCategoria <= 0)
                throw new ArgumentException("El ID de la categoría es inválido.");

            return CategoriaDAL.EliminarCategoria(idCategoria);
        }
    }
}