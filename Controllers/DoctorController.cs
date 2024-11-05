using Assignment_Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        private readonly MyContext _context;

        public DoctorController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctors = _context.Doctors
                                  .Include(d => d.Hospital)
                                  .ToList();
            return View(doctors);
        }
        public IActionResult Details(int id)
        {
            var doctor = _context.Doctors.Include(d => d.Hospital)
                                         .FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        public IActionResult Create()
        {
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalId", "Name", doctor.HospitalId);
            return View(doctor);
        }

        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
                return NotFound();

            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalId", "Name", doctor.HospitalId);
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(doctor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalId", "Name", doctor.HospitalId);
            return View(doctor);
        }

        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Include(d => d.Hospital).FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

