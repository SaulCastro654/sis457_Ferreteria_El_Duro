using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFerreteria.Models;

namespace WebFerreteria.Controllers
{
    public class MarcaController : Controller
    {
        private readonly LabFerreteriaContext _context;

        public MarcaController(LabFerreteriaContext context)
        {
            _context = context;
        }

        // GET: Marca
        public async Task<IActionResult> Index(string searchString)
        {
            var marcasQuery = _context.Marca
                .Where(m => m.Estado == 1);

            // Aplicar filtro de búsqueda si se proporciona
            if (!string.IsNullOrEmpty(searchString))
            {
                marcasQuery = marcasQuery.Where(m => m.Nombre.Contains(searchString));
            }

            var marcas = await marcasQuery
                .OrderBy(m => m.Nombre)
                .ToListAsync();

            // Pasar el término de búsqueda a la vista para mantenerlo en el input
            ViewData["CurrentFilter"] = searchString;

            return View(marcas);
        }

        // GET: Marca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Marca marca)
        {
            // Asignar valores primero
            marca.UsuarioRegistro = User.Identity?.Name ?? "Admin";
            marca.FechaRegistro = DateTime.Now;
            marca.Estado = 1;

            // Remover específicamente los errores de validación para estos campos
            ModelState.Remove("UsuarioRegistro");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(marca);
        }

        // GET: Marca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var marca = await _context.Marca.FindAsync(id);
            if (marca == null) return NotFound();

            return View(marca);
        }

        // POST: Marca/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Marca marcaForm)
        {
            if (id != marcaForm.Id) return NotFound();

            var marcaBD = await _context.Marca.FindAsync(id);
            if (marcaBD == null) return NotFound();

            // Remover campos que no vienen del formulario
            ModelState.Remove("UsuarioRegistro");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {
                try
                {
                    marcaBD.Nombre = marcaForm.Nombre;

                    _context.Update(marcaBD);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "No se pudo actualizar la marca.");
                }
            }

            return View(marcaForm);
        }

        // GET: Marca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var marca = await _context.Marca.FindAsync(id);
            if (marca == null) return NotFound();

            return View(marca);
        }

        // GET: Marca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var marca = await _context.Marca.FindAsync(id);
            if (marca == null) return NotFound();

            return View(marca);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marca = await _context.Marca.FindAsync(id);

            if (marca != null)
            {
                marca.Estado = 0;
                _context.Update(marca);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // AJAX para Productos
        [HttpPost]
        public async Task<JsonResult> CreateAjax(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return Json(new { success = false, message = "El nombre no puede estar vacío." });

            var marca = new Marca
            {
                Nombre = nombre,
                Estado = 1,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = User.Identity?.Name ?? "Admin"
            };

            _context.Marca.Add(marca);
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = marca.Id, nombre = marca.Nombre });
        }

        // AJAX: Buscar marcas SOLO POR NOMBRE (para uso en otros módulos)
        [HttpGet]
        public async Task<JsonResult> BuscarMarcas(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return Json(new List<object>());

            var marcas = await _context.Marca
                .Where(m => m.Estado == 1 && m.Nombre.Contains(termino))
                .Select(m => new
                {
                    id = m.Id,
                    nombre = m.Nombre
                })
                .OrderBy(m => m.nombre)
                .Take(10)
                .ToListAsync();

            return Json(marcas);
        }
    }
}