using LabManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.ViewModels
{
    public class PatientViewModel
    {
        public Patient_Table Patient_Table { get; set; }
        public Test Test { get; set; }
    }
}
