using Assignment_Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Hospital_Management.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly MyContext _context;

        public AppointmentController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.Appointments
                                       .Include(a => a.Doctor)
                                       .Include(a => a.Patient)
                                       .ToList();
            return View(appointments);
        }
        public IActionResult Details(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        public IActionResult Create()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name");
            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name", appointment.DoctorId);
            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "Name", appointment.PatientId);
            return View(appointment);
        }

        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
                return NotFound();

            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name", appointment.DoctorId);
            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "Name", appointment.PatientId);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name", appointment.DoctorId);
            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "Name", appointment.PatientId);
            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
