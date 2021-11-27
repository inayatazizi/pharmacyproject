using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.Models
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string TestCode { get; set; }
        [Required(ErrorMessage = "Test Name is required")]
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal TestPrice { get; set; }
        public ICollection<Patient_Table> Patient_Tables { get; set; }
    }
}
