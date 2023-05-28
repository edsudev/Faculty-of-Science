using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Faculty_Portal.Data;
using Faculty_Portal.Models;

namespace Faculty_Portal.Controllers
{
    public class HODsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HODsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: HODs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hods.Include(h => h.Departments);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HODs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hods == null)
            {
                return NotFound();
            }

            var hOD = await _context.Hods
                .Include(h => h.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hOD == null)
            {
                return NotFound();
            }

            return View(hOD);
        }

        // GET: HODs/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: HODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile img ,HOD hOD)
        {
            //if (ModelState.IsValid)
            //{
                if (img != null && img.Length > 0)
                {
                    var uploadDir = @"hod";
                    var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                    var extension = Path.GetExtension(img.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = hOD.Email + extension;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        img.CopyTo(fs);
                        hOD.Picture = fileName;
                        if (fs != null)
                        {
                            fs.Close();
                            fs.Dispose();
                        }
                    }

              //  }
                _context.Add(hOD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", hOD.DepartmentId);
            return View(hOD);
        }

        // GET: HODs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hods == null)
            {
                return NotFound();
            }

            var hOD = await _context.Hods.FindAsync(id);
            if (hOD == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", hOD.DepartmentId);
            return View(hOD);
        }

        // POST: HODs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Picture,DepartmentId,IsActive,Message,ResumedOn,EndedOn,CreatedAt,UpdatedAt")] HOD hOD)
        {
            if (id != hOD.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hOD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HODExists(hOD.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", hOD.DepartmentId);
            return View(hOD);
        }

        // GET: HODs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hods == null)
            {
                return NotFound();
            }

            var hOD = await _context.Hods
                .Include(h => h.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hOD == null)
            {
                return NotFound();
            }

            return View(hOD);
        }

        // POST: HODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hods'  is null.");
            }
            var hOD = await _context.Hods.FindAsync(id);
            if (hOD != null)
            {
                _context.Hods.Remove(hOD);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HODExists(int id)
        {
          return (_context.Hods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
