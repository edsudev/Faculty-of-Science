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
            var facultyStaff = new List<FacultyStaff>(_context.FacultyStaffs.ToList());
            var department = new List<Department>();

            VM mymodel = new VM();
            mymodel.Deans = dean;
            mymodel.HODs = hod;
            mymodel.FacultyStaffs = facultyStaff;
            mymodel.Departments = department;

            return View(mymodel);
        }

        public IActionResult Newsletter()
        {
            return View();
        }
        public IActionResult Departments()
        {
            var dpts = (from s in _context.Departments select s).ToList();
            return View(dpts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Newsletter(Subscription sub)
        {
            var emailExist = _context.Subscriptions.Where(x => x.Email == sub.Email).Select(x => x.Email).FirstOrDefault();
            if (emailExist != null)
            {
                try
                {
                    _context.Add(sub);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "An error occured while trying to process your " +
                                      "request. Kindly contact the ICT if problem persist.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["error"] = "An error occured while trying to process your " +
                                        "request. Kindly contact the ICT if problem persist.";
                    throw;
                }

            }
            else
            {
                TempData["emailExist"] = "It seems the email address you provided already exists.";
                return RedirectToAction(nameof(Index));
            }
            
            
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}