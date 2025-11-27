using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFerreteria.Models;

namespace WebFerreteria.Controllers
{
    public class VentaController : Controller
    {
        private readonly LabFerreteriaContext _context;

        public VentaController(LabFerreteriaContext context)
        {
            _context = context;
        }

        // GET: Venta
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .Where(v => v.Estado == 1)
                .OrderByDescending(v => v.FechaRegistro)
                .ToListAsync();

            return View(ventas);
        }

        // GET: Venta/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente.Where(c => c.Estado == 1), "Id", "Nombre");
            ViewData["IdUsuario"] = new SelectList(_context.Usuario.Where(u => u.Estado == 1), "Id", "Nombre");
            return View();
        }

        // POST: Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdCliente,IdUsuario,Total,TipoEntrega")] Venta venta,
            string detallesJson)
        {
            Console.WriteLine("=== INICIANDO CREACIÓN DE VENTA ===");
            Console.WriteLine($"IdCliente: {venta.IdCliente}");
            Console.WriteLine($"IdUsuario: {venta.IdUsuario}");
            Console.WriteLine($"Total: {venta.Total}");
            Console.WriteLine($"TipoEntrega: {venta.TipoEntrega}");
            Console.WriteLine($"detallesJson: {detallesJson}");

            try
            {
                if (venta.IdCliente == 0)
                {
                    ModelState.AddModelError("IdCliente", "Debe seleccionar un cliente");
                }

                if (venta.IdUsuario == 0)
                {
                    ModelState.AddModelError("IdUsuario", "Debe seleccionar un usuario");
                }

                if (venta.Total <= 0)
                {
                    ModelState.AddModelError("Total", "El total debe ser mayor a 0");
                }

                List<DetalleVenta> detalles = new();
                if (!string.IsNullOrEmpty(detallesJson))
                {
                    try
                    {
                        detalles = System.Text.Json.JsonSerializer.Deserialize<List<DetalleVenta>>(detallesJson);
                        Console.WriteLine($"Detalles deserializados: {detalles?.Count ?? 0}");
                    }
                    catch (Exception jsonEx)
                    {
                        Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
                        ModelState.AddModelError("", "Error en el formato de los detalles de la venta");
                    }
                }

                if (detalles == null || !detalles.Any())
                {
                    ModelState.AddModelError("", "Debe agregar al menos un producto a la venta");
                }

                venta.UsuarioRegistro = User.Identity?.Name ?? "Admin";
                venta.FechaRegistro = DateTime.Now;
                venta.Estado = 1;

                ModelState.Remove("UsuarioRegistro");
                ModelState.Remove("FechaRegistro");
                ModelState.Remove("Estado");
                ModelState.Remove("IdClienteNavigation");
                ModelState.Remove("IdUsuarioNavigation");
                ModelState.Remove("DetalleVenta");

                if (ModelState.IsValid && detalles != null && detalles.Any())
                {
                    using var transaction = await _context.Database.BeginTransactionAsync();

                    try
                    {
                        Console.WriteLine("Guardando venta principal...");
                        _context.Add(venta);
                        await _context.SaveChangesAsync();
                        Console.WriteLine($"Venta guardada con ID: {venta.Id}");

                        Console.WriteLine("Guardando detalles...");
                        foreach (var detalle in detalles)
                        {
                            detalle.IdVenta = venta.Id;
                            detalle.Estado = 1;
                            detalle.FechaRegistro = DateTime.Now;
                            detalle.UsuarioRegistro = User.Identity?.Name ?? "Admin";

                            Console.WriteLine($"Procesando detalle - Producto: {detalle.IdProducto}, Cantidad: {detalle.Cantidad}, Precio: {detalle.PrecioUnitario}");

                            var producto = await _context.Producto.FindAsync(detalle.IdProducto);
                            if (producto == null)
                            {
                                throw new Exception($"Producto con ID {detalle.IdProducto} no encontrado");
                            }

                            if (producto.Stock < detalle.Cantidad)
                            {
                                throw new Exception($"Stock insuficiente para {producto.Nombre}. Stock actual: {producto.Stock}, Solicitado: {detalle.Cantidad}");
                            }

                            producto.Stock -= detalle.Cantidad;
                            _context.Update(producto);

                            _context.Add(detalle);
                            Console.WriteLine($"Detalle guardado para producto: {producto.Nombre}");
                        }

                        Console.WriteLine("Guardando todos los cambios...");
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        Console.WriteLine("=== VENTA GUARDADA EXITOSAMENTE ===");

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine($"ERROR durante la transacción: {ex.Message}");
                        Console.WriteLine($"StackTrace: {ex.StackTrace}");
                        ModelState.AddModelError("", "Error al guardar la venta: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Errores de validación:");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($" - {error.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR general: {ex.Message}");
                ModelState.AddModelError("", "Error al procesar la venta: " + ex.Message);
            }

            ViewData["IdCliente"] = new SelectList(_context.Cliente.Where(c => c.Estado == 1), "Id", "Nombre", venta.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario.Where(u => u.Estado == 1), "Id", "Nombre", venta.IdUsuario);

            Console.WriteLine("=== RETORNANDO A LA VISTA CON ERRORES ===");
            return View(venta);
        }

        // GET: Venta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Venta
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null) return NotFound();

            ViewData["IdCliente"] = new SelectList(_context.Cliente.Where(c => c.Estado == 1), "Id", "Nombre", venta.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario.Where(u => u.Estado == 1), "Id", "Nombre", venta.IdUsuario);

            ViewBag.DetallesExistentes = venta.DetalleVenta.Where(d => d.Estado == 1).ToList();

            return View(venta);
        }

        // POST: Venta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,IdCliente,IdUsuario,Total,TipoEntrega")] Venta ventaForm,
            string detallesJson)
        {
            if (id != ventaForm.Id) return NotFound();

            Console.WriteLine("=== EDITANDO VENTA ===");
            Console.WriteLine($"ID: {ventaForm.Id}, Cliente: {ventaForm.IdCliente}, Total: {ventaForm.Total}");
            Console.WriteLine($"Detalles JSON: {detallesJson}");

            var ventaBD = await _context.Venta
                .Include(v => v.DetalleVenta)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (ventaBD == null) return NotFound();

            List<DetalleVenta> nuevosDetalles = new();
            if (!string.IsNullOrEmpty(detallesJson))
            {
                try
                {
                    nuevosDetalles = System.Text.Json.JsonSerializer.Deserialize<List<DetalleVenta>>(detallesJson);
                    Console.WriteLine($"Nuevos detalles deserializados: {nuevosDetalles?.Count ?? 0}");
                }
                catch (Exception jsonEx)
                {
                    Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
                    ModelState.AddModelError("", "Error en el formato de los detalles de la venta");
                }
            }

            ModelState.Remove("UsuarioRegistro");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("Estado");
            ModelState.Remove("IdClienteNavigation");
            ModelState.Remove("IdUsuarioNavigation");
            ModelState.Remove("DetalleVenta");

            if (ModelState.IsValid && nuevosDetalles != null && nuevosDetalles.Any())
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    ventaBD.IdCliente = ventaForm.IdCliente;
                    ventaBD.IdUsuario = ventaForm.IdUsuario;
                    ventaBD.Total = ventaForm.Total;
                    ventaBD.TipoEntrega = ventaForm.TipoEntrega;

                    foreach (var detalleViejo in ventaBD.DetalleVenta.Where(d => d.Estado == 1))
                    {
                        var producto = await _context.Producto.FindAsync(detalleViejo.IdProducto);
                        if (producto != null)
                        {
                            producto.Stock += detalleViejo.Cantidad;
                            _context.Update(producto);
                        }
                        detalleViejo.Estado = 0;
                    }

                    foreach (var nuevoDetalle in nuevosDetalles)
                    {
                        var detalle = new DetalleVenta
                        {
                            IdVenta = ventaBD.Id,
                            IdProducto = nuevoDetalle.IdProducto,
                            Cantidad = nuevoDetalle.Cantidad,
                            PrecioUnitario = nuevoDetalle.PrecioUnitario,
                            Estado = 1,
                            FechaRegistro = DateTime.Now,
                            UsuarioRegistro = User.Identity?.Name ?? "Admin"
                        };

                        var producto = await _context.Producto.FindAsync(nuevoDetalle.IdProducto);
                        if (producto != null && producto.Stock >= nuevoDetalle.Cantidad)
                        {
                            producto.Stock -= nuevoDetalle.Cantidad;
                            _context.Update(producto);
                        }
                        else
                        {
                            throw new Exception($"Stock insuficiente para el producto: {producto?.Nombre}");
                        }

                        _context.Add(detalle);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    Console.WriteLine("=== VENTA ACTUALIZADA EXITOSAMENTE ===");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"ERROR durante la actualización: {ex.Message}");
                    ModelState.AddModelError("", "Error al actualizar la venta: " + ex.Message);
                }
            }
            else if (nuevosDetalles == null || !nuevosDetalles.Any())
            {
                ModelState.AddModelError("", "Debe agregar al menos un producto a la venta");
            }

            ViewData["IdCliente"] = new SelectList(_context.Cliente.Where(c => c.Estado == 1), "Id", "Nombre", ventaForm.IdCliente);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario.Where(u => u.Estado == 1), "Id", "Nombre", ventaForm.IdUsuario);

            var ventaRecargada = await _context.Venta
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(v => v.Id == id);

            ViewBag.DetallesExistentes = ventaRecargada?.DetalleVenta.Where(d => d.Estado == 1).ToList();

            return View(ventaForm);
        }

        // GET: Venta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // GET: Venta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Venta.FindAsync(id);

            if (venta != null)
            {
                venta.Estado = 0;
                _context.Update(venta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // AJAX: Buscar clientes
        [HttpGet]
        public async Task<JsonResult> BuscarClientes(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return Json(new List<object>());

            var clientes = await _context.Cliente
                .Where(c => c.Estado == 1 &&
                       (c.Nombre.Contains(termino) ||
                        c.Telefono != null && c.Telefono.Contains(termino) ||
                        c.Direccion != null && c.Direccion.Contains(termino)))
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    telefono = c.Telefono,
                    direccion = c.Direccion
                })
                .OrderBy(c => c.nombre)
                .Take(10)
                .ToListAsync();

            return Json(clientes);
        }

        // AJAX: Obtener todos los productos disponibles
        [HttpGet]
        public async Task<JsonResult> GetProductosDisponibles()
        {
            var productos = await _context.Producto
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Estado == 1 && p.Stock > 0)
                .Select(p => new
                {
                    id = p.Id,
                    nombre = p.Nombre,
                    precio = p.Precio,
                    stock = p.Stock,
                    marca = p.IdMarcaNavigation.Nombre,
                    categoria = p.IdCategoriaNavigation.Nombre
                })
                .OrderBy(p => p.nombre)
                .ToListAsync();

            return Json(productos);
        }

        // AJAX: Buscar productos
        [HttpGet]
        public async Task<JsonResult> BuscarProductos(string termino, int? categoriaId)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return Json(new List<object>());

            var query = _context.Producto
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Estado == 1 && p.Stock > 0 &&
                       (p.Nombre.Contains(termino) ||
                        p.IdMarcaNavigation.Nombre.Contains(termino)));

            if (categoriaId.HasValue && categoriaId.Value > 0)
            {
                query = query.Where(p => p.IdCategoria == categoriaId.Value);
            }

            var productos = await query
                .Select(p => new
                {
                    id = p.Id,
                    nombre = p.Nombre,
                    precio = p.Precio,
                    stock = p.Stock,
                    marca = p.IdMarcaNavigation.Nombre,
                    categoria = p.IdCategoriaNavigation.Nombre
                })
                .OrderBy(p => p.nombre)
                .Take(20)
                .ToListAsync();

            return Json(productos);
        }

        // AJAX: Obtener categorías
        [HttpGet]
        public async Task<JsonResult> GetCategorias()
        {
            var categorias = await _context.Categoria
                .Where(c => c.Estado == 1)
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre
                })
                .OrderBy(c => c.nombre)
                .ToListAsync();

            return Json(categorias);
        }

        // AJAX: Obtener cliente por ID
        [HttpGet]
        public async Task<JsonResult> GetClienteById(int id)
        {
            var cliente = await _context.Cliente
                .Where(c => c.Id == id && c.Estado == 1)
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    telefono = c.Telefono,
                    direccion = c.Direccion
                })
                .FirstOrDefaultAsync();

            return Json(cliente);
        }

        // AJAX: Actualizar datos de contacto del cliente
        [HttpPost]
        public async Task<JsonResult> UpdateContactoCliente([FromBody] ClienteContactoModel model)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(model.Id);
                if (cliente == null)
                {
                    return Json(new { success = false, message = "Cliente no encontrado" });
                }

                cliente.Telefono = model.Telefono;
                cliente.Direccion = model.Direccion;

                _context.Update(cliente);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Datos actualizados correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar: " + ex.Message });
            }
        }
    }
    public class ClienteContactoModel
    {
        public int Id { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}