using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class DetallesCln
    {
        public static int insertar(DetalleVenta detalle)
        {
            using (var context = new LabFerreteriaEntities())
            {
                context.DetalleVenta.Add(detalle);
                context.SaveChanges();
                return detalle.IdDetalle;
            }
        }
        public static int actualizar(DetalleVenta detalle)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.DetalleVenta.Find(detalle.IdDetalle);
                existe.IdCliente = detalle.IdCliente;
                existe.IdProducto = detalle.IdProducto;
                existe.Cantidad = detalle.Cantidad;
                existe.Subtotal = detalle.Subtotal;
                existe.usuarioRegistro = detalle.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.DetalleVenta.Find(id);
                existe.estado = -1;
                existe.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static DetalleVenta obtenerUno(int id)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.DetalleVenta.Find(id);
            }
        }
        public static List<DetalleVenta> listar()
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.DetalleVenta.Where(x => x.estado != -1).ToList();
            }
        }
        public static List<paDetalleVentaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.paDetalleVentaListar(parametro).ToList();
            }
        }
    }

}
