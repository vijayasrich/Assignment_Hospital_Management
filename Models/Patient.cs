using System.ComponentModel.DataAnnotations;

namespace Assignment_Hospital_Management.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
