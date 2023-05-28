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
    public class FacultyStaffsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FacultyStaffsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: FacultyStaffs
        public async Task<IActionResult> Index()
        {
              return _context.FacultyStaffs != null ? 
                          View(await _context.FacultyStaffs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FacultyStaffs'  is null.");
        }

        // GET: FacultyStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FacultyStaffs == null)
            {
                return NotFound();
            }

            var facultyStaff = await _context.FacultyStaffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyStaff == null)
            {
                return NotFound();
            }

            return View(facultyStaff);
        }

        // GET: FacultyStaffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacultyStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile img, FacultyStaff facultyStaff)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.Length > 0)
                {
                    var uploadDir = @"facultystaff";
                    var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                    var extension = Path.GetExtension(img.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = facultyStaff.Email + extension;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        img.CopyTo(fs);
                        facultyStaff.Picture = fileName;
                        if (fs != null)
                        {
                            fs.Close();
                            fs.Dispose();
                        }
                    }
                }
                    _context.Add(facultyStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facultyStaff);
        }

        // GET: FacultyStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FacultyStaffs == null)
            {
                return NotFound();
            }

            var facultyStaff = await _context.FacultyStaffs.FindAsync(id);
            if (facultyStaff == null)
            {
                return NotFound();
            }
            return View(facultyStaff);
        }

        // POST: FacultyStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Picture,Position,IsActive,Email,CreatedAt,UpdatedAt")] FacultyStaff facultyStaff)
        {
            if (id != facultyStaff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyStaffExists(facultyStaff.Id))
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
            return View(facultyStaff);
        }

        // GET: FacultyStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FacultyStaffs == null)
            {
                return NotFound();
            }

            var facultyStaff = await _context.FacultyStaffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyStaff == null)
            {
                return NotFound();
            }

            return View(facultyStaff);
        }

        // POST: FacultyStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FacultyStaffs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FacultyStaffs'  is null.");
            }
            var facultyStaff = await _context.FacultyStaffs.FindAsync(id);
            if (facultyStaff != null)
            {
                _context.FacultyStaffs.Remove(facultyStaff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyStaffExists(int id)
        {
          return (_context.FacultyStaffs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
