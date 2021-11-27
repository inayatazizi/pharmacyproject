using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Models
{
    public class Proffessional
    {
        [Key]
        public int ProfId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile No is required")] 
        [MaxLength(11)]
        public int MobileNo { get; set; }
        public string Department { get; set; }
        [Required(ErrorMessage = "Qualification is Required")]
        public string Qualification { get; set; }
        public string University { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public string Experince { get; set; }
        public string Address { get; set; }
    }
}
