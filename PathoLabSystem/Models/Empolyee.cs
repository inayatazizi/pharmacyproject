using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLabSystem.Models
{
    public class Empolyee
    {
       [Key]
        public int EmpId { get; set; }
        [Required(ErrorMessage = " Employee No Is Required")]
        public string EmpNo { get; set; }
        [Required(ErrorMessage =" Joining Date")]
        public string JionDate { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string EmpFName { get; set; }
        public string EmpLName { get; set; }
        [Required(ErrorMessage ="DoB")]
        public string DoB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus  { get; set; }
        [Required(ErrorMessage ="Designation")]
        public string Designation { get; set; }
        [Required(ErrorMessage ="Deprt")]
        public string  Department { get; set; }
        public int MobileNo { get; set; }
        [Required(ErrorMessage ="E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Address")]
        public string Address { get; set; }

    }
}
