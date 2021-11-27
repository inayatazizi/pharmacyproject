using LabManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabManagmentSystem.ViewModels
{
    public class ManageUserRoles
    {
        public PathoUser AppUser { get; set; }
        public List<SelectListItem> roles { get; set; }
    }
}
