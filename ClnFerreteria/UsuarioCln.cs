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
            using (var context = new LabFerreteriaEntities())
            {
                return context.Usuario
                    .Where(u => u.Nombre == usuario && u.Clave == clave)
                    .FirstOrDefault();
            }
        }
    }
}
