using System.ComponentModel.DataAnnotations;

namespace Assignment_Hospital_Management.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        public virtual ICollection<Doctor>? Doctors { get; set; }
    }
}
