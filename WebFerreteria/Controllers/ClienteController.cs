using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFerreteria.Models;

namespace WebFerreteria.Controllers
{
    public class ClienteController : Controller
    {
        private readonly LabFerreteriaContext _context;

        public ClienteController(LabFerreteriaContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Cliente
                .Where(c => c.Estado == 1)
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            return View(clientes);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            // Asignar valores primero
            cliente.UsuarioRegistro = User.Identity?.Name ?? "Admin";
            cliente.FechaRegistro = DateTime.Now;
            cliente.Estado = 1;

            // Remover específicamente los errores de validación para estos campos
            ModelState.Remove("UsuarioRegistro");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente clienteForm)
        {
            if (id != clienteForm.Id) return NotFound();

            var clienteBD = await _context.Cliente.FindAsync(id);
            if (clienteBD == null) return NotFound();

            // Remover campos que no vienen del formulario
            ModelState.Remove("UsuarioRegistro");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {
                try
                {
                    clienteBD.Nombre = clienteForm.Nombre;
                    clienteBD.Telefono = clienteForm.Telefono;
                    clienteBD.Direccion = clienteForm.Direccion;

                    _context.Update(clienteBD);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "No se pudo actualizar el cliente.");
                }
            }

            return View(clienteForm);
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente != null)
            {
                cliente.Estado = 0;
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        // AJAX: Obtener cliente por ID
        [HttpGet]
        public async Task<JsonResult> GetClienteById(int id)
        {
            var cliente = await _context.Cliente
                .Where(c => c.Id == id)
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

        // AJAX: Actualizar contacto del cliente
        [HttpPost]
        public async Task<JsonResult> UpdateContactoAjax(int id, string telefono, string direccion)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                    return Json(new { success = false, message = "Cliente no encontrado" });

                cliente.Telefono = telefono;
                cliente.Direccion = direccion;

                _context.Update(cliente);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}