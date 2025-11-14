using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class ClienteCln
    {
        public static int insertar(Cliente cliente)
        {
            using (var context = new LabFerreteriaEntities())
            {
                context.Cliente.Add(cliente);
                context.SaveChanges();
                return cliente.IdCliente;
            }
        }
        public static int actualizar(Cliente cliente)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.Cliente.Find(cliente.IdCliente);
                existe.Nombre = cliente.Nombre;
                existe.Telefono = cliente.Telefono;
                existe.Direccion = cliente.Direccion;
                existe.Fecha = cliente.Fecha;
                existe.usuarioRegistro = cliente.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.Cliente.Find(id);
                existe.estado = -1;
                existe.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static Cliente obtenerUno(int id)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.Cliente.Find(id);
            }
        }
        public static List<Cliente> listar()
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.Cliente.Where(x => x.estado != -1).ToList();
            }
        }
        public static List<paClienteListar_Result> listarPa(string parametro)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.paClienteListar(parametro).ToList();
            }
        }
    }
}
