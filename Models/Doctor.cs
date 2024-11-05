using System.ComponentModel.DataAnnotations;

namespace Assignment_Hospital_Management.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Specialty { get; set; }

        public int HospitalId { get; set; }

        public virtual Hospital? Hospital { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}

