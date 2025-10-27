using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class UsuarioCln
    {
        public static Usuario validar(string usuario, string clave)
        {
            using (var context = new LabFerreteriaEntities1())
            { 
                return context.Usuario
                    .Where(x => x.Nombre == usuario && x.Clave == clave && x.estado == 1)
                    .FirstOrDefault();
            }
        }
    }
}
