using Faculty_Portal.Data;
using Faculty_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace Faculty_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var dean = new List<Dean>(_context.Deans.ToList());
            var hod = new List<HOD>(_context.Hods.Include(h => h.Departments).ToList());
            //var departmentStaff = new List<DepartmentStaff>(_context.DepartmentStaff.Include(h => h.Departments).ToList());
            var facultyStaff = new List<FacultyStaff>(_context.FacultyStaffs.ToList());
            var department = new List<Department>();

            VM mymodel = new VM();
            mymodel.Deans = dean;
            mymodel.HODs = hod;
            //mymodel.DepartmentStaffs = departmentStaff;
            mymodel.FacultyStaffs = facultyStaff;
            mymodel.Departments = department;

            return View(mymodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}