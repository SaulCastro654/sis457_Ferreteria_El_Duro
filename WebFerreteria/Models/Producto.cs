using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public string? CantidadMedida { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public int? IdMarca { get; set; }

    public int? IdCategoria { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }
}
