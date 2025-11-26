using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebFerreteria.Models
{
    public partial class Marca
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = null!;

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public short Estado { get; set; }

        public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
    }
}
