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
    public class DepartmentStaffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentStaffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DepartmentStaffs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DepartmentStaff.Include(d => d.Departments);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DepartmentStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DepartmentStaff == null)
            {
                return NotFound();
            }

            var departmentStaff = await _context.DepartmentStaff
                .Include(d => d.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentStaff == null)
            {
                return NotFound();
            }

            return View(departmentStaff);
        }

        // GET: DepartmentStaffs/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            return View();
        }

        // POST: DepartmentStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Name,Picture,ResearchArea,Education,Position,DepartmentId,Rank,IsActive,Email,CreatedAt,UpdatedAt,ResearchGate,LinkedIn,GoogleScholar,Scopus,CV,ORCID")] DepartmentStaff departmentStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", departmentStaff.DepartmentId);
            return View(departmentStaff);
        }

        // GET: DepartmentStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DepartmentStaff == null)
            {
                return NotFound();
            }

            var departmentStaff = await _context.DepartmentStaff.FindAsync(id);
            if (departmentStaff == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", departmentStaff.DepartmentId);
            return View(departmentStaff);
        }

        // POST: DepartmentStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Name,Picture,ResearchArea,Education,Position,DepartmentId,Rank,IsActive,Email,CreatedAt,UpdatedAt,ResearchGate,LinkedIn,GoogleScholar,Scopus,CV,ORCID")] DepartmentStaff departmentStaff)
        {
            if (id != departmentStaff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentStaffExists(departmentStaff.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", departmentStaff.DepartmentId);
            return View(departmentStaff);
        }

        // GET: DepartmentStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DepartmentStaff == null)
            {
                return NotFound();
            }

            var departmentStaff = await _context.DepartmentStaff
                .Include(d => d.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentStaff == null)
            {
                return NotFound();
            }

            return View(departmentStaff);
        }

        // POST: DepartmentStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DepartmentStaff == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DepartmentStaff'  is null.");
            }
            var departmentStaff = await _context.DepartmentStaff.FindAsync(id);
            if (departmentStaff != null)
            {
                _context.DepartmentStaff.Remove(departmentStaff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentStaffExists(int id)
        {
          return (_context.DepartmentStaff?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
