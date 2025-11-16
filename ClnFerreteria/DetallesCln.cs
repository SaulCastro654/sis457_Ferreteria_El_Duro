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
                var producto = context.Producto.Find(detalle.IdProducto);
                if (producto == null) throw new Exception("Producto no encontrado");

                detalle.Subtotal = detalle.Cantidad * producto.Precio;
                detalle.estado = 1;
                detalle.fechaRegistro = DateTime.Now;

                context.DetalleVenta.Add(detalle);
                context.SaveChanges();

                var venta = context.Venta.Find(detalle.IdVenta);
                if (venta != null)
                {
                    venta.Total = context.DetalleVenta
                        .Where(d => d.IdVenta == detalle.IdVenta && d.estado != -1)
                        .Sum(d => d.Subtotal);
                    context.SaveChanges();
                }

                return detalle.IdDetalle;
            }
        }

        public static int actualizar(DetalleVenta detalle)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.DetalleVenta.Find(detalle.IdDetalle);
                if (existe == null) throw new Exception("Detalle no encontrado");

                var producto = context.Producto.Find(detalle.IdProducto);
                if (producto == null) throw new Exception("Producto no encontrado");

                existe.IdCliente = detalle.IdCliente;
                existe.IdProducto = detalle.IdProducto;
                existe.Cantidad = detalle.Cantidad;
                existe.Subtotal = detalle.Cantidad * producto.Precio;
                existe.usuarioRegistro = detalle.usuarioRegistro;

                context.SaveChanges();

                var venta = context.Venta.Find(existe.IdVenta);
                if (venta != null)
                {
                    venta.Total = context.DetalleVenta
                        .Where(d => d.IdVenta == existe.IdVenta && d.estado != -1)
                        .Sum(d => d.Subtotal);
                    context.SaveChanges();
                }

                return existe.IdDetalle;
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var detalle = context.DetalleVenta.Find(id);
                if (detalle == null) throw new Exception("Detalle no encontrado");

                detalle.estado = -1;
                detalle.usuarioRegistro = usuarioRegistro;
                context.SaveChanges();

                var venta = context.Venta.Find(detalle.IdVenta);
                if (venta != null)
                {
                    venta.Total = context.DetalleVenta
                        .Where(d => d.IdVenta == detalle.IdVenta && d.estado != -1)
                        .Sum(d => d.Subtotal);
                    context.SaveChanges();
                }

                return detalle.IdDetalle;
            }
        }
        public static DetalleVenta obtenerUno(int idDetalle)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.DetalleVenta
                    .FirstOrDefault(x => x.IdDetalle == idDetalle && x.estado != -1);
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
