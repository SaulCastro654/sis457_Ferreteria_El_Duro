using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFerreteria.Models;

namespace WebFerreteria.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        private readonly LabFerreteriaContext _context;

        public ProductosController(LabFerreteriaContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var productos = _context.Producto
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Where(p => p.Estado == 1)
                .OrderBy(p => p.Nombre);

            return View(await productos.ToListAsync());
        }
        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Producto
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdMarca"] = new SelectList(_context.Marca, "Id", "Nombre");
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre");

            return View();
        }

        private bool Validar(Producto p)
        {
            return !string.IsNullOrWhiteSpace(p.Nombre)
                && p.Precio > 0
                && p.Stock >= 0;
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (Validar(producto))
            {
                producto.UsuarioRegistro = User.Identity?.Name ?? "anon";
                producto.FechaRegistro = DateTime.Now;
                producto.Estado = 1;

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdMarca"] = new SelectList(_context.Marca, "Id", "Nombre", producto.IdMarca);
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);

            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
                return NotFound();

            ViewData["IdMarca"] = new SelectList(_context.Marca, "Id", "Nombre", producto.IdMarca);
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);

            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id)
                return NotFound();

            if (Validar(producto))
            {
                try
                {
                    producto.UsuarioRegistro = User.Identity?.Name ?? "";
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdMarca"] = new SelectList(_context.Marca, "Id", "Nombre", producto.IdMarca);
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);

            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Producto
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto != null)
            {
                producto.UsuarioRegistro = User.Identity?.Name ?? "";
                producto.Estado = -1; // eliminacion lógica
                _context.Update(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
