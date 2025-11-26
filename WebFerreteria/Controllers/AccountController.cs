using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebFerreteria.Models;

namespace WebFerreteria.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly LabFerreteriaContext _context;

        public AccountController(LabFerreteriaContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Intentos de inicio de sesión no válidos.");
                return View(model);
            }

            // BUSCAR USUARIO
            var usuario = await _context.Usuario
                .Where(x => x.Estado == 1 &&
                            x.Nombre == model.Usuario &&
                            x.Clave == Encrypt(model.Clave))
                .FirstOrDefaultAsync();

            if (usuario != null)
            {
                TempData["isLogged"] = true;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("UserId", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, "user")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.recordarme,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties
                );

                if (returnUrl != null)
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                return View(model);
            }
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            TempData["isLogged"] = false;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "SIS457-1nf0!";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using Aes encryptor = Aes.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }

            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
