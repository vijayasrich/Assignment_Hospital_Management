using Assignment_Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_Hospital_Management.Controllers
{
    public class HospitalController : Controller
    {
        private readonly MyContext _context;

        public HospitalController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hospitals = _context.Hospitals.ToList();
            return View(hospitals);
        }
        public IActionResult Details(int id)
        {
            var hospital = _context.Hospitals.FirstOrDefault(h => h.HospitalId == id);
            if (hospital == null)
                return NotFound();

            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                _context.Hospitals.Add(hospital);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(hospital);
        }

        public IActionResult Edit(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital == null)
                return NotFound();

            return View(hospital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Hospital hospital)
        {
            if (id != hospital.HospitalId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(hospital);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(hospital);
        }

        public IActionResult Delete(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital == null)
                return NotFound();

            return View(hospital);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
