using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.MVC.Models;
using Microsoft.AspNetCore.Localization;
using SchoolManagementApp.MVC.Data;

namespace SchoolManagementApp.MVC.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SchoolManagementDbContext _context;

    public HomeController(ILogger<HomeController> logger, SchoolManagementDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl ?? "/");
    }

    public async Task<IActionResult> Dashboard()
    {
        var totalStudents = _context.Students.Count();
        var totalClasses = _context.Classes.Count();
        var totalLecturers = _context.Lecturers.Count();
        var totalEnrollments = _context.Enrollments.Count();
        var activeClasses = _context.Classes.Count(c => c.Status == "Started");
        var canceledClasses = _context.Classes.Count(c => c.Status == "Cancelled");
        var avgStudentsPerClass = totalClasses > 0 ? (double)totalEnrollments / totalClasses : 0;

        var dashboardData = new
        {
            TotalStudents = totalStudents,
            TotalClasses = totalClasses,
            TotalLecturers = totalLecturers,
            TotalEnrollments = totalEnrollments,
            ActiveClasses = activeClasses,
            CanceledClasses = canceledClasses,
            AvgStudentsPerClass = avgStudentsPerClass
        };

        return View(dashboardData);
    }
}
