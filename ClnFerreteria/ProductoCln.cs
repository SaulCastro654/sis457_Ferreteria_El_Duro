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
            using (var context = new LabFerreteriaEntities1())
            {
                context.Producto.Add(producto);
                context.SaveChanges();
                return producto.IdProducto;
            }
        }

        public static int actualizar(Producto producto)
        {
            using (var context = new LabFerreteriaEntities1())
            {
                var existe = context.Producto.Find(producto.IdProducto);
                existe.Nombre = producto.Nombre;
                existe.Precio = producto.Precio;
                existe.Stock = producto.Stock;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int IdProducto, string UsuarioRegistro)
        {
            using (var context = new LabFerreteriaEntities1())
            {
                var existe = context.Producto.Find(IdProducto);
                existe.estado = -1;
                existe.usuarioRegistro = UsuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Producto obtenerUno(int IdProducto)
        {
            using (var context = new LabFerreteriaEntities1())
            {
                return context.Producto.Find(IdProducto);
            }
        }

        public static List<Producto> listar()
        {
            using (var context = new LabFerreteriaEntities1())
            {
                return context.Producto.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paProductoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabFerreteriaEntities1())
            { 
                return context.paProductoListar(parametro).ToList();
            }
        }

        public static void eliminar(int id, Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
