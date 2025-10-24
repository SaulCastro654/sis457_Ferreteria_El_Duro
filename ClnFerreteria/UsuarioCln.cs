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
            using (var context = new FerreteriaEntities())
            { 
                return context.Usuario
                    .Where(u => u.usaurio1 = usuario && u.clave == clave)
                    .FirstOrDefault();
            }
        }
    }
}
