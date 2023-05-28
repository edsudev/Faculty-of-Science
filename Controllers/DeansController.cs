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
    public class DeansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public DeansController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Deans
        public async Task<IActionResult> Index()
        {
              return _context.Deans != null ? 
                          View(await _context.Deans.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Deans'  is null.");
        }

        // GET: Deans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Deans == null)
            {
                return NotFound();
            }

            var dean = await _context.Deans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dean == null)
            {
                return NotFound();
            }

            return View(dean);
        }

        // GET: Deans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dean dean, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.Length > 0)
                {
                    var uploadDir = @"dean";
                    var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                    var extension = Path.GetExtension(img.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = dean.Email + extension;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        img.CopyTo(fs);
                        dean.Picture = fileName;
                        if (fs != null)
                        {
                            fs.Close();
                            fs.Dispose();
                        }
                    }

                }
                _context.Add(dean);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dean);
        }

        // GET: Deans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deans == null)
            {
                return NotFound();
            }

            var dean = await _context.Deans.FindAsync(id);
            if (dean == null)
            {
                return NotFound();
            }
            return View(dean);
        }

        // POST: Deans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Picture,IsActive,Message,ResumedOn,EndedOn,CreatedAt,UpdatedAt")] Dean dean)
        {
            if (id != dean.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dean);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeanExists(dean.Id))
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
            return View(dean);
        }

        // GET: Deans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Deans == null)
            {
                return NotFound();
            }

            var dean = await _context.Deans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dean == null)
            {
                return NotFound();
            }

            return View(dean);
        }

        // POST: Deans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deans == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Deans'  is null.");
            }
            var dean = await _context.Deans.FindAsync(id);
            if (dean != null)
            {
                _context.Deans.Remove(dean);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeanExists(int id)
        {
          return (_context.Deans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
