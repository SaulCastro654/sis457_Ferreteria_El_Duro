using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class MarcaCln
    {
        public static int insertar(Marca marca)
        {
            using (var context = new LabFerreteriaEntities())
            {
                context.Marca.Add(marca);
                context.SaveChanges();
                return marca.IdMarca;
            }
        }

        public static int actualizar(Marca marca)
        {
            using (var context = new LabFerreteriaEntities())
            {
                var existe = context.Marca.Find(marca.IdMarca);
                existe.Nombre = marca.Nombre;

                return context.SaveChanges();
            }
        }
        public static Marca obtenerUno(int id)
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.Marca.Find(id);
            }
        }

        public static List<Marca> listar()
        {
            using (var context = new LabFerreteriaEntities())
            {
                return context.Marca.Where(x => x.estado != -1).ToList();
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
