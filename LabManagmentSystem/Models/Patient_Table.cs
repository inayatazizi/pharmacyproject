using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Models
{
    public class Patient_Table
    {
        [Key]
        public int Patient_Id { get; set; }
        [Required(ErrorMessage = "Frist Name is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Mobile No is required")]
        [MaxLength(11)]
        public string MobileNo { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Eamil is required")]
        public string Email { get; set; }
       // public string TestName { get; set; }
        [Required(ErrorMessage = "Appointment Date is required")]
        public DateTime AppointDate { get; set; }
        public int TestId { get; set; }        
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }
    }
    
}

