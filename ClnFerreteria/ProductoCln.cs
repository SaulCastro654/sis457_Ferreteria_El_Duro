using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public static class ProductoCln
    {
        public static int Insertar(Producto producto)
        {
            using (var context = new FerreteriaEntities())
            {
                context.Producto.Add(producto);
                context.SavestChanges();
                return producto.IdProducto;
            }
        }
        public static int actualizar(Producto producto)
        {
            using (var context = new FerreteriaEntities())
            {
                var existe = context.Producto.Find(producto.IdProducto);
                existe.nombre = producto.nombre;
                existe.precio = producto.precio;
                existe.stock = producto.stock;
                existe.estado = producto.estado;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int IdProducto, string Nombre)
        {
            using (var context = new FerreteriaEntities())
            {
                var existe = context.Producto.Find(id);
                existe.estado = -1;
                return context.SaveChanges();
            }
        }
        public static List<Producto> listar()
        {
            using (var context = new FerreteriaEntities())
            {
                return context.Producto.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paProductoListar_Result> listarPa(string parametro)
        {
            using (var context = new FerreteriaEntities())
            {
                return context.paProductoListar(parametro).ToList();
            }
        }
    }
}
