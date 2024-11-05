using System.ComponentModel.DataAnnotations;

namespace Assignment_Hospital_Management.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        public virtual Doctor ? Doctor { get; set; }
        public virtual Patient ? Patient { get; set; }
    }
}
