using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.MVC.Data;

namespace SchoolManagementApp.MVC.Controllers
{
     [Authorize]
    public class CoursesController : Controller
    {
        private readonly SchoolManagementDbContext _context;
        private readonly INotyfService _notyfService;

        public CoursesController(SchoolManagementDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
              /* 
                If (databaseTableExists) {
                    var recordsInTable = await _context.Courses.ToListAsync();
                    return View(recordsInTable);
                }

                return Problem("Entity set 'SchoolManagementDbContext.Courses'  is null.");
              */
              return _context.Courses != null ? 
                          View(await _context.Courses.ToListAsync()) :
                          Problem("Entity set 'SchoolManagementDbContext.Courses'  is null.");
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                _notyfService.Success("Course created successfully.");
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,Credits")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    _notyfService.Success("Course updated successfully.");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'SchoolManagementDbContext.Courses' is null.");
            }

            var course = await _context.Courses
                .Include(c => c.Classes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course != null)
            {
                if (course.Classes.Any())
                {
                    //TempData["SwalMessage"] = "Cannot delete course. The course has associated classes.";
                    //TempData["SwalType"] = "error";
                    _notyfService.Error("Cannot delete course. The course has associated classes.");
                    return RedirectToAction(nameof(Index));
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }

            // TempData["SwalMessage"] = "Course deleted successfully.";
            // TempData["SwalType"] = "success";
            _notyfService.Success("Course deleted successfully.");
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
