using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvaEva.Data;
using EvaEva.Models;
using Microsoft.AspNetCore.Authorization;

namespace EvaEva.Controllers
{
    [Authorize]
    public class LibroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Libro
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Libros.Include(l => l.Autor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            // Crear una lista de autores con el nombre completo
            var autores = _context.Autores
                .Select(a => new
                {
                    AutorId = a.AutorId,
                    NombreCompleto = $"{a.Nombre} {a.Apellido}" // Concatenar nombre y apellido
                })
                .ToList();

            // Pasar la lista a la vista con el nombre completo del autor
            ViewData["AutorId"] = new SelectList(autores, "AutorId", "NombreCompleto");
            return View();
        }

        // POST: Libro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibroId,Titulo,Genero,FechaPublicacion,Isbn,AutorId")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Para el caso de error, pasamos la lista de autores nuevamente
            var autores = _context.Autores
                .Select(a => new
                {
                    AutorId = a.AutorId,
                    NombreCompleto = $"{a.Nombre} {a.Apellido}"
                })
                .ToList();

            ViewData["AutorId"] = new SelectList(autores, "AutorId", "NombreCompleto", libro.AutorId);
            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            // Crear una lista de autores con el nombre completo para el campo de selección
            var autores = _context.Autores
                .Select(a => new
                {
                    AutorId = a.AutorId,
                    NombreCompleto = $"{a.Nombre} {a.Apellido}"
                })
                .ToList();

            ViewData["AutorId"] = new SelectList(autores, "AutorId", "NombreCompleto", libro.AutorId);
            return View(libro);
        }

        // POST: Libro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibroId,Titulo,Genero,FechaPublicacion,Isbn,AutorId")] Libro libro)
        {
            if (id != libro.LibroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.LibroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Para el caso de error, pasamos la lista de autores nuevamente
            var autores = _context.Autores
                .Select(a => new
                {
                    AutorId = a.AutorId,
                    NombreCompleto = $"{a.Nombre} {a.Apellido}"
                })
                .ToList();

            ViewData["AutorId"] = new SelectList(autores, "AutorId", "NombreCompleto", libro.AutorId);
            return View(libro);
        }

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }
    }
}
