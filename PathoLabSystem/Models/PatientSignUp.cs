using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PathologyLabManagment.Models
{
    public class PatientSignUp
    {
        [Key]
        public int Patient_Id { get; set; }
        [Required(ErrorMessage ="Frist Name is Required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage ="Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage ="Gender is required")]
        public string Sex { get; set; }
        [Required(ErrorMessage ="Mobile No is required")]
        public string MobileNo { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage ="Eamil is required")]
        public string Email { get; set; }   
        public string Test { get; set; }
        public string DrName { get; set; }
        [Required(ErrorMessage = "Appointment Date is required")]
        public DateTime AppointDate{ get; set; }
        [Required(ErrorMessage = "Appointment Time is required")]
        public TimeSpan AppointTime { get; set; }
    }
}
