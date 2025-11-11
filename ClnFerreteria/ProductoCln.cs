using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class ProductoCln
    {
        public static int insertar(Producto producto)
        {
            using (var context = new LabFerreteriaEntities())
            {
                context.Producto.Add(producto);
                context.SaveChanges();
                return producto.IdProducto;
            }
        }

        public static int actualizar(Producto producto)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.Producto.Find(producto.IdProducto);
                existe.Nombre = producto.Nombre;
                existe.Stock = producto.Stock;
                existe.Precio = producto.Precio;
                existe.usuarioRegistro = producto.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.Producto.Find(id);
                existe.estado = -1;
                existe.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Producto obtenerUno(int id)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.Producto.Find(id);
            }
        }

        public static List<Producto> listar()
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.Producto.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paProductoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.paProductoListar(parametro).ToList();
            }
        }
    }
}
