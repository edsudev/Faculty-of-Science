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
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DepartmentsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
              return _context.Departments != null ? 
                          View(await _context.Departments.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Portal(string? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Slug == id);
            ViewBag.DptName = department?.Name;
            ViewBag.DptInfo = department?.Info;
            ViewBag.DptPic = department?.Picture;

            if (department == null)
            {
                return NotFound();
            }



            var hod = new List<HOD>(_context.Hods.Where(c => c.DepartmentId == department.Id).Include(h => h.Departments).ToList());
            var departmentStaff = new List<DepartmentStaff>(_context.DepartmentStaff.Include(h => h.Departments).ToList());

            VM mymodel = new VM();
            mymodel.HODs = hod;
            mymodel.DepartmentStaffs = departmentStaff;
           
            return View(mymodel);

        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile img, Department department)
        {
           
            if (img != null && img.Length > 0)
            {
                var uploadDir = @"department";
                var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                var extension = Path.GetExtension(img.FileName);
                var webRootPath = _hostingEnvironment.WebRootPath;
                fileName = department.ShortCode + extension;
                var path = Path.Combine(webRootPath, uploadDir, fileName);
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    img.CopyTo(fs);
                    department.Picture = fileName;
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }

            }
            department.Slug = department.Name?.Replace(" ", "_");
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortCode,IsActive,Info,Picture")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Departments'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
